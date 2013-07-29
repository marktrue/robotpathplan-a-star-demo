namespace RobotPathPlanShow
{
    partial class About_frm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.About_lbl = new System.Windows.Forms.Label();
            this.OK_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // About_lbl
            // 
            this.About_lbl.AutoSize = true;
            this.About_lbl.Location = new System.Drawing.Point(38, 34);
            this.About_lbl.Name = "About_lbl";
            this.About_lbl.Size = new System.Drawing.Size(233, 36);
            this.About_lbl.TabIndex = 0;
            this.About_lbl.Text = "说明: 这是一个A*算法自动寻路的演示程序\r\n作者: 卢肖\r\nEmail: ahu.marktrue@gmail.com";
            // 
            // OK_btn
            // 
            this.OK_btn.Location = new System.Drawing.Point(112, 96);
            this.OK_btn.Name = "OK_btn";
            this.OK_btn.Size = new System.Drawing.Size(75, 23);
            this.OK_btn.TabIndex = 1;
            this.OK_btn.Text = "确定";
            this.OK_btn.UseVisualStyleBackColor = true;
            this.OK_btn.Click += new System.EventHandler(this.OK_btn_Click);
            // 
            // About_frm
            // 
            this.AcceptButton = this.OK_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 134);
            this.Controls.Add(this.OK_btn);
            this.Controls.Add(this.About_lbl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(305, 168);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(305, 168);
            this.Name = "About_frm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label About_lbl;
        private System.Windows.Forms.Button OK_btn;
    }
}