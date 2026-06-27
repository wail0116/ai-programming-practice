using System;
using System.Collections.Generic;
using System.IO;

namespace video_player.Models
{
    public class MonitorConfig
    {
        public string LastDirectory { get; set; } = "";
        public bool DetectFreeze { get; set; } = false;
        public bool DetectBlackScreen { get; set; } = false;
        public bool DetectSceneChange { get; set; } = true;
        public int AnalysisIntervalSec { get; set; } = 5;
        public double FreezeThreshold { get; set; } = 0.95;
        public double BlackScreenThreshold { get; set; } = 10;
        public double SceneChangeThreshold { get; set; } = 0.30;
        public string ScreenshotDir { get; set; } = "Screenshots";
        public string LogFile { get; set; } = "player.log";
        public List<string> RecentFiles { get; set; } = new List<string>();

        public static MonitorConfig Load(string path)
        {
            var c = new MonitorConfig();
            if (!File.Exists(path)) return c;
            try
            {
                var dic = ParseSimpleJson(File.ReadAllText(path));
                string v;
                if (dic.TryGetValue("LastDirectory", out v)) c.LastDirectory = v;
                if (dic.TryGetValue("DetectFreeze", out v)) c.DetectFreeze = v == "true";
                if (dic.TryGetValue("DetectBlackScreen", out v)) c.DetectBlackScreen = v == "true";
                if (dic.TryGetValue("DetectSceneChange", out v)) c.DetectSceneChange = v == "true";
                int iv; double dv;
                if (dic.TryGetValue("AnalysisIntervalSec", out v) && int.TryParse(v, out iv)) c.AnalysisIntervalSec = iv;
                if (dic.TryGetValue("FreezeThreshold", out v) && double.TryParse(v, out dv)) c.FreezeThreshold = dv;
                if (dic.TryGetValue("BlackScreenThreshold", out v) && double.TryParse(v, out dv)) c.BlackScreenThreshold = dv;
                if (dic.TryGetValue("SceneChangeThreshold", out v) && double.TryParse(v, out dv)) c.SceneChangeThreshold = dv;
                if (dic.TryGetValue("ScreenshotDir", out v)) c.ScreenshotDir = v;
                if (dic.TryGetValue("LogFile", out v)) c.LogFile = v;
                string raw = File.ReadAllText(path);
                int arr = raw.IndexOf("\"RecentFiles\"");
                if (arr >= 0)
                {
                    int start = raw.IndexOf('[', arr);
                    int end = raw.IndexOf(']', start);
                    if (start >= 0 && end > start)
                    {
                        string inner = raw.Substring(start + 1, end - start - 1);
                        foreach (var item in inner.Split(','))
                        {
                            var f = item.Trim().Trim('"');
                            if (!string.IsNullOrEmpty(f) && File.Exists(f)) c.RecentFiles.Add(f);
                        }
                    }
                }
            }
            catch { }
            return c;
        }

        public void Save(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var lines = new List<string>();
            lines.Add("{");
            lines.Add(S("LastDirectory", LastDirectory));
            lines.Add(S("ScreenshotDir", ScreenshotDir));
            lines.Add(S("LogFile", LogFile));
            lines.Add(B("DetectFreeze", DetectFreeze));
            lines.Add(B("DetectBlackScreen", DetectBlackScreen));
            lines.Add(B("DetectSceneChange", DetectSceneChange));
            lines.Add(N("AnalysisIntervalSec", AnalysisIntervalSec));
            lines.Add(F("FreezeThreshold", FreezeThreshold));
            lines.Add(F("BlackScreenThreshold", BlackScreenThreshold));
            lines.Add(F("SceneChangeThreshold", SceneChangeThreshold));
            lines.Add("  \"RecentFiles\": [");
            for (int i = 0; i < RecentFiles.Count; i++)
                lines.Add("    \"" + RecentFiles[i].Replace("\\", "\\\\") + "\"" + (i < RecentFiles.Count - 1 ? "," : ""));
            lines.Add("  ]");
            lines.Add("}");
            File.WriteAllText(path, string.Join("\r\n", lines));
        }

        public void AddRecent(string filePath)
        {
            RecentFiles.RemoveAll(f => f == filePath);
            RecentFiles.Insert(0, filePath);
            if (RecentFiles.Count > 20) RecentFiles.RemoveAt(RecentFiles.Count - 1);
        }

        static string S(string k, string v) => "  \"" + k + "\": \"" + (v ?? "").Replace("\\", "\\\\") + "\",";
        static string B(string k, bool v) => "  \"" + k + "\": " + (v ? "true" : "false") + ",";
        static string N(string k, int v) => "  \"" + k + "\": " + v + ",";
        static string F(string k, double v) => "  \"" + k + "\": " + v + ",";

        static Dictionary<string, string> ParseSimpleJson(string json)
        {
            var dic = new Dictionary<string, string>();
            var reader = new StringReader(json);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var t = line.Trim();
                if (t.Length < 5 || t == "{" || t == "}" || t.TrimStart().StartsWith("[")) continue;
                int colon = t.IndexOf(':');
                if (colon < 0) continue;
                string key = t.Substring(0, colon).Trim().Trim('"');
                string val = t.Substring(colon + 1).Trim().TrimEnd(',').Trim('"');
                if (!string.IsNullOrEmpty(key)) dic[key] = val;
            }
            return dic;
        }
    }
}
