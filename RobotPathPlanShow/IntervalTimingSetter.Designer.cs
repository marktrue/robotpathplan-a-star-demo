namespace RobotPathPlanShow
{
    partial class IntervalTimingSetter
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
            this.OK_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.Interval_txt = new System.Windows.Forms.TextBox();
            this.Interval_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OK_btn
            // 
            this.OK_btn.Location = new System.Drawing.Point(27, 79);
            this.OK_btn.Name = "OK_btn";
            this.OK_btn.Size = new System.Drawing.Size(75, 23);
            this.OK_btn.TabIndex = 0;
            this.OK_btn.Text = "确定";
            this.OK_btn.UseVisualStyleBackColor = true;
            this.OK_btn.Click += new System.EventHandler(this.OK_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Location = new System.Drawing.Point(119, 79);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.Cancel_btn.TabIndex = 1;
            this.Cancel_btn.Text = "取消";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // Interval_txt
            // 
            this.Interval_txt.Location = new System.Drawing.Point(119, 29);
            this.Interval_txt.Name = "Interval_txt";
            this.Interval_txt.Size = new System.Drawing.Size(75, 21);
            this.Interval_txt.TabIndex = 2;
            // 
            // Interval_lbl
            // 
            this.Interval_lbl.AutoSize = true;
            this.Interval_lbl.Location = new System.Drawing.Point(25, 32);
            this.Interval_lbl.Name = "Interval_lbl";
            this.Interval_lbl.Size = new System.Drawing.Size(83, 12);
            this.Interval_lbl.TabIndex = 3;
            this.Interval_lbl.Text = "反应时间(ms):";
            // 
            // IntervalTimingSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 124);
            this.Controls.Add(this.Interval_lbl);
            this.Controls.Add(this.Interval_txt);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.OK_btn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntervalTimingSetter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置反应时间";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.TextBox Interval_txt;
        private System.Windows.Forms.Label Interval_lbl;
    }
}