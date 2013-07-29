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
    enum BlockType
    {
        Blank = 0,
        Wall = 'o',
        Robot = 's',
        Goal = 'g',
        Path = 'p',
        LastPath = 'q'
    }

    enum State
    {
        Run = 0,
        Edit = 1
    }

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeMap();
            InitializeShow();
        }

        private const int m_nMaxBlocks = 1024;
        private const int m_nBlockPixel = 5;
        private const int m_nCnstWidth = 100;
        private const int m_nCnstHeight = 100;
        private const int m_nTimeInterval = 100;

        private Bitmap m_bmpDrawingSpace;
        private int m_nWidth;
        private int m_nHeight;

        // ms
        private State m_enumState;
        private BlockType m_enumCurrentBlock;
        private Point m_pntRobot;
        private Point m_pntEndPoint;

        private int[] m_aPath;
        //private int m_nPos;
        private int m_nPathLen;
        private System.Windows.Forms.Timer m_oTimer;

        private bool m_nMouseDown;

        private void InitializeShow()
        {
            m_oTimer = new Timer();
            m_oTimer.Interval = m_nTimeInterval;
            m_oTimer.Tick += new EventHandler(m_oTimer_Tick);
            m_oTimer.Stop();
            //m_nPos = 0;
            ChangeState(State.Edit);
            ChangeCurrentBlock(BlockType.Wall);
            m_nMouseDown = false;
            m_aPath = null;
            m_nPathLen = 0;
        }

        private void InitializeMap()
        {
            byte[] bmap = null;
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;
            //
            m_nWidth = m_nCnstWidth;
            m_nHeight = m_nCnstHeight;
            //
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            //
            m_bmpDrawingSpace = new Bitmap(m_nWidth * m_nBlockPixel, m_nHeight * m_nBlockPixel);
            //
            if (DllAPI.RobotCreateEmptyMap(m_nWidth, m_nHeight, ref startX, ref startY, ref endX, ref endY) != 0)
            {
                return;
            }
            m_pntRobot.X = startX;
            m_pntRobot.Y = startY;
            m_pntEndPoint.X = endX;
            m_pntEndPoint.Y = endY;
            if (DllAPI.getMap(ref bmap) != 0)
            {
                return;
            }
            RePaint(bmap);
            ReSizeForm();
        }

        private void RePaint(byte[] bmap)
        {
            for (int i = 0; i < m_nWidth; ++i)
            {
                for (int j = 0; j < m_nHeight; ++j)
                {
                    RePaint(i, j, bmap[i + j * m_nWidth]);
                }
            }
            RePaint(m_pntRobot.X, m_pntRobot.Y, (byte)BlockType.Robot);
            RePaint(m_pntEndPoint.X, m_pntEndPoint.Y, (byte)BlockType.Goal);
        }
        
        private void RePaint(int x, int y, byte data)
        {
            if (x >= m_nWidth || x < 0 || y >m_nHeight || y < 0)
            {
                throw new Exception("x " + x + ",y " + y);
            }
            Color pntColor = Color.Yellow;  //error color
            if (data == (byte)BlockType.Blank)
            {
                pntColor = Color.White;
            }
            else if (data == (byte)BlockType.Wall)
            {
                pntColor = Color.Black;
            }
            else if (data == (byte)BlockType.Robot)
            {
                pntColor = Color.Red;
            }
            else if (data == (byte)BlockType.Goal)
            {
                pntColor = Color.Orange;
            }
            else if (data == (byte)BlockType.Path)
            {
                pntColor = Color.Pink;
            }
            else if (data == (byte)BlockType.LastPath)
            {
                pntColor = Color.Gray;
            }
            Graphics g = Graphics.FromImage(this.m_bmpDrawingSpace);
            SolidBrush slidbrush = new SolidBrush(pntColor);
            g.FillRectangle(slidbrush, x * m_nBlockPixel, y * m_nBlockPixel, m_nBlockPixel, m_nBlockPixel);
            g.Dispose();
            slidbrush.Dispose();
        }

        private void UpdateBlock(int x, int y, BlockType data)
        {
            byte _data = 0;
            if (x < 0 || x >= m_nWidth || y < 0 || y >= m_nHeight)
            {
                return;
            }
            DllAPI.ReadMapByte(x, y, ref _data);
            if (data == BlockType.Robot)
            {
                if (x == m_pntEndPoint.X && y == m_pntEndPoint.Y || _data != (byte)BlockType.Blank)
                {
                    return;
                }
                RePaint(m_pntRobot.X, m_pntRobot.Y, (byte)BlockType.Blank);
                m_pntRobot.X = x;
                m_pntRobot.Y = y;
                RePaint(x, y, (byte)BlockType.Robot);
                DllAPI.setRobotPoint(x, y);
            }
            else if (data == BlockType.Goal)
            {
                if (x == m_pntRobot.X && y == m_pntRobot.Y || _data != (byte)BlockType.Blank)
                {
                    return;
                }
                RePaint(m_pntEndPoint.X, m_pntEndPoint.Y, (byte)BlockType.Blank);
                m_pntEndPoint.X = x;
                m_pntEndPoint.Y = y;
                RePaint(x, y, (byte)BlockType.Goal);
                DllAPI.setEndPoint(x, y);
            }
            else if (data == BlockType.Blank)
            {
                if (x == m_pntRobot.X && y == m_pntRobot.Y || x == m_pntEndPoint.X && y == m_pntEndPoint.Y)
                {
                    return;
                }
                RePaint(x, y, (byte)BlockType.Blank);
                DllAPI.UpdateMap(x, y, (byte)data);
            }
            else if (data == BlockType.Wall)
            {
                if (x == m_pntRobot.X && y == m_pntRobot.Y || x == m_pntEndPoint.X && y == m_pntEndPoint.Y)
                {
                    return;
                }
                RePaint(x, y, (byte)BlockType.Wall);
                DllAPI.UpdateMap(x, y, (byte)BlockType.Wall);
            }
            this.Invalidate();
            this.Update();
        }

        private void ReFindPath()
        {
            //m_aPath = null;
            //m_nPathLen = 0;
            //m_nPos = 0;
            //清除前面的路径标记
            for (int i = 0; i < m_nPathLen - 2; i += 2)
            {
                byte _data = 0;
                DllAPI.ReadMapByte(m_aPath[i], m_aPath[i + 1], ref _data);
                if (_data == (byte)BlockType.Blank)
                {
                    RePaint(m_aPath[i], m_aPath[i + 1], (byte)BlockType.Blank);
                }
            }
            //
            DllAPI.setRobotPoint(m_pntRobot.X, m_pntRobot.Y);
            DllAPI.setEndPoint(m_pntEndPoint.X, m_pntEndPoint.Y);
            if (DllAPI.RobotPathPlan(ref m_aPath, ref m_nPathLen) != 0)
            {
                return;
            }
            //显示路径标记
            for (int i = 0; i < m_nPathLen - 2; i += 2 )
            {
                RePaint(m_aPath[i], m_aPath[i + 1], (byte)BlockType.Path);
            }
            this.Invalidate();
            this.Update();
        }

        private void ChangeState(State s)
        {
            m_enumState = s;
            switch (s)
            {
                case State.Edit:
                    m_oTimer.Stop();
                    this.自动寻路模式ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.停止寻路ToolStripMenuItem.CheckState = CheckState.Checked;
                    break;
                case State.Run:
                    this.自动寻路模式ToolStripMenuItem.CheckState = CheckState.Checked;
                    this.停止寻路ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    break;
                default:
                    break;
            }
        }

        private void ChangeCurrentBlock(BlockType blk)
        {
            m_enumCurrentBlock = blk;
            switch (blk)
            {
                case BlockType.Blank:
                    this.空白ToolStripMenuItem.CheckState = CheckState.Checked;
                    this.机器人ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.墙ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.终止点ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    break;
                case BlockType.Wall:
                    this.空白ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.机器人ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.墙ToolStripMenuItem.CheckState = CheckState.Checked;
                    this.终止点ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    break;
                case BlockType.Robot:
                    this.空白ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.机器人ToolStripMenuItem.CheckState = CheckState.Checked;
                    this.墙ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.终止点ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    break;
                case BlockType.Goal:
                    this.空白ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.机器人ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.墙ToolStripMenuItem.CheckState = CheckState.Unchecked;
                    this.终止点ToolStripMenuItem.CheckState = CheckState.Checked;
                    break;
                default:
                    break;
            }
        }

        private void ReSizeForm()
        {
            this.MaximumSize = new Size(m_nWidth * m_nBlockPixel + 20, m_nHeight * m_nBlockPixel + 70);
            this.MinimumSize = new Size(m_nWidth * m_nBlockPixel + 20, m_nHeight * m_nBlockPixel + 70);
        }

        private void m_oTimer_Tick(object sender, EventArgs e)
        {
            if ((m_pntRobot.X - m_pntEndPoint.X) * (m_pntRobot.X - m_pntEndPoint.X) + (m_pntRobot.Y - m_pntEndPoint.Y) * (m_pntRobot.Y - m_pntEndPoint.Y) == 1)
            {
                return;
            }
            if (m_pntEndPoint.X >= m_nCnstWidth || m_pntEndPoint.X < 0 || m_pntEndPoint.Y >= m_nCnstHeight || m_pntEndPoint.Y < 0)
            {
                return;
            }
            if (m_pntRobot.X >= m_nCnstWidth || m_pntRobot.X < 0 || m_pntRobot.Y >= m_nCnstHeight || m_pntRobot.Y < 0)
            {
                return;
            }
            ReFindPath();
            if (0 < m_nPathLen)
            {
                UpdateBlock(m_aPath[0], m_aPath[1], BlockType.Robot);
            }
        }
        
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.DrawPanel.CreateGraphics();
            g.DrawImage(m_bmpDrawingSpace, new Point(0, 0));
            g.Dispose();
        }
            
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && !m_nMouseDown)
            {
                if (m_enumState == State.Run)
                    m_oTimer.Stop();
                m_nMouseDown = true;
                int x = e.X / m_nBlockPixel;
                int y = e.Y / m_nBlockPixel;
                UpdateBlock(x, y, m_enumCurrentBlock);
            }
        }

        private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && m_nMouseDown)
            {
                if (m_enumState == State.Run)
                {
                    m_oTimer.Start();
                }
                m_nMouseDown = false;
            }
        }

        private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_nMouseDown)
            {
                int x = e.X / m_nBlockPixel;
                int y = e.Y / m_nBlockPixel;
                UpdateBlock(x, y, m_enumCurrentBlock);
            }
        }

        private void 空白ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCurrentBlock(BlockType.Blank);
        }

        private void 墙ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCurrentBlock(BlockType.Wall);
        }

        private void 机器人ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCurrentBlock(BlockType.Robot);
        }

        private void 终止点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCurrentBlock(BlockType.Goal);
        }

        private void 自动寻路模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeState(State.Run);
            m_oTimer.Start();
        }

        private void 停止寻路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeState(State.Edit);
            m_oTimer.Stop();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string filePath;
            byte[] bMap = null;
            int startX = 0, startY = 0, endX = 0, endY = 0;
            //
            ChangeState(State.Edit);
            //
            dialog.Filter = "地图文件|*.mp|所有文件|*.*";
            dialog.CheckFileExists = true;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                DllAPI.RobotLoadMap(filePath, ref startX, ref startY, ref endX, ref endY);
                m_pntRobot.X = startX;
                m_pntRobot.Y = startY;
                m_pntEndPoint.X = endX;
                m_pntEndPoint.Y = endY;
                m_nWidth = DllAPI.getWidth();
                m_nHeight = DllAPI.getHeight();
                if (DllAPI.getMap(ref bMap) != 0)
                {
                    return;
                }
                //
                m_bmpDrawingSpace = new Bitmap(m_nWidth * m_nBlockPixel, m_nHeight * m_nBlockPixel);
                //
                RePaint(bMap);
                ReSizeForm();
                this.Invalidate();
                this.Update();
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            string filePath;
            ChangeState(State.Edit);
            dialog.Filter = "地图文件|*.mp";
            dialog.CheckPathExists = true;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                if (DllAPI.RobotSaveMap(filePath) != 0)
                {
                    return;
                }
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] bmap = null;
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;
            ChangeState(State.Edit);
            if (DllAPI.RobotCreateEmptyMap(m_nWidth, m_nHeight, ref startX, ref startY, ref endX, ref endY) != 0)
            {
                return;
            }
            m_pntRobot.X = startX;
            m_pntRobot.Y = startY;
            m_pntEndPoint.X = endX;
            m_pntEndPoint.Y = endY;
            if (DllAPI.getMap(ref bmap) != 0)
            {
                return;
            }
            RePaint(bmap);
            ReSizeForm();
            this.Invalidate();
            this.Update();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 随机生成地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] bmap = null;
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;
            ChangeState(State.Edit);
            //
            m_nWidth = m_nCnstWidth;
            m_nHeight = m_nCnstHeight;
            //
            if (DllAPI.RobotCreateRandomMap(m_nWidth, m_nHeight, ref startX, ref startY, ref endX, ref endY, 0.2f) != 0)
            {
                return;
            }
            m_pntRobot.X = startX;
            m_pntRobot.Y = startY;
            m_pntEndPoint.X = endX;
            m_pntEndPoint.Y = endY;
            if (DllAPI.getMap(ref bmap) != 0)
            {
                return;
            }
            RePaint(bmap);
            ReSizeForm();
            this.Invalidate();
            this.Update();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_frm abFrm = new About_frm();
            abFrm.ShowDialog();
        }

        private void 运动速度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_enumState == State.Run)
            {
                m_oTimer.Stop();
            }
            IntervalTimingSetter itSetter = new IntervalTimingSetter(m_oTimer.Interval);
            if (itSetter.ShowDialog() == DialogResult.OK)
            {
                m_oTimer.Interval = itSetter.getInterval();
            }
            if (m_enumState == State.Run)
            {
                m_oTimer.Start();
            }
        }
    
    }
}
