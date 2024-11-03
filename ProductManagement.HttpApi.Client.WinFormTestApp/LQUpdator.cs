using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQUpdator : Form
    {
        public LQUpdator(IServiceProvider serviceProvider)
        {
            InitializeComponent();
        }

        private void LQUpdator_Load(object sender, EventArgs e)
        {
            if (Environment.GetCommandLineArgs().Length != 2)
            {
                Application.Exit();
                return;
            }

            var updatePath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[1]);

            // remove all files and directory in the updatePath
            DeleteAllFiles(updatePath);

            try
            {
                System.Diagnostics.Process.Start(updatePath);
            }
            catch (Exception)
            {
            }
            finally
            {
                Application.Exit();
            }
        }

        private static void DeleteAllFiles(string? updatePath)
        {
            if (Directory.Exists(updatePath))
            {
                foreach (var file in Directory.GetFiles(updatePath))
                {
                    File.Delete(file);
                }

                foreach (var dir in Directory.GetDirectories(updatePath))
                {
                    Directory.Delete(dir, true);
                }
            }
        }
    }
}
