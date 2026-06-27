using System.Drawing;
using System.Windows.Forms;

namespace video_player
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelSidebar;
        private Label lblSideTitle;
        private ListBox lstPlaylist;
        private Button btnAddFiles;
        private Button btnRemove;
        private Button btnClearAll;
        private ComboBox cmbPlayMode;
        private Label lblSpeed;
        private ComboBox cmbSpeed;
        private Label lblVolume;
        private TrackBar tbarVolume;
        private Label lblVolVal;
        private Button btnScreenshot;
        private Button btnInfo;
        private Panel panelVideo;
        private Panel panelInfoOverlay;
        private Label lblInfoTitle;
        private Label lblInfoDetail;
        private Panel panelControls;
        private TrackBar tbarProgress;
        private Label lblTimeCur;
        private Label lblTimeTotal;
        private Button btnPrev;
        private Button btnPlayPause;
        private Button btnStop;
        private Button btnNext;
        private TextBox txtLog;
        private Panel panelStatusBar;
        private Label lblState;
        private Label lblAnomalyCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnScreenshot = new System.Windows.Forms.Button();
            this.lblVolVal = new System.Windows.Forms.Label();
            this.tbarVolume = new System.Windows.Forms.TrackBar();
            this.lblVolume = new System.Windows.Forms.Label();
            this.cmbSpeed = new System.Windows.Forms.ComboBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.cmbPlayMode = new System.Windows.Forms.ComboBox();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.lstPlaylist = new System.Windows.Forms.ListBox();
            this.lblSideTitle = new System.Windows.Forms.Label();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.panelInfoOverlay = new System.Windows.Forms.Panel();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.lblInfoDetail = new System.Windows.Forms.Label();
            this.panelControls = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.lblTimeTotal = new System.Windows.Forms.Label();
            this.lblTimeCur = new System.Windows.Forms.Label();
            this.tbarProgress = new System.Windows.Forms.TrackBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblState = new System.Windows.Forms.Label();
            this.lblAnomalyCount = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).BeginInit();
            this.panelInfoOverlay.SuspendLayout();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarProgress)).BeginInit();
            this.panelStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.panelSidebar.Controls.Add(this.btnInfo);
            this.panelSidebar.Controls.Add(this.btnScreenshot);
            this.panelSidebar.Controls.Add(this.lblVolVal);
            this.panelSidebar.Controls.Add(this.tbarVolume);
            this.panelSidebar.Controls.Add(this.lblVolume);
            this.panelSidebar.Controls.Add(this.cmbSpeed);
            this.panelSidebar.Controls.Add(this.lblSpeed);
            this.panelSidebar.Controls.Add(this.cmbPlayMode);
            this.panelSidebar.Controls.Add(this.btnClearAll);
            this.panelSidebar.Controls.Add(this.btnRemove);
            this.panelSidebar.Controls.Add(this.btnAddFiles);
            this.panelSidebar.Controls.Add(this.lstPlaylist);
            this.panelSidebar.Controls.Add(this.lblSideTitle);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(280, 750);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(80)))), ((int)(((byte)(223)))));
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInfo.ForeColor = System.Drawing.Color.White;
            this.btnInfo.Location = new System.Drawing.Point(12, 544);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(256, 36);
            this.btnInfo.TabIndex = 12;
            this.btnInfo.Text = "Video Info 视频信息";
            this.btnInfo.UseVisualStyleBackColor = false;
            // 
            // btnScreenshot
            // 
            this.btnScreenshot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(161)))), ((int)(((byte)(214)))));
            this.btnScreenshot.FlatAppearance.BorderSize = 0;
            this.btnScreenshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScreenshot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnScreenshot.ForeColor = System.Drawing.Color.White;
            this.btnScreenshot.Location = new System.Drawing.Point(12, 496);
            this.btnScreenshot.Name = "btnScreenshot";
            this.btnScreenshot.Size = new System.Drawing.Size(256, 36);
            this.btnScreenshot.TabIndex = 11;
            this.btnScreenshot.Text = "Screenshot 截图";
            this.btnScreenshot.UseVisualStyleBackColor = false;
            // 
            // lblVolVal
            // 
            this.lblVolVal.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblVolVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.lblVolVal.Location = new System.Drawing.Point(218, 458);
            this.lblVolVal.Name = "lblVolVal";
            this.lblVolVal.Size = new System.Drawing.Size(50, 20);
            this.lblVolVal.TabIndex = 10;
            this.lblVolVal.Text = "100%";
            this.lblVolVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbarVolume
            // 
            this.tbarVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.tbarVolume.Location = new System.Drawing.Point(12, 458);
            this.tbarVolume.Maximum = 200;
            this.tbarVolume.Name = "tbarVolume";
            this.tbarVolume.Size = new System.Drawing.Size(200, 69);
            this.tbarVolume.TabIndex = 9;
            this.tbarVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbarVolume.Value = 100;
            // 
            // lblVolume
            // 
            this.lblVolume.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.lblVolume.Location = new System.Drawing.Point(12, 438);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(120, 20);
            this.lblVolume.TabIndex = 8;
            this.lblVolume.Text = "Volume 音量";
            // 
            // cmbSpeed
            // 
            this.cmbSpeed.BackColor = System.Drawing.Color.White;
            this.cmbSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpeed.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmbSpeed.FormattingEnabled = true;
            this.cmbSpeed.Items.AddRange(new object[] {
            "0.25x",
            "0.5x",
            "0.75x",
            "1x (Normal)",
            "1.25x",
            "1.5x",
            "2x"});
            this.cmbSpeed.Location = new System.Drawing.Point(138, 400);
            this.cmbSpeed.Name = "cmbSpeed";
            this.cmbSpeed.Size = new System.Drawing.Size(130, 33);
            this.cmbSpeed.TabIndex = 7;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.lblSpeed.Location = new System.Drawing.Point(12, 404);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(120, 20);
            this.lblSpeed.TabIndex = 6;
            this.lblSpeed.Text = "Speed 速度";
            // 
            // cmbPlayMode
            // 
            this.cmbPlayMode.BackColor = System.Drawing.Color.White;
            this.cmbPlayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlayMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPlayMode.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cmbPlayMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmbPlayMode.FormattingEnabled = true;
            this.cmbPlayMode.Items.AddRange(new object[] {
            "Sequential 顺序",
            "Shuffle 随机",
            "Repeat One 单曲循环",
            "Repeat All 列表循环"});
            this.cmbPlayMode.Location = new System.Drawing.Point(12, 366);
            this.cmbPlayMode.Name = "cmbPlayMode";
            this.cmbPlayMode.Size = new System.Drawing.Size(256, 31);
            this.cmbPlayMode.TabIndex = 5;
            // 
            // btnClearAll
            // 
            this.btnClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(185)))));
            this.btnClearAll.FlatAppearance.BorderSize = 0;
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearAll.ForeColor = System.Drawing.Color.White;
            this.btnClearAll.Location = new System.Drawing.Point(199, 320);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(69, 32);
            this.btnClearAll.TabIndex = 4;
            this.btnClearAll.Text = "Clear ";
            this.btnClearAll.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(101, 320);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(101, 32);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove ";
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(164)))), ((int)(((byte)(79)))));
            this.btnAddFiles.FlatAppearance.BorderSize = 0;
            this.btnAddFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddFiles.ForeColor = System.Drawing.Color.White;
            this.btnAddFiles.Location = new System.Drawing.Point(12, 320);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(83, 32);
            this.btnAddFiles.TabIndex = 2;
            this.btnAddFiles.Text = "+ Add Files 添加";
            this.btnAddFiles.UseVisualStyleBackColor = false;
            // 
            // lstPlaylist
            // 
            this.lstPlaylist.BackColor = System.Drawing.Color.White;
            this.lstPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPlaylist.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstPlaylist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.lstPlaylist.FormattingEnabled = true;
            this.lstPlaylist.ItemHeight = 25;
            this.lstPlaylist.Location = new System.Drawing.Point(12, 50);
            this.lstPlaylist.Name = "lstPlaylist";
            this.lstPlaylist.Size = new System.Drawing.Size(256, 252);
            this.lstPlaylist.TabIndex = 1;
            // 
            // lblSideTitle
            // 
            this.lblSideTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSideTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.lblSideTitle.Location = new System.Drawing.Point(12, 12);
            this.lblSideTitle.Name = "lblSideTitle";
            this.lblSideTitle.Size = new System.Drawing.Size(256, 30);
            this.lblSideTitle.TabIndex = 0;
            this.lblSideTitle.Text = "Video Player Pro ";
            // 
            // panelVideo
            // 
            this.panelVideo.BackColor = System.Drawing.Color.Black;
            this.panelVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVideo.Location = new System.Drawing.Point(280, 0);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(920, 554);
            this.panelVideo.TabIndex = 1;
            // 
            // panelInfoOverlay
            // 
            this.panelInfoOverlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelInfoOverlay.Controls.Add(this.lblInfoTitle);
            this.panelInfoOverlay.Controls.Add(this.lblInfoDetail);
            this.panelInfoOverlay.Location = new System.Drawing.Point(300, 20);
            this.panelInfoOverlay.Name = "panelInfoOverlay";
            this.panelInfoOverlay.Size = new System.Drawing.Size(440, 110);
            this.panelInfoOverlay.TabIndex = 2;
            this.panelInfoOverlay.Visible = false;
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblInfoTitle.ForeColor = System.Drawing.Color.White;
            this.lblInfoTitle.Location = new System.Drawing.Point(14, 12);
            this.lblInfoTitle.Name = "lblInfoTitle";
            this.lblInfoTitle.Size = new System.Drawing.Size(412, 26);
            this.lblInfoTitle.TabIndex = 0;
            // 
            // lblInfoDetail
            // 
            this.lblInfoDetail.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.lblInfoDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.lblInfoDetail.Location = new System.Drawing.Point(14, 40);
            this.lblInfoDetail.Name = "lblInfoDetail";
            this.lblInfoDetail.Size = new System.Drawing.Size(412, 62);
            this.lblInfoDetail.TabIndex = 1;
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControls.Controls.Add(this.btnNext);
            this.panelControls.Controls.Add(this.btnStop);
            this.panelControls.Controls.Add(this.btnPlayPause);
            this.panelControls.Controls.Add(this.btnPrev);
            this.panelControls.Controls.Add(this.lblTimeTotal);
            this.panelControls.Controls.Add(this.lblTimeCur);
            this.panelControls.Controls.Add(this.tbarProgress);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(280, 554);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(920, 70);
            this.panelControls.TabIndex = 3;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(240)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.btnNext.Location = new System.Drawing.Point(367, 38);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(92, 30);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "►|下一个";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(281, 38);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(71, 30);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "■停止";
            this.btnStop.UseVisualStyleBackColor = false;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(102)))), ((int)(((byte)(214)))));
            this.btnPlayPause.FlatAppearance.BorderSize = 0;
            this.btnPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayPause.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnPlayPause.ForeColor = System.Drawing.Color.White;
            this.btnPlayPause.Location = new System.Drawing.Point(188, 35);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(87, 33);
            this.btnPlayPause.TabIndex = 4;
            this.btnPlayPause.Text = "▶ 播放";
            this.btnPlayPause.UseVisualStyleBackColor = false;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(240)))));
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.btnPrev.Location = new System.Drawing.Point(80, 37);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(102, 30);
            this.btnPrev.TabIndex = 3;
            this.btnPrev.Text = "|◄上一个";
            this.btnPrev.UseVisualStyleBackColor = false;
            // 
            // lblTimeTotal
            // 
            this.lblTimeTotal.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblTimeTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(150)))));
            this.lblTimeTotal.Location = new System.Drawing.Point(845, 38);
            this.lblTimeTotal.Name = "lblTimeTotal";
            this.lblTimeTotal.Size = new System.Drawing.Size(65, 22);
            this.lblTimeTotal.TabIndex = 2;
            this.lblTimeTotal.Text = "00:00";
            this.lblTimeTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTimeCur
            // 
            this.lblTimeCur.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblTimeCur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.lblTimeCur.Location = new System.Drawing.Point(14, 38);
            this.lblTimeCur.Name = "lblTimeCur";
            this.lblTimeCur.Size = new System.Drawing.Size(60, 22);
            this.lblTimeCur.TabIndex = 1;
            this.lblTimeCur.Text = "00:00";
            // 
            // tbarProgress
            // 
            this.tbarProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tbarProgress.Location = new System.Drawing.Point(10, 5);
            this.tbarProgress.Maximum = 1000;
            this.tbarProgress.Name = "tbarProgress";
            this.tbarProgress.Size = new System.Drawing.Size(900, 69);
            this.tbarProgress.TabIndex = 0;
            this.tbarProgress.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(75)))));
            this.txtLog.Location = new System.Drawing.Point(280, 624);
            this.txtLog.Margin = new System.Windows.Forms.Padding(8);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(920, 100);
            this.txtLog.TabIndex = 4;
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.panelStatusBar.Controls.Add(this.lblState);
            this.panelStatusBar.Controls.Add(this.lblAnomalyCount);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(280, 724);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Size = new System.Drawing.Size(920, 26);
            this.panelStatusBar.TabIndex = 5;
            // 
            // lblState
            // 
            this.lblState.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.lblState.Location = new System.Drawing.Point(10, 4);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(700, 18);
            this.lblState.TabIndex = 0;
            this.lblState.Text = "Ready 就绪";
            // 
            // lblAnomalyCount
            // 
            this.lblAnomalyCount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAnomalyCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(120)))), ((int)(((byte)(20)))));
            this.lblAnomalyCount.Location = new System.Drawing.Point(730, 4);
            this.lblAnomalyCount.Name = "lblAnomalyCount";
            this.lblAnomalyCount.Size = new System.Drawing.Size(180, 18);
            this.lblAnomalyCount.TabIndex = 1;
            this.lblAnomalyCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.panelVideo);
            this.Controls.Add(this.panelInfoOverlay);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(960, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Player Pro 视频播放器";
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).EndInit();
            this.panelInfoOverlay.ResumeLayout(false);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarProgress)).EndInit();
            this.panelStatusBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
