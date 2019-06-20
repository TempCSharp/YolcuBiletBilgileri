using System;
using System.Windows.Forms;

namespace YolcuBiletBilgileri
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 frm = new Form1();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form2());
            }
        }
    }
}