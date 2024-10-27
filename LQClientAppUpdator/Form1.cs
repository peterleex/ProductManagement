using System.Diagnostics;

namespace LQClientAppUpdator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WaitForClientAppToExit();
        }

        private const string _clientAppProcessName = @"龍騰數位題庫應用程式";
        private readonly double _waitToExitSeconds = 30;
        private void WaitForClientAppToExit()
        {
            var processes = Process.GetProcessesByName(_clientAppProcessName);
            var result = false;
            if (processes.Any())
            {
                var process = processes.First();
                MessageBox.Show(process!.MainModule!.FileName);
                result = process.WaitForExit(TimeSpan.FromSeconds(_waitToExitSeconds));
            }
            else
                result = true;

            if (result)
                MessageBox.Show("龍騰數位題庫應用程式.exe 已結束。", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("龍騰數位題庫應用程式.exe 未結束。", "通知", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
