using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace WindowsFormsApplication2
{
    
    public partial class Form1 : Form
    {
        public string sendToSupportPool = @"c:\Send_to_support\Pool";
        public string installPathTrimedFinal;
        public string sendToSupportLearnSet = @"c:\Send_to_support\Learnset";
        public string sendToSupportLogs = @"c:\Send_to_support\Logs";
        public string sendToSupportGeneric = @"c:\Send_to_support\Generic";
        public string windowsVersionRegistry = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";
        public Form1()
        {
            InitializeComponent();
            
            Directory.CreateDirectory(sendToSupportLogs);
            Directory.CreateDirectory(sendToSupportGeneric);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {


            ///////////////////// copy info from registry ////////////////////
            Task copyInfoFromReg = Task.Run(() =>
                {
                    string reg64 = "HKEY_LOCAL_MACHINE" + "\\" + "SOFTWARE" + "\\" + "Wow6432Node" + "\\" + "Brainware";
                    string reg32 = "HKEY_LOCAL_MACHINE" + "\\" + "SOFTWARE" + "\\" + "Brainware";

                    if (Environment.Is64BitOperatingSystem == true)
                    {
                        Process.Start("regedit.exe", "/e" + " " + @"c:\Send_To_Support\Generic\Registry_Export.reg" + " " + reg64);


                    }
                    else
                    {
                        Process.Start("regedit.exe", "/e" + " " + @"c:\Send_To_Support\Generic\Registry_Export.reg" + " " + reg32);
                    }
                }
                );





            ///////////////////create os file information//////////////////////
            Task createOsFileInfo = Task.Run(() =>
            {
                string command = "/C systeminfo > c:" + "\\" + "Send_to_support" + "\\" + "Generic" + "\\" + "OSInfo.txt";
                Process.Start("CMD.exe", command);


                RegistryKey installFolderK = Registry.LocalMachine;
                installFolderK = installFolderK.OpenSubKey(@"SOFTWARE\", true);
                string componentInfo = "ComponentInfo.txt";
                StreamWriter createFile = File.CreateText(sendToSupportGeneric + "\\" + componentInfo);
                createFile.Close();
            });


            Task getCompInfo1 = Task.Run(() =>
                {
                    /////////////////////////////
                    RegistryKey installFolderK = Registry.LocalMachine;
                    installFolderK = installFolderK.OpenSubKey(@"SOFTWARE\", true);
                    string componentInfo = "ComponentInfo.txt";
                    StreamWriter createFile = File.CreateText(sendToSupportGeneric + "\\" + componentInfo);
                    createFile.Close();
                    {

                        Parallel.ForEach(installFolderK.GetSubKeyNames(), subkeynamelist => 
                        {
                            try
                            {
                                using (RegistryKey key = installFolderK.OpenSubKey(subkeynamelist))
                                {

                                    foreach (var subkeynamelist2 in key.GetSubKeyNames())
                                    {
                                        try
                                        {
                                            using (RegistryKey key2 = key.OpenSubKey(subkeynamelist2))
                                            {

                                                foreach (var subkeyvalue in key2.GetValueNames())
                                                {

                                                    string installPathCRO = key2.GetValue(subkeyvalue).ToString();




                                                    if (subkeyvalue == "CRO")
                                                    {
                                                        string componentsFolder = System.IO.Directory.GetParent(installPathCRO.ToString()).ToString();
                                                        string mainFolder = System.IO.Directory.GetParent(componentsFolder).ToString();//Brainware folder

                                                        foreach (var firstLevelMainFolder in Directory.GetDirectories(mainFolder))
                                                        {

                                                            foreach (var secondLevelMainFolder in Directory.GetDirectories(firstLevelMainFolder))
                                                            {
                                                                foreach (var secondLevelMainFolderFiles in Directory.GetFiles(secondLevelMainFolder))
                                                                {

                                                                    if (secondLevelMainFolderFiles.EndsWith("Brainware.System.Project.exe.config"))
                                                                    {
                                                                        string fileName = Path.GetFileName(secondLevelMainFolderFiles);
                                                                        string destPath = Path.Combine(sendToSupportGeneric, fileName);
                                                                        File.Copy(secondLevelMainFolderFiles, destPath, true);
                                                                    }

                                                                }


                                                            }

                                                        }


                                                    }


                                                }

                                            }
                                        }
                                        catch (System.IO.IOException)
                                        {
                                        }
                                    }

                                }
                            }
                            catch (System.IO.IOException)
                            {

                            }
                        });

                    }
                });
            Task getCompInfo2 = Task.Run(() =>
                {
                    RegistryKey installFolderK = Registry.LocalMachine;
                    installFolderK = installFolderK.OpenSubKey(@"SOFTWARE\", true);
                    Parallel.ForEach(installFolderK.GetSubKeyNames(), subkeynamelist =>
                        {
                            try
                            {
                                using (RegistryKey key = installFolderK.OpenSubKey(subkeynamelist))
                                {

                                    foreach (var subkeynamelist2 in key.GetSubKeyNames())
                                    {
                                        try
                                        {
                                            using (RegistryKey key2 = key.OpenSubKey(subkeynamelist2))
                                            {

                                                foreach (var subkeyvalue in key2.GetValueNames())
                                                {
                                                    string installPathCRO = key2.GetValue(subkeyvalue).ToString();
                                                    string installPathCDR = key2.GetValue(subkeyvalue).ToString();



                                                    if (subkeyvalue == "CRO")
                                                    {

                                                        string windowslocation32 = "C:\\Windows\\System32";
                                                        string windowslocation64 = "C:\\Windows\\Syswow64";

                                                        if (Environment.Is64BitOperatingSystem)
                                                        {

                                                            foreach (string filename in Directory.EnumerateFiles(windowslocation64, "WWB9_32W.dll"))
                                                            {

                                                                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(filename);
                                                                FileStream fs = new FileStream(@"c:\Send_to_support\Generic\ComponentInfo.txt", FileMode.Append, FileAccess.Write);
                                                                StreamWriter InjectIntoFile = new StreamWriter(fs);
                                                                InjectIntoFile.WriteLine(fileInfo);
                                                                InjectIntoFile.Close();

                                                            }



                                                        }
                                                        else
                                                        {
                                                            foreach (string filename in Directory.EnumerateFiles(windowslocation32, "WWB9_32W.dll"))
                                                            {

                                                                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(filename);
                                                                FileStream fs = new FileStream(@"c:\Send_to_support\Generic\ComponentInfo.txt", FileMode.Append, FileAccess.Write);
                                                                StreamWriter InjectIntoFile = new StreamWriter(fs);
                                                                InjectIntoFile.WriteLine(fileInfo);
                                                                InjectIntoFile.Close();

                                                            }

                                                        }



                                                        //////////////////////////////go to CRO step back and export the key////////////////////////
                                                        ////////////////////////////////////////////////////////////////////////////////////////////

                                                        foreach (string fileName in Directory.EnumerateFiles(installPathCRO, "*.dll"))
                                                        {


                                                            FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fileName);
                                                            FileStream fs = new FileStream(@"c:\Send_to_support\Generic\ComponentInfo.txt", FileMode.Append, FileAccess.Write);
                                                            StreamWriter InjectIntoFile = new StreamWriter(fs);
                                                            InjectIntoFile.WriteLine(fileInfo);
                                                            InjectIntoFile.Close();

                                                        }

                                                    }

                                                }


                                            }

                                        }
                                        catch (System.IO.IOException)
                                        {
                                        }


                                    }

                                }
                            }
                            catch (System.IO.IOException)
                            {
                            }
                        }

                        );
                    
                }
                
               );
            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox5);
        }
                    
                  
            
                
        
        
        private void button2_Click(object sender, EventArgs e)
        {
            Task getCompInfo2 = Task.Run(() =>
            {
                DialogResultYesNo newDialogResult = new DialogResultYesNo();
                Class1 callClass1 = new Class1();
                callClass1.openDialog("Log Files (*.log)|*.log", sendToSupportLogs);
                DialogResultYesNo newDiagRes = new DialogResultYesNo();
                newDiagRes.diagRes(pictureBox4);
            });
          
           
        }
        
        public void button3_Click(object sender, EventArgs e)
        {
             
            {
                RegistryKey installFolderK = Registry.LocalMachine;
                installFolderK = installFolderK.OpenSubKey(@"SOFTWARE\", true);
                string componentInfo = "ComponentInfo.txt";
                StreamWriter createFile = File.CreateText(sendToSupportGeneric + "\\" + componentInfo);
                createFile.Close();
                {


                    foreach (var subkeynamelist in installFolderK.GetSubKeyNames())
                    {
                        try
                        {
                            using (RegistryKey key = installFolderK.OpenSubKey(subkeynamelist))
                            {
                                
                                foreach (var subkeynamelist2 in key.GetSubKeyNames())
                                {
                                    try
                                    {
                                        using (RegistryKey key2 = key.OpenSubKey(subkeynamelist2))
                                        {

                                            foreach (var subkeyvalue in key2.GetValueNames())
                                            {
                                                
                                                string installPathCRO = key2.GetValue(subkeyvalue).ToString();
                                                
                                                                                        


                                                if (subkeyvalue == "CRO")
                                                {
                                                    string componentsFolder = System.IO.Directory.GetParent(installPathCRO.ToString()).ToString();
                                                    string mainFolder = System.IO.Directory.GetParent(componentsFolder).ToString();

                                                    foreach (string firstLevelMainFolder in Directory.GetDirectories(mainFolder))
                                                    {
                                                        if (firstLevelMainFolder.EndsWith("Server"))
                                                        {
                                                            string[] filesInWebServer = Directory.GetFiles(firstLevelMainFolder);

                                                            foreach (var d in filesInWebServer)
                                                            {

                                                                    string fileName = Path.GetFileName(d);
                                                                    string destPath = Path.Combine(sendToSupportLogs, fileName);
                                                                    string destPath2 = Path.Combine(sendToSupportGeneric, fileName);
                                                                    if (d.EndsWith("Web.config"))
                                                                    {
                                                                        File.Copy(d, destPath2, true);
                                                                    }
                                                                    if (d.EndsWith("trace.log"))
                                                                    {
                                                                        File.Copy(d, destPath, true);
                                                                    }
                                                            }

                                                        }
                                                        foreach (var secondLevelMainFolder in Directory.GetDirectories(firstLevelMainFolder))
                                                        {
                                                            foreach (var secondLevelMainFolderFiles in Directory.GetFiles(secondLevelMainFolder))
                                                            {
                                                                
                                                                if (secondLevelMainFolderFiles.EndsWith("Brainware.System.Project.exe.config"))
                                                                {
                                                                    //MessageBox.Show(secondLevelMainFolderFiles);
                                                                    string fileName = Path.GetFileName(secondLevelMainFolderFiles);
                                                                    string destPath = Path.Combine(sendToSupportGeneric, fileName);
                                                                    File.Copy(secondLevelMainFolderFiles, destPath, true);
                                                                }

                                                            }
                                                            
                                                            foreach (var thirdLevelMainFolder in Directory.GetDirectories(secondLevelMainFolder))
                                                            {


                                                                if (thirdLevelMainFolder.EndsWith("Log"))
                                                                {
                                                                    string[] logFiles = Directory.GetFiles(thirdLevelMainFolder);
                                                                    
                                                                    foreach (var d in logFiles)
                                                                    {
                                                                        string fileName = Path.GetFileName(d);
                                                                        string destPath = Path.Combine(sendToSupportLogs, fileName);
                                                                        File.Copy(d, destPath, true);

                                                                    }

                                                                }
                                                                    
                                                            }
                                                        }
                                                    
                                                    }

                                                                                                                                
                                                }


                                            }

                                        }
                                    }
                                    catch (System.Security.SecurityException)
                                    {
                                    }
                                }

                            }
                        }
                        catch (System.Security.SecurityException)
                        {
                        }

                    }
                }               
            }
            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox1);
            
        }
        


        private void button5_Click(object sender, EventArgs e)
        {
            Class1 callClass1 = new Class1();
            callClass1.openDialog("INI File (*.ini)|*.ini", sendToSupportGeneric);

            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox2);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Class1 callClass1 = new Class1();
            callClass1.openDialog("Project File (*.sdp)|*.sdp", sendToSupportGeneric);
            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Class2 newClass2 = new Class2();
            newClass2.openDiagMethod(sendToSupportPool);
            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox7);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Class2 newClass2 = new Class2();
            newClass2.openDiagMethod(sendToSupportLearnSet);
            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            
            string credentials = @"C:\\Send_to_support\\credentials.txt";
            
            FileStream fs = new FileStream(credentials, FileMode.Append, FileAccess.Write);
            StreamWriter InjectIntoFile = new StreamWriter(fs);
            InjectIntoFile.WriteLine(textBox1.Text);
            InjectIntoFile.WriteLine(textBox2.Text);
            InjectIntoFile.Close();

            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox9);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Class1 callClass1 = new Class1();
            callClass1.openDialog("License File (*.lic)|*.lic", sendToSupportGeneric);
            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox10);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Class1 callClass1 = new Class1();
            callClass1.openDialog("License File (*.lic)|*.lic", sendToSupportGeneric);
            DialogResultYesNo newDiagRes = new DialogResultYesNo();
            newDiagRes.diagRes(pictureBox11);
            
            
        }

        
    }


    
}
