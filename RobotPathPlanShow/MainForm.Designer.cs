namespace RobotPathPlanShow
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DrawPanel = new System.Windows.Forms.Panel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动寻路模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止寻路ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空白ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.墙ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.机器人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.终止点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运动速度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.随机生成地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // DrawPanel
            // 
            this.DrawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawPanel.Location = new System.Drawing.Point(0, 28);
            this.DrawPanel.Name = "DrawPanel";
            this.DrawPanel.Size = new System.Drawing.Size(502, 322);
            this.DrawPanel.TabIndex = 1;
            this.DrawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.DrawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseMove);
            this.DrawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseUp);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(502, 25);
            this.MainMenu.TabIndex = 2;
            this.MainMenu.Text = "主菜单";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.模式ToolStripMenuItem,
            this.地形ToolStripMenuItem,
            this.运动速度ToolStripMenuItem,
            this.随机生成地图ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 模式ToolStripMenuItem
            // 
            this.模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自动寻路模式ToolStripMenuItem,
            this.停止寻路ToolStripMenuItem});
            this.模式ToolStripMenuItem.Name = "模式ToolStripMenuItem";
            this.模式ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.模式ToolStripMenuItem.Text = "模式";
            // 
            // 自动寻路模式ToolStripMenuItem
            // 
            this.自动寻路模式ToolStripMenuItem.Name = "自动寻路模式ToolStripMenuItem";
            this.自动寻路模式ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.自动寻路模式ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.自动寻路模式ToolStripMenuItem.Text = "自动寻路模式";
            this.自动寻路模式ToolStripMenuItem.Click += new System.EventHandler(this.自动寻路模式ToolStripMenuItem_Click);
            // 
            // 停止寻路ToolStripMenuItem
            // 
            this.停止寻路ToolStripMenuItem.Name = "停止寻路ToolStripMenuItem";
            this.停止寻路ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.停止寻路ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.停止寻路ToolStripMenuItem.Text = "停止寻路";
            this.停止寻路ToolStripMenuItem.Click += new System.EventHandler(this.停止寻路ToolStripMenuItem_Click);
            // 
            // 地形ToolStripMenuItem
            // 
            this.地形ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.空白ToolStripMenuItem,
            this.墙ToolStripMenuItem,
            this.机器人ToolStripMenuItem,
            this.终止点ToolStripMenuItem});
            this.地形ToolStripMenuItem.Name = "地形ToolStripMenuItem";
            this.地形ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.地形ToolStripMenuItem.Text = "地形";
            // 
            // 空白ToolStripMenuItem
            // 
            this.空白ToolStripMenuItem.Name = "空白ToolStripMenuItem";
            this.空白ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
            this.空白ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.空白ToolStripMenuItem.Text = "空白";
            this.空白ToolStripMenuItem.Click += new System.EventHandler(this.空白ToolStripMenuItem_Click);
            // 
            // 墙ToolStripMenuItem
            // 
            this.墙ToolStripMenuItem.Name = "墙ToolStripMenuItem";
            this.墙ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.墙ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.墙ToolStripMenuItem.Text = "墙";
            this.墙ToolStripMenuItem.Click += new System.EventHandler(this.墙ToolStripMenuItem_Click);
            // 
            // 机器人ToolStripMenuItem
            // 
            this.机器人ToolStripMenuItem.Name = "机器人ToolStripMenuItem";
            this.机器人ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
            this.机器人ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.机器人ToolStripMenuItem.Text = "机器人";
            this.机器人ToolStripMenuItem.Click += new System.EventHandler(this.机器人ToolStripMenuItem_Click);
            // 
            // 终止点ToolStripMenuItem
            // 
            this.终止点ToolStripMenuItem.Name = "终止点ToolStripMenuItem";
            this.终止点ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D4)));
            this.终止点ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.终止点ToolStripMenuItem.Text = "终止点";
            this.终止点ToolStripMenuItem.Click += new System.EventHandler(this.终止点ToolStripMenuItem_Click);
            // 
            // 运动速度ToolStripMenuItem
            // 
            this.运动速度ToolStripMenuItem.Name = "运动速度ToolStripMenuItem";
            this.运动速度ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.运动速度ToolStripMenuItem.Text = "运动速度...";
            this.运动速度ToolStripMenuItem.Click += new System.EventHandler(this.运动速度ToolStripMenuItem_Click);
            // 
            // 随机生成地图ToolStripMenuItem
            // 
            this.随机生成地图ToolStripMenuItem.Name = "随机生成地图ToolStripMenuItem";
            this.随机生成地图ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.随机生成地图ToolStripMenuItem.Text = "随机生成地图";
            this.随机生成地图ToolStripMenuItem.Click += new System.EventHandler(this.随机生成地图ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 350);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.DrawPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "机器人路径自动规划演示";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawPanel;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动寻路模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止寻路ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空白ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 墙ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 机器人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 终止点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运动速度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 随机生成地图ToolStripMenuItem;
    }
}

