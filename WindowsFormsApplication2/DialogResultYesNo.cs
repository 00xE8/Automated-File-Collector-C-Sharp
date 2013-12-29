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
using System.Management;

namespace WindowsFormsApplication2
{
    class DialogResultYesNo : Form1
    {
        public void diagRes(PictureBox picBoxNumber)
        {

            DialogResult actionCompleted = MessageBox.Show("Mark this action as completed?", "Important query", MessageBoxButtons.YesNo);

            if (actionCompleted == DialogResult.Yes)
            {
                picBoxNumber.Visible = true;
            }
            if (actionCompleted == DialogResult.No)
            {
                picBoxNumber.Visible = false;
            }
        }
    }
}
