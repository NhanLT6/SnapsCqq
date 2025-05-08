using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SnapsCqq
{
    public partial class Main : Form
    {
        private const int MouseEventLeftClick = 0x0002 | 0x0004;
        private const string PageDown = "{PGDN}";
        private const string PageUp = "{PGUP}";

        public Main()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private void MainForm_Load(object sender, EventArgs e) { }

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
            int actionCount = rnd.Next(3, 11);

            for (var i = 0; i < actionCount - 2; i++) SendRandomPageKey(rnd);

            ClickMouseMultipleTimes(actionCount - 3);
        }

        private void SendRandomPageKey(Random rnd)
        {
            string keyToSend = rnd.Next(1, 3) == 1 ? PageUp : PageDown;
            SendKeys.Send(keyToSend);
        }

        private void ClickMouseMultipleTimes(int clickCount)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;

            for (var i = 0; i < clickCount; i++) mouse_event(MouseEventLeftClick, x, y, 0, 0);
        }
    }
}