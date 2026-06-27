using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using video_player.Models;
using video_player.Services;

namespace video_player
{
    public partial class Form1 : Form
    {
        StreamingService _streaming;
        MonitorConfig _config;
        List<string> _playlist = new List<string>();
        int _currentIndex = -1;
        bool _isPaused;
        bool _seeking;

        // Video adjustment controls (added programmatically to sidebar)
        Label _lblBrightness, _lblContrast, _lblSaturation, _lblGamma;
        TrackBar _tbarBrightness, _tbarContrast, _tbarSaturation, _tbarGamma;
        Label _lblBriVal, _lblConVal, _lblSatVal, _lblGamVal;

        enum PlayMode { Sequential, Shuffle, RepeatOne, RepeatAll }
        PlayMode _playMode = PlayMode.Sequential;
        Random _rng = new Random();
        List<int> _shuffleOrder = new List<int>();
        int _shufflePos;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            if (DesignMode) return;

            btnAddFiles.Click += BtnAddFiles_Click;
            btnRemove.Click += BtnRemove_Click;
            btnClearAll.Click += BtnClearAll_Click;
            btnScreenshot.Click += BtnScreenshot_Click;
            btnInfo.Click += BtnInfo_Click;
            btnPrev.Click += BtnPrev_Click;
            btnPlayPause.Click += BtnPlayPause_Click;
            btnStop.Click += BtnStop_Click;
            btnNext.Click += BtnNext_Click;
            tbarProgress.MouseDown += (s, e) => _seeking = true;
            tbarProgress.MouseUp += TbarProgress_MouseUp;
            tbarVolume.ValueChanged += TbarVolume_ValueChanged;
            cmbSpeed.SelectedIndexChanged += CmbSpeed_Changed;
            cmbPlayMode.SelectedIndexChanged += CmbPlayMode_Changed;
            lstPlaylist.SelectedIndexChanged += LstPlaylist_Selected;
            this.FormClosing += OnFormClosing_Handler;

            _config = MonitorConfig.Load("config.json");
            cmbPlayMode.SelectedIndex = 0;
            cmbSpeed.SelectedIndex = 3;
            RestoreRecentFiles();
            InitAdjustControls();
            Log("Video Player Pro ready.");
        }

