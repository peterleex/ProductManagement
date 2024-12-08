using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordAddIn
{
    internal class LQHelper
    {
        public static void InfoMessage(string message, string title = "")
        {
            MessageBox.Show(
               message, title,
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        public static void ErrorMessage(string message, string title = "")
        {
            MessageBox.Show(
               message, title,
               MessageBoxButtons.OK,
               MessageBoxIcon.Error);
        }

        public static DialogResult ConfirmMessage(string message, string title = "")
        {
            return MessageBox.Show(
                message, title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }
    }
}
