using Serilog;
using Polly.Caching;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;
using System.Diagnostics;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQUpdator : Form
    {
        public LQUpdator(IServiceProvider serviceProvider)
        {
            InitializeComponent();
        }

        private string targetPath = null!;
        private async void LQUpdator_Load(object sender, EventArgs e)
        {
            var result = await BeginUpdate();
            while (UpdateClientResult.Error == result)
            {
                LQHelper.InfoMessage(LQMessage(LQCode.C0013));
                result = await BeginUpdate();
            }

            ExcuteMainExe();
            Application.Exit();
        }

        private void ExcuteMainExe()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = MainExeFilePath,
                WorkingDirectory = Path.GetDirectoryName(MainExeFilePath)!,
            };
            Process.Start(startInfo);
        }   

        private string MainExeFilePath => Path.Combine(targetPath, MainExeFileName);

        private async Task<UpdateClientResult> BeginUpdate()
        {
            GetUpdatePath();
            try
            {
                await DeleteOldFiles();
                await CopyNewFiles();
                return UpdateClientResult.Successful;
            }
            catch (Exception ex)
            {
                Log.Error(ex,LQMessage(LQCode.C0012));
                return UpdateClientResult.Error;
            }
        }

        private async Task CopyNewFiles()
        {
            var sourcePath = LQHelper.CurrentProcessDirectory;
            var total = GetFileAndDirectoryCount(sourcePath);
            copyCount = 0;
            await CopyDirectory(sourcePath, targetPath, total);
        }

        private int copyCount;
        private async Task CopyDirectory(string sourceDir, string destDir, int total)
        {
            var info = LQMessage(LQCode.C0011);

            // Create the destination directory if it doesn't exist
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
                UpdateProgress(++copyCount, total, info);
            }

            // Copy all the files in the directory
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var fileName = Path.GetFileName(file);
                var destFile = Path.Combine(destDir, fileName);
                await Task.Run(() => File.Copy(file, destFile, true));
                UpdateProgress(++copyCount, total, info);
            }

            // Recursively copy all the subdirectories
            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                var dirName = Path.GetFileName(dir);
                var destSubDir = Path.Combine(destDir, dirName);
                await CopyDirectory(dir, destSubDir, total);
            }
        }

        private void GetUpdatePath()
        {
            if (Environment.GetCommandLineArgs().Length != 2)
            {
                Application.Exit();
                return;
            }

            targetPath = Environment.GetCommandLineArgs()[1];
        }

        private async Task DeleteOldFiles()
        {
            int count = 0;
            var total = GetFileAndDirectoryCount(targetPath);
            var info = LQMessage(LQCode.C0010);

            foreach (var file in Directory.GetFiles(targetPath))
            {
                await Task.Run(() => File.Delete(file));
                UpdateProgress(++count, total, info);
            }

            foreach (var dir in Directory.GetDirectories(targetPath))
            {
                await Task.Run(() => Directory.Delete(dir, true));
                UpdateProgress(++count, total, info);
            }
        }

        private void UpdateProgress(int count, int total, string info)
        {
            var progress = (int)((count * 100) / (float)total);

            progressBar.Value = progress;
            UpdateProgressLabel(info);
        }

        private void UpdateProgressLabel(string info)
        {
            percentageLabel.Text = $"{info}{progressBar.Value * 100 / progressBar.Maximum}%";
        }

        private static int GetFileAndDirectoryCount(string path)
        {
            int fileCount = 0;
            int directoryCount = 0;

            fileCount += Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
            directoryCount += Directory.GetDirectories(path, "*", SearchOption.AllDirectories).Length;

            return fileCount + directoryCount;

            //int fileCount = Directory.GetFiles(path).Length;
            //int directoryCount = Directory.GetDirectories(path).Length;

            //foreach (var dir in Directory.GetDirectories(path))
            //{
            //    fileCount += Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Length;
            //    directoryCount += Directory.GetDirectories(dir, "*", SearchOption.AllDirectories).Length;
            //}

            //return fileCount + directoryCount;
        }
    }
}