        void InitAdjustControls()
        {
            int baseY = 590;
            int gap = 36;

            // Brightness
            _lblBrightness = new Label { Text = "Brightness 亮度", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(120, 120, 130), Location = new Point(12, baseY), Size = new Size(120, 18) };
            _tbarBrightness = new TrackBar { BackColor = Color.FromArgb(245, 246, 248), TickStyle = TickStyle.None, Minimum = 0, Maximum = 200, Value = 100, Location = new Point(12, baseY + 16), Size = new Size(200, 30) };
            _lblBriVal = new Label { Text = "100%", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(60, 60, 65), Location = new Point(218, baseY + 18), Size = new Size(50, 18), TextAlign = ContentAlignment.MiddleRight };
            _tbarBrightness.ValueChanged += (s, e) => { if (_streaming != null) { float v = _tbarBrightness.Value / 100f; _streaming.SetBrightness(v); _lblBriVal.Text = $"{(int)(v * 100)}%"; } };

            // Contrast
            int y2 = baseY + gap;
            _lblContrast = new Label { Text = "Contrast 对比度", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(120, 120, 130), Location = new Point(12, y2), Size = new Size(120, 18) };
            _tbarContrast = new TrackBar { BackColor = Color.FromArgb(245, 246, 248), TickStyle = TickStyle.None, Minimum = 0, Maximum = 200, Value = 100, Location = new Point(12, y2 + 16), Size = new Size(200, 30) };
            _lblConVal = new Label { Text = "100%", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(60, 60, 65), Location = new Point(218, y2 + 18), Size = new Size(50, 18), TextAlign = ContentAlignment.MiddleRight };
            _tbarContrast.ValueChanged += (s, e) => { if (_streaming != null) { float v = _tbarContrast.Value / 100f; _streaming.SetContrast(v); _lblConVal.Text = $"{(int)(v * 100)}%"; } };

            // Saturation
            int y3 = baseY + gap * 2;
            _lblSaturation = new Label { Text = "Saturation 饱和度", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(120, 120, 130), Location = new Point(12, y3), Size = new Size(120, 18) };
            _tbarSaturation = new TrackBar { BackColor = Color.FromArgb(245, 246, 248), TickStyle = TickStyle.None, Minimum = 0, Maximum = 200, Value = 100, Location = new Point(12, y3 + 16), Size = new Size(200, 30) };
            _lblSatVal = new Label { Text = "100%", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(60, 60, 65), Location = new Point(218, y3 + 18), Size = new Size(50, 18), TextAlign = ContentAlignment.MiddleRight };
            _tbarSaturation.ValueChanged += (s, e) => { if (_streaming != null) { float v = _tbarSaturation.Value / 100f; _streaming.SetSaturation(v); _lblSatVal.Text = $"{(int)(v * 100)}%"; } };

            // Gamma
            int y4 = baseY + gap * 3;
            _lblGamma = new Label { Text = "Gamma 伽马", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(120, 120, 130), Location = new Point(12, y4), Size = new Size(120, 18) };
            _tbarGamma = new TrackBar { BackColor = Color.FromArgb(245, 246, 248), TickStyle = TickStyle.None, Minimum = 0, Maximum = 200, Value = 100, Location = new Point(12, y4 + 16), Size = new Size(200, 30) };
            _lblGamVal = new Label { Text = "100%", Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(60, 60, 65), Location = new Point(218, y4 + 18), Size = new Size(50, 18), TextAlign = ContentAlignment.MiddleRight };
            _tbarGamma.ValueChanged += (s, e) => { if (_streaming != null) { float v = _tbarGamma.Value / 100f; _streaming.SetGamma(v); _lblGamVal.Text = $"{(int)(v * 100)}%"; } };

            panelSidebar.Controls.AddRange(new Control[] {
                _lblBrightness, _tbarBrightness, _lblBriVal,
                _lblContrast, _tbarContrast, _lblConVal,
                _lblSaturation, _tbarSaturation, _lblSatVal,
                _lblGamma, _tbarGamma, _lblGamVal
            });
        }

        void RestoreRecentFiles()
        {
            foreach (var f in _config.RecentFiles)
            {
                if (File.Exists(f) && !_playlist.Contains(f))
                {
                    _playlist.Add(f);
                    lstPlaylist.Items.Add(Path.GetFileName(f));
                }
            }
        }

        bool InitStreaming()
        {
            if (_streaming != null) return true;
            _streaming = new StreamingService();
            if (!_streaming.Initialize(panelVideo))
            {
                Log("[ERROR] VLC runtime not found.");
                return false;
            }
            _streaming.StateChanged += s =>
                this.Invoke(new Action(() => lblState.Text = $"State: {s}"));
            _streaming.TimeChanged += (pos, len) =>
                this.Invoke(new Action(() => UpdateProgress(pos, len)));
            _streaming.OnPlaybackEnded += () =>
                this.Invoke(new Action(() => OnVideoEnded()));
            _streaming.InfoReady += info =>
                this.Invoke(new Action(() => Log($"[INFO] {info.Resolution} {info.Codec} {info.Duration}")));
            return true;
        }

