using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class Class2 : Form1
    {
        public void openDiagMethod(string folderName)
    {
        FolderBrowserDialog folderBrDiag = new FolderBrowserDialog();

            folderBrDiag.ShowNewFolderButton = false;
            folderBrDiag.RootFolder = Environment.SpecialFolder.MyComputer;
            DialogResult result = folderBrDiag.ShowDialog();
            if (result == DialogResult.OK)
            {
                Directory.CreateDirectory(folderName);
                string filesToCopy = folderBrDiag.SelectedPath;
                foreach (string s in Directory.GetFiles(filesToCopy))
                {
                    
                    string fileName = Path.GetFileName(s);
                    string destPath = Path.Combine(folderName, fileName);
                    File.Copy(s, destPath, true);

                }

            }
    
    }
    }
}
