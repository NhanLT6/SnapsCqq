using SnapsCqq.Helper;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SnapsCqq
{
    public partial class Main : Form
    {
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;

        private const string PAGE_DOWN = "{PGDN}";
        private const string PAGE_UP = "{PGUP}";
        
        private bool IsStarted = false;

        public Main()
        {
            InitializeComponent();
        }

        #region Form events

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            btnStart.Text = timer1.Enabled ? "Stop" : "Start";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var rnd = new Random();
            var count = rnd.Next(3, 11);
            try
            {
                for (var i = 0; i < count - 2; i++)
                {
                    // Send key
                    switch (rnd.Next(1, 3))
                    {
                        case 1:
                            SendKeys.Send(PAGE_UP);
                            break;
                        case 2:
                            SendKeys.Send(PAGE_DOWN);
                            break;
                    }
                }
            }
            catch { }

            var X = Cursor.Position.X;
            var Y = Cursor.Position.Y;

            for (var i = 0; i < count - 3; i++)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0); 
            }
        }

        #endregion

        #region System Functions

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion
    }
}