        void BtnAddFiles_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Video Files|*.mp4;*.mkv;*.avi;*.flv;*.ts;*.mov;*.wmv;*.webm;*.m4v|All Files|*.*";
                dlg.Multiselect = true;
                dlg.InitialDirectory = string.IsNullOrEmpty(_config.LastDirectory)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
                    : _config.LastDirectory;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _config.LastDirectory = Path.GetDirectoryName(dlg.FileNames[0]);
                    foreach (var f in dlg.FileNames) AddToPlaylist(f);
                    _config.Save("config.json");
                }
            }
        }

        void AddToPlaylist(string filePath)
        {
            if (_playlist.Contains(filePath)) return;
            _playlist.Add(filePath);
            lstPlaylist.Items.Add(Path.GetFileName(filePath));
            _config.AddRecent(filePath);
        }

        void BtnRemove_Click(object sender, EventArgs e)
        {
            int idx = lstPlaylist.SelectedIndex;
            if (idx < 0) return;
            _playlist.RemoveAt(idx);
            lstPlaylist.Items.RemoveAt(idx);
            if (idx == _currentIndex) _currentIndex = -1;
            else if (idx < _currentIndex) _currentIndex--;
        }

        void BtnClearAll_Click(object sender, EventArgs e)
        {
            _streaming?.Stop();
            _playlist.Clear();
            lstPlaylist.Items.Clear();
            _currentIndex = -1;
            lblTimeCur.Text = "00:00";
            lblTimeTotal.Text = "00:00";
            tbarProgress.Value = 0;
        }

        void LstPlaylist_Selected(object sender, EventArgs e)
        {
            if (lstPlaylist.SelectedIndex >= 0 && lstPlaylist.SelectedIndex != _currentIndex)
            {
                _currentIndex = lstPlaylist.SelectedIndex;
                PlayCurrent();
            }
        }

        void BtnPlayPause_Click(object sender, EventArgs e)
        {
            if (_streaming != null && _isPaused)
            {
                _streaming.Resume();
                _isPaused = false;
                btnPlayPause.Text = "⏸ 暂停";
                Log("[RESUME] 继续");
            }
            else if (_streaming != null && _streaming.IsPlaying)
            {
                _streaming.Pause();
                _isPaused = true;
                btnPlayPause.Text = "▶ 播放";
                Log("[PAUSE] 暂停");
            }
            else
            {
                if (_playlist.Count == 0) { Log("[INFO] Add files first 请先添加文件."); return; }
                if (_currentIndex < 0) _currentIndex = 0;
                PlayCurrent();
            }
        }

        void BtnStop_Click(object sender, EventArgs e)
        {
            _streaming?.Stop();
            _isPaused = false;
            btnPlayPause.Text = "▶ 播放";
            lblTimeCur.Text = "00:00";
            tbarProgress.Value = 0;
            lblState.Text = "State: Stopped 已停止";
            Log("[STOP] 停止");
        }

        void BtnPrev_Click(object sender, EventArgs e)
        {
            if (_playlist.Count == 0) return;
            if (_playMode == PlayMode.Shuffle && _shuffleOrder.Count > 1)
            {
                _shufflePos = Math.Max(0, _shufflePos - 1);
                _currentIndex = _shuffleOrder[_shufflePos];
            }
            else
            {
                _currentIndex = _currentIndex <= 0 ? _playlist.Count - 1 : _currentIndex - 1;
            }
            PlayCurrent();
        }

        void BtnNext_Click(object sender, EventArgs e)
        {
            if (_playlist.Count == 0) return;
            PlayNext();
        }

        void PlayCurrent()
        {
            if (_currentIndex < 0 || _currentIndex >= _playlist.Count) return;
            if (!InitStreaming()) return;
            string filePath = _playlist[_currentIndex];
            lstPlaylist.SelectedIndex = _currentIndex;
            if (_streaming.Play(filePath))
            {
                _isPaused = false;
                btnPlayPause.Text = "⏸ 暂停";
                Log($"[PLAY] {Path.GetFileName(filePath)}");
                lblState.Text = "State: Playing 播放中";
                lblTimeCur.Text = "00:00";
                tbarProgress.Value = 0;
            }
            else
            {
                Log($"[ERROR] Cannot play: {Path.GetFileName(filePath)}");
            }
        }

        void PlayNext()
        {
            switch (_playMode)
            {
                case PlayMode.RepeatOne: PlayCurrent(); return;
                case PlayMode.Shuffle:
                    if (_shufflePos >= _shuffleOrder.Count - 1)
                    {
                        _shuffleOrder = Enumerable.Range(0, _playlist.Count).OrderBy(_ => _rng.Next()).ToList();
                        _shufflePos = 0;
                    }
                    else _shufflePos++;
                    _currentIndex = _shuffleOrder[_shufflePos];
                    break;
                case PlayMode.RepeatAll:
                    _currentIndex = (_currentIndex + 1) % _playlist.Count;
                    break;
                default:
                    _currentIndex++;
                    if (_currentIndex >= _playlist.Count) { Log("[END] Playlist finished."); return; }
                    break;
            }
            PlayCurrent();
        }

        void OnVideoEnded()
        {
            this.Invoke(new Action(() =>
            {
                Log($"[ENDED] {Path.GetFileName(_playlist[_currentIndex])}");
                PlayNext();
            }));
        }

        void UpdateProgress(long pos, long len)
        {
            if (_seeking) return;
            if (len > 0) { tbarProgress.Value = (int)(pos * 1000 / len); lblTimeTotal.Text = FormatTime(len); }
            lblTimeCur.Text = FormatTime(pos);
        }

        void TbarProgress_MouseUp(object sender, MouseEventArgs e)
        {
            if (_streaming != null && _streaming.Length > 0)
                _streaming.Position = tbarProgress.Value / 1000f;
            _seeking = false;
        }

        void TbarVolume_ValueChanged(object sender, EventArgs e)
        {
            if (_streaming != null) _streaming.Volume = tbarVolume.Value;
            lblVolVal.Text = $"{tbarVolume.Value}%";
        }

        void CmbSpeed_Changed(object sender, EventArgs e)
        {
            if (_streaming == null) return;
            var speeds = new[] { 0.25f, 0.5f, 0.75f, 1.0f, 1.25f, 1.5f, 2.0f };
            int idx = cmbSpeed.SelectedIndex;
            if (idx >= 0 && idx < speeds.Length)
            { _streaming.Rate = speeds[idx]; Log($"[SPEED] {speeds[idx]}x"); }
        }

        void CmbPlayMode_Changed(object sender, EventArgs e)
        {
            _playMode = (PlayMode)cmbPlayMode.SelectedIndex;
            if (_playMode == PlayMode.Shuffle)
            {
                _shuffleOrder = Enumerable.Range(0, _playlist.Count).OrderBy(_ => _rng.Next()).ToList();
                _shufflePos = 0;
            }
        }

        void BtnScreenshot_Click(object sender, EventArgs e)
        {
            if (_streaming == null || !_streaming.IsPlaying)
            { Log("[INFO] No video playing. 当前无视频播放。"); return; }
            var dir = _config.ScreenshotDir;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string name = Path.GetFileNameWithoutExtension(_playlist[_currentIndex]);
            string path = Path.Combine(dir, $"{name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            if (_streaming.TakeSnapshot(path))
                Log($"[SCREENSHOT] {Path.GetFileName(path)}");
            else
                Log("[ERROR] Screenshot failed.");
        }

        void BtnInfo_Click(object sender, EventArgs e)
        {
            if (_streaming?.CurrentInfo == null)
            { Log("[INFO] No video loaded. 没有加载视频。"); return; }
            var info = _streaming.CurrentInfo;
            if (info.DurationMs <= 0)
            {
                Log("[INFO] Video info not ready yet. Try again. 信息未就绪，稍后再试。");
                return;
            }
            MessageBox.Show(
                $"文件: {info.FileName}\r\n时长: {info.Duration}\r\n分辨率: {info.Resolution}\r\n编码: {info.Codec}\r\n音轨: {info.AudioTracks}\r\n字幕: {info.SubtitleTracks}",
                "Video Info 视频信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static string FormatTime(long ms)
        {
            var ts = TimeSpan.FromMilliseconds(ms);
            return ts.Hours > 0 ? ts.ToString(@"hh\:mm\:ss") : ts.ToString(@"mm\:ss");
        }

        void Log(string msg)
        {
            if (txtLog.Lines.Length > 500) txtLog.Clear();
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
        }

        void OnFormClosing_Handler(object sender, FormClosingEventArgs e)
        {
            _streaming?.Stop();
            _streaming?.Dispose();
            _config.Save("config.json");
        }
    }
}
