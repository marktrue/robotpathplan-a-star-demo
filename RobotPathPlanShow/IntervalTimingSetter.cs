using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RobotPathPlanShow
{
    public partial class IntervalTimingSetter : Form
    {
        public IntervalTimingSetter(int nInterval)
        {
            InitializeComponent();
            Interval_txt.Text = nInterval.ToString();
        }

        private void OK_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public int getInterval()
        {
            return System.Int32.Parse(Interval_txt.Text);
        }

    }
}
