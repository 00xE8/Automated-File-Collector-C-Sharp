using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;




namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             
            {
                RegistryKey installFolderKey = Registry.LocalMachine;
                //installFolderKey = installFolderKey.OpenSubKey(@"SOFTWARE\Wow6432Node\Brainware\Components", true);
                installFolderKey = installFolderKey.OpenSubKey(@"SOFTWARE\Wow6432Node", true);
                MessageBox.Show(installFolderKey.GetValue("CRO").ToString());

                
                //string installFolderValue = installFolderKey.GetValue("CDR").ToString();

                //foreach (string fileName in Directory.EnumerateFiles(installFolderValue, "*.dll"))
                //{
                //    FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fileName);
                //    FileStream fs = new FileStream(@"C:\folder1\test.txt", FileMode.Append, FileAccess.Write);
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine(fileInfo);
                //    sw.Close();
                //}

                
                
            }
        }
        
    }

    
}
