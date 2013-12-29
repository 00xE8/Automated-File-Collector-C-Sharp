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



namespace WindowsFormsApplication2
{
    public class Class1 : Form1
    {
        //This class is intended to open a dialog to enable users to select files to be copied////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        public delegate void openDialogEventHandler(string filterExtension, string location);
        
        public void openDialog(string filterExtension, string location)
        {

            
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Multiselect = true;

            opendialog.Filter = filterExtension;
            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in opendialog.FileNames)
                {
                    try
                    {
                        string fileName = Path.GetFileName(s);
                        var dest = Path.Combine(location, fileName);
                        File.Copy(s, dest, true);
                       
                      
                    }
                    catch (System.Security.SecurityException)
                    {
                    }

                }
            }
        }
        
    }
}
