# Video Player Pro — 多路视频播放器

## 1. 项目概要

**项目名称:** Video Player Pro  
**类型:** Windows Forms 桌面应用程序  
**技术栈:** .NET Framework 4.8 / WinForm / LibVLCSharp 3.9.7 / GDI+  
**开发模式:** AI辅助编程 (Claude Code)  
**源项目演进:** StreamMonitor（直播监控） → 本地视频播放器

### 项目目标
构建一个全功能桌面视频播放器，支持播放列表管理、多模式播放、倍速/音量/画面调整、截图保存和视频元信息提取。基于 LibVLCSharp 封装 VLC 原生播放能力，通过 WinForm 提供完整的桌面交互体验。

## 2. 功能范围

| 模块 | 功能 | 涉及技术 |
|------|------|---------|
| 播放控制 | 播放/暂停/停止/上下一曲、进度拖拽、250ms定时刷新 | WinForm Timer, async Invoke |
| 播放列表 | 多文件添加、去重、删除、清空、历史恢复(最近20个) | WinForm ListBox, 文件对话框 |
| 4种播放模式 | 顺序/随机/单曲循环/列表循环 | LINQ OrderBy, Fisher-Yates洗牌 |
| 倍速播放 | 0.25x ~ 2x 七档速度 | LibVLCSharp SetRate |
| 音量控制 | 0~200% TrackBar 实时调节 | LibVLCSharp Volume |
| 画面调整 | 亮度/对比度/饱和度/伽马 4轨独立调节 | VLC VideoAdjustOption API |
| 截图保存 | 当前帧 → PNG，命名含视频名+时间戳 | VLC TakeSnapshot |
| 视频信息 | 分辨率/编码/时长/音轨数/字幕数 弹窗显示 | VLC MediaTrack 遍历 + FourCC 解析 |
| 配置持久化 | JSON配置文件读写，最近打开记录 | 手写JSON解析器 (无第三方库) |

## 3. 技术选型理由

**为什么选 LibVLCSharp?**
- 课程"多媒体处理"技术要求使用第三方播放引擎。LibVLCSharp 是 VLC 的官方 .NET 绑定，支持 200+ 视频格式，API 成熟。
- 与课程 `WindowsFormsApp3` 项目中已掌握的技术栈一致（NAudio/WMP/LibVLC 三引擎对比）。

**为什么手写 JSON 解析?**
- 避免 Newtonsoft.Json 第三方依赖，保持项目最小化。
- GitHub API 项目中已验证了 `IndexOf` + `Substring` 手写解析的可行性，本项目的 config.json 结构简单（16个键值对+数组），手工解析足够。

**为什么用事件驱动架构?**
- StreamingService 与 Form1 通过 `StateChanged`/`TimeChanged`/`OnPlaybackEnded`/`InfoReady` 四个事件解耦。
- UI 线程安全通过 `this.Invoke(new Action(...))` 保证。

## 4. 系统架构

```
video player.sln
└── video player/
    ├── Program.cs                     // 入口点
    ├── Form1.cs                       // UI协调层 (389行)
    ├── Form1.Designer.cs              // 设计器布局 (506行)
    ├── Models/
    │   └── MonitorConfig.cs           // 配置模型 + JSON读写 (115行)
    └── Services/
        └── StreamingService.cs        // LibVLC封装 (225行)
```

### 架构模式: 服务分层

```
┌──────────────┐      ┌──────────────────┐
│  Form1.cs     │─────>│ StreamingService │
│  (UI Layer)   │      │   (Service)      │
│  389 lines    │      │   225 lines      │
└──────┬───────┘      └────────┬─────────┘
       │                       │
       │ 事件回调:              │ 引擎调用:
       │ StateChanged          │ Core.Initialize
       │ TimeChanged            │ MediaPlayer.Play
       │ OnPlaybackEnded       │ SetAdjustFloat
       │ InfoReady              │ TakeSnapshot
       │                       │
       └── MonitorConfig       └── LibVLCSharp 3.9.7
           (JSON Config)            (VLC Native)
```

### 数据流
1. 用户添加文件 → `OpenFileDialog` 多选 → 去重加入 `_playlist`
2. 选择/双击列表项 → `PlayCurrent()` → `StreamingService.Play(filePath)`
3. `MediaPlayer.Play()` → 250ms定时器 `TimeChanged` 事件 → UI 更新进度条/时间
4. 播放结束 → `EndReached` 事件 → `OnPlaybackEnded` → `PlayNext()` 根据模式切歌
5. 画面调整滑块 → `SetAdjustFloat` → VLC 实时滤镜 → 立刻生效

### 事件流转图
```
StreamingService ──StateChanged──┐
                    ──TimeChanged──┼── Form1.Invoke ── UI更新
                    ──InfoReady────┤
                    ──PlaybackEnded┘
```

## 5. 核心模块说明

### StreamingService (多媒体处理)
- 封装 LibVLCSharp 播放引擎，提供统一的 Play/Pause/Resume/Stop/TakeSnapshot API
- 音视频属性控制: Position(进度)/Volume(音量)/Rate(倍速)/Brightness/Contrast/Saturation/Gamma
- 自动发现 VLC 运行时: 先查打包的 libvlc 目录, 再查系统 VLC 安装路径
- 异步提取视频元数据: 500ms延迟后遍历 MediaTrack 数组, 解析 FourCC 编码标识

### Form1 (UI + 协调)
- 39 个 WinForm 控件, 亮色主题 (Segoe UI + Consolas)
- 三区 Dock 布局: 左侧边栏(280px) + 中央视频区(Fill) + 底部控制栏(70px) + 日志(100px)
- 暂停状态通过 `_isPaused` 标志独立管理, 避免 LibVLCSharp `IsPlaying` 对暂停态的误判
- 画面调整控件通过代码动态创建, 不污染设计器文件

### MonitorConfig (文件处理)
- 零依赖 JSON 读写: 逐行解析键值对, 手动构建 JSON 字符串
- 最近文件记录: 去重 + FIFO 上限20条, 启动时自动恢复播放列表

## 6. 项目Git仓库

```
https://github.com/wail0116/ai-programming-practice.git
```
