# Video Player Pro — 视频播放器

## 1. 项目概要

**项目名称:** Video Player Pro
**类型:** Windows Forms 桌面应用程序
**技术栈:** .NET Framework 4.8 / WinForm / LibVLCSharp 3.9.7
**开发模式:** AI辅助编程 (Claude Code)
**演进路径:** StreamMonitor（直播监控） → 本地视频播放器

### 项目目标
基于 LibVLCSharp 封装 VLC 播放引擎，构建一个支持播放列表管理、多种播放模式、画面实时调整和视频元信息提取的桌面播放器。

## 2. 功能范围

| 模块 | 功能 | 涉及技术 |
|------|------|---------|
| 播放控制 | 播放/暂停/停止/上下曲、进度拖拽、250ms定时刷新 | WinForm Timer, async Invoke |
| 播放列表 | 多文件添加、去重、删除、清空、历史恢复(最近20个) | WinForm ListBox, OpenFileDialog |
| 4种播放模式 | 顺序 / 随机 / 单曲循环 / 列表循环 | LINQ, Fisher-Yates洗牌 |
| 倍速播放 | 0.25x ~ 2x 七档速度 | LibVLCSharp SetRate |
| 音量控制 | 0~200% TrackBar 实时调节 | LibVLCSharp Volume |
| 画面调整 | 亮度/对比度/饱和度/伽马 4轨独立调节 | VLC VideoAdjustOption |
| 截图保存 | 当前帧 → PNG，含视频名+时间戳 | VLC TakeSnapshot |
| 视频信息 | 分辨率/编码/时长/音轨/字幕 弹窗显示 | VLC MediaTrack + FourCC 解析 |
| 配置持久化 | JSON配置文件读写，最近打开记录 | 手写JSON解析器(零依赖) |

## 3. 技术选型理由

**LibVLCSharp** — 课程"多媒体处理"要求的第三方播放引擎。VLC 官方 .NET 绑定，支持 200+ 格式。与 `WindowsFormsApp3` 项目中已掌握的 NAudio/WMP/LibVLC 三引擎对比经验一致。

**手写 JSON 解析** — 避免 Newtonsoft.Json 依赖，保持项目最小化。GitHub API 项目中已验证了 `IndexOf`+`Substring` 手写解析的可行性，config.json 仅 16 个键值对，无需引入第三方库。

**事件驱动** — StreamingService 与 Form1 通过 `StateChanged`/`TimeChanged`/`OnPlaybackEnded`/`InfoReady` 四个事件解耦，UI 线程安全用 `this.Invoke` 保证。

## 4. 系统架构

```
video player.sln
└── video player/
    ├── Program.cs
    ├── Form1.cs / Form1.Designer.cs     (UI层 + 协调逻辑)
    ├── Models/MonitorConfig.cs           (配置模型 + JSON读写)
    └── Services/StreamingService.cs      (LibVLC播放引擎封装)
```

```
Form1 (UI) ──事件──> StreamingService (Service) ──API──> LibVLCSharp ──> VLC Native
    │                      │
    └── MonitorConfig      └── VideoInfo (元数据模型)
        (JSON file)
```

### 数据流
1. 用户添加文件 → `OpenFileDialog` 多选 → 去重入 `_playlist`
2. 选择列表项 → `PlayCurrent()` → `StreamingService.Play(filePath)`
3. 250ms 定时器 `TimeChanged` 事件 → UI 更新进度条和时间
4. 播放结束 `EndReached` → `PlayNext()` 根据当前模式切歌
5. 画面调整滑块 → `SetAdjustFloat()` → VLC 实时滤镜生效

## 5. 核心模块说明

### StreamingService (多媒体处理)
封装 LibVLCSharp 提供统一的 `Play/Pause/Resume/Stop/TakeSnapshot` API。暴露 `Position/Volume/Rate/Brightness/Contrast/Saturation/Gamma` 可读写属性。VLC 运行时路径自动发现（先查打包的 libvlc，再查系统安装路径）。视频元数据通过 500ms 延迟后遍历 `MediaTrack[]` 异步提取，FourCC 编码标识模糊匹配大小写和别名。

### Form1 (UI + 协调)
39 个 WinForm 控件，亮色主题（Segoe UI + Consolas）。Dock 布局：左侧边栏(280px) + 中央视频区(Fill) + 底部控制栏(70px) + 日志(100px)。暂停状态用独立 `_isPaused` 标志管理，避免 LibVLCSharp `IsPlaying` 在暂停时返回 false 导致的逻辑错误。画面调整控件通过代码动态创建，不污染设计器文件。

### MonitorConfig (文件处理)
零依赖 JSON 读写：逐行解析键值对，手动序列化。最近文件记录支持去重 + FIFO(上限20条) + 启动时自动恢复列表 + 已删除文件过滤。

## 6. 项目Git仓库

```
https://github.com/wail0116/ai-programming-practice.git
```
