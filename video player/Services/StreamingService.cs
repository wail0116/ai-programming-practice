using System;
using System.IO;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace video_player.Services
{
    public class VideoInfo
    {
        public string FilePath { get; set; } = "";
        public string FileName => Path.GetFileName(FilePath);
        public long DurationMs { get; set; }
        public int VideoWidth { get; set; }
        public int VideoHeight { get; set; }
        public string Codec { get; set; } = "";
        public int AudioTracks { get; set; }
        public int SubtitleTracks { get; set; }
        public string Duration => TimeSpan.FromMilliseconds(DurationMs).ToString(@"hh\:mm\:ss");
        public string Resolution => VideoWidth > 0 ? $"{VideoWidth}x{VideoHeight}" : "N/A";
    }

    public class StreamingService : IDisposable
    {
        private LibVLC _libVLC;
        private MediaPlayer _player;
        private VideoView _view;
        private Media _currentMedia;
        private Timer _positionTimer;
        private VideoInfo _currentInfo;

        public VideoView View => _view;
        public VideoInfo CurrentInfo => _currentInfo;

        public bool IsPlaying
        {
            get { try { return _player != null && _player.IsPlaying; } catch { return false; } }
        }

        public long Length
        {
            get { try { return _player?.Length ?? 0; } catch { return 0; } }
        }

        public float Position
        {
            get { try { return _player?.Position ?? 0f; } catch { return 0f; } }
            set { try { if (_player != null && _player.Length > 0) _player.Position = value; } catch { } }
        }

        public int Volume
        {
            get { try { return _player?.Volume ?? 0; } catch { return 0; } }
            set { try { if (_player != null) _player.Volume = Math.Max(0, Math.Min(200, value)); } catch { } }
        }

        public float Rate
        {
            get { try { return _player?.Rate ?? 1.0f; } catch { return 1.0f; } }
            set { try { if (_player != null) _player.SetRate(value); } catch { } }
        }

        public void SetBrightness(float val) { try { _player?.SetAdjustFloat(VideoAdjustOption.Enable, 1); _player?.SetAdjustFloat(VideoAdjustOption.Brightness, val); } catch { } }
        public void SetContrast(float val) { try { _player?.SetAdjustFloat(VideoAdjustOption.Enable, 1); _player?.SetAdjustFloat(VideoAdjustOption.Contrast, val); } catch { } }
        public void SetSaturation(float val) { try { _player?.SetAdjustFloat(VideoAdjustOption.Enable, 1); _player?.SetAdjustFloat(VideoAdjustOption.Saturation, val); } catch { } }
        public void SetGamma(float val) { try { _player?.SetAdjustFloat(VideoAdjustOption.Enable, 1); _player?.SetAdjustFloat(VideoAdjustOption.Gamma, val); } catch { } }

        public long CurrentTime
        {
            get { try { return _player != null && _player.Length > 0 ? (long)(_player.Position * _player.Length) : 0; } catch { return 0; } }
        }

        public event Action<VLCState> StateChanged;
        public event Action<long, long> TimeChanged;
        public event Action<VideoInfo> InfoReady;
        public event Action OnPlaybackEnded;

        public bool Initialize(Control parent)
        {
            var vlcDir = FindVlcDirectory();
            if (vlcDir == null) return false;
            try
            {
                Core.Initialize(vlcDir.FullName);
                _libVLC = new LibVLC();
                _player = new MediaPlayer(_libVLC);
                _view = new VideoView { Dock = DockStyle.Fill, MediaPlayer = _player };
                parent.Controls.Add(_view);

                _player.Playing += (s, e) => StateChanged?.Invoke(VLCState.Playing);
                _player.Paused += (s, e) => StateChanged?.Invoke(VLCState.Paused);
                _player.Stopped += (s, e) => StateChanged?.Invoke(VLCState.Stopped);
                _player.EndReached += (s, e) =>
                {
                    StateChanged?.Invoke(VLCState.Ended);
                    OnPlaybackEnded?.Invoke();
                };
                _player.EncounteredError += (s, e) => StateChanged?.Invoke(VLCState.Error);

                _positionTimer = new Timer { Interval = 250 };
                _positionTimer.Tick += (s2, e2) =>
                {
                    if (_player != null && _player.IsPlaying && _player.Length > 0)
                        TimeChanged?.Invoke(CurrentTime, _player.Length);
                };

                return true;
            }
            catch { return false; }
        }

        public bool Play(string filePath)
        {
            if (_player == null || string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return false;
            try
            {
                Stop();
                _currentMedia = new Media(_libVLC, new Uri(filePath));
                _player.Play(_currentMedia);
                _positionTimer.Start();
                ExtractVideoInfo(filePath);
                return true;
            }
            catch { return false; }
        }

        public void Pause()
        {
            try { _player?.Pause(); _positionTimer.Stop(); } catch { }
        }

        public void Resume()
        {
            try { _player?.Play(); _positionTimer.Start(); } catch { }
        }

        public void Stop()
        {
            _positionTimer?.Stop();
            try { _player?.Stop(); } catch { }
            _currentMedia?.Dispose();
            _currentMedia = null;
            _currentInfo = null;
        }

        public bool TakeSnapshot(string filePath)
        {
            if (_player == null || !_player.IsPlaying) return false;
            try
            {
                var d = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(d) && !Directory.Exists(d)) Directory.CreateDirectory(d);
                return _player.TakeSnapshot(0, filePath, 0, 0);
            }
            catch { return false; }
        }

        void ExtractVideoInfo(string filePath)
        {
            _currentInfo = new VideoInfo { FilePath = filePath };
            try
            {
                System.Threading.Tasks.Task.Delay(500).ContinueWith(_ =>
                {
                    try
                    {
                        if (_player != null && _player.Length > 0)
                        {
                            _currentInfo.DurationMs = _player.Length;
                            foreach (var track in _player.Media?.Tracks ?? new MediaTrack[0])
                            {
                                if (track.TrackType == TrackType.Video)
                                {
                                    _currentInfo.VideoWidth = (int)track.Data.Video.Width;
                                    _currentInfo.VideoHeight = (int)track.Data.Video.Height;
                                    uint codec = (uint)track.Codec;
                                    string fourcc = "" + (char)(codec & 0xFF) + (char)((codec >> 8) & 0xFF) + (char)((codec >> 16) & 0xFF) + (char)((codec >> 24) & 0xFF);
                                    var f = fourcc.ToLower();
                                    if (f.StartsWith("h264") || f.StartsWith("avc1")) _currentInfo.Codec = "H.264";
                                    else if (f.StartsWith("h265") || f.StartsWith("hevc")) _currentInfo.Codec = "H.265/HEVC";
                                    else if (f.StartsWith("mp4v") || f.StartsWith("mpg4")) _currentInfo.Codec = "MPEG-4";
                                    else if (f.StartsWith("vp9")) _currentInfo.Codec = "VP9";
                                    else if (f.StartsWith("av01")) _currentInfo.Codec = "AV1";
                                    else _currentInfo.Codec = fourcc;
                                }
                                else if (track.TrackType == TrackType.Audio) _currentInfo.AudioTracks++;
                                else if (track.TrackType == TrackType.Text) _currentInfo.SubtitleTracks++;
                            }
                        }
                    }
                    catch { }
                    InfoReady?.Invoke(_currentInfo);
                });
            }
            catch { }
        }

        static DirectoryInfo FindVlcDirectory()
        {
            var arch = Environment.Is64BitProcess ? "win-x64" : "win-x86";
            var bundled = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc", arch);
            if (Directory.Exists(bundled) && File.Exists(Path.Combine(bundled, "libvlc.dll")))
                return new DirectoryInfo(bundled);
            string[] paths = {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "VideoLAN", "VLC"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "VideoLAN", "VLC"),
            };
            foreach (var p in paths)
                if (Directory.Exists(p) && File.Exists(Path.Combine(p, "libvlc.dll")))
                    return new DirectoryInfo(p);
            return null;
        }

        public void Dispose()
        {
            _positionTimer?.Stop();
            _positionTimer?.Dispose();
            try { _player?.Stop(); } catch { }
            _currentMedia?.Dispose();
            _player?.Dispose();
            _view?.Dispose();
            _libVLC?.Dispose();
        }
    }
}
