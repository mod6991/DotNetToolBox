using DotNetToolBox.Configuration;
using System;

namespace DotNetToolBox.Tester
{
    public static class ConfigurationTest
    {
        public static void XmlConfig()
        {
            try
            {
                XMLConfigFileManager xml = new XMLConfigFileManager(@"C:\Temp\test.xml");
                xml.AddSetting("", "UseDB", "False");
                xml.AddSetting("Database", "Name", "Prod");
                xml.AddSetting("Database", "Username", "josuec");
                xml.AddSetting("Database", "Password", "test1234");
                xml.AddSetting("Paths", "Input", @"C:\Data\Input");
                xml.AddSetting("Paths", "Output", @"C:\Data\Output");
                xml.AddSetting("Paths", "Temp", @"C:\Temp");
                xml.SaveConfigurationFile();

                xml.Settings.Clear();
                xml.LoadConfigurationFile();
                xml.RemoveSetting("Paths", "Temp");
                xml.SaveConfigurationFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void IniConfig()
        {
            try
            {
                INIConfigFileManager ini = new INIConfigFileManager(@"C:\Temp\test.ini");
                ini.AddSetting("", "UseDB", "False");
                ini.AddSetting("Database", "Name", "Prod");
                ini.AddSetting("Database", "Username", "josuec");
                ini.AddSetting("Database", "Password", "test1234");
                ini.AddSetting("Paths", "Input", @"C:\Data\Input");
                ini.AddSetting("Paths", "Output", @"C:\Data\Output");
                ini.AddSetting("Paths", "Temp", @"C:\Temp");
                ini.SaveConfigurationFile();

                ini.Settings.Clear();
                ini.LoadConfigurationFile();
                ini.RemoveSetting("Paths", "Temp");
                ini.SaveConfigurationFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
