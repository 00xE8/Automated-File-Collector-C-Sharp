using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class folderAccessStructure : Form1
    {
        RegistryKey installFolderK = Registry.LocalMachine;
        installFolderK = installFolderK.OpenSubKey(@"SOFTWARE\", true);
                string componentInfo = "ComponentInfo.txt";
                StreamWriter createFile = File.CreateText(sendToSupportGeneric + "\\" + componentInfo);
                createFile.Close();
        public string returnFirstLevelFolder()
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

                                                    foreach (var firstLevelMainFolder in Directory.GetDirectories(mainFolder))
                                                    {
                                                        if (firstLevelMainFolder.EndsWith("Server"))
                                                        {
        }
        
        
        
        
        string componentsFolderTest
        {
            get { return System.IO.Directory.GetParent(ins)}
        }
    }
}
