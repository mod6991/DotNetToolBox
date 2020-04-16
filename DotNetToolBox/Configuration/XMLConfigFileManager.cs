#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012  Josué Clément
//mod6991@gmail.com

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace DotNetToolBox.Configuration
{
    public class XMLConfigFileManager : IConfigFileManager
    {
        private string _file;
        private Dictionary<string, Dictionary<string, string>> _settings;

        #region Constructors

        public XMLConfigFileManager(string file)
        {
            _file = file;
            _settings = new Dictionary<string, Dictionary<string, string>>();
        }
        
        #endregion

        #region Properties

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public Dictionary<string, Dictionary<string, string>> Settings
        {
            get { return _settings; }
        }

        public Dictionary<string, string> this[string section]
        {
            get
            {
                if (!_settings.ContainsKey(section))
                    throw new SectionNotFoundException($"Section '{section}' not found");
                return _settings[section];
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a setting value
        /// </summary>
        /// <param name="section">Section name</param>
        /// <param name="name">Property name</param>
        public string GetSettingValue(string section, string name)
        {
            if (!_settings.ContainsKey(section))
                throw new SectionNotFoundException($"Section '{section}' not found");

            if (!_settings[section].ContainsKey(name))
                throw new SettingNotFoundException($"Setting '{name}' not found in section '{section}'");

            return _settings[section][name];
        }

        /// <summary>
        /// Add a new setting
        /// </summary>
        /// <param name="section">Section name</param>
        /// <param name="name">Property name</param>
        /// <param name="value">Property value</param>
        public void AddSetting(string section, string name, string value)
        {
            if (!_settings.ContainsKey(section))
                _settings.Add(section, new Dictionary<string, string>());

            if (_settings[section].ContainsKey(name))
                throw new SettingAlreadyExistsException($"Setting '{name}' already exists in section '{section}'");

            _settings[section].Add(name, value);
        }

        /// <summary>
        /// Remove a setting
        /// </summary>
        /// <param name="section">Section name</param>
        /// <param name="name">Property name</param>
        public void RemoveSetting(string section, string name)
        {
            if (!_settings.ContainsKey(section))
                throw new SectionNotFoundException($"Section '{section}' not found");

            if (!_settings[section].ContainsKey(name))
                throw new SettingNotFoundException($"Setting '{name}' not found in section '{section}'");

            _settings[section].Remove(name);
        }

        /// <summary>
        /// Save configuration file
        /// </summary>
        public void SaveConfigurationFile()
        {
            using (FileStream fs = new FileStream(_file, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using(XmlTextWriter xml = new XmlTextWriter(fs, new UTF8Encoding(false)))
                {
                    xml.Formatting = Formatting.Indented;

                    xml.WriteStartDocument();
                    xml.WriteStartElement("ConfigurationFile");

                    foreach (KeyValuePair<string, Dictionary<string, string>> kvp in _settings)
                    {
                        xml.WriteStartElement("Section");
                        xml.WriteAttributeString("Name", kvp.Key);

                        foreach(KeyValuePair<string, string> kvp2 in kvp.Value)
                        {
                            xml.WriteStartElement("Setting");

                            xml.WriteAttributeString("Name", kvp2.Key);
                            xml.WriteAttributeString("Value", kvp2.Value);

                            xml.WriteEndElement();
                        }

                        xml.WriteEndElement();
                    }

                    xml.WriteEndElement();
                    xml.WriteEndDocument();
                }
            }
        }

        /// <summary>
        /// Loads configuration file
        /// </summary>
        public void LoadConfigurationFile()
        {
            if (!System.IO.File.Exists(_file))
                throw new FileNotFoundException("Config file not found", _file);

            _settings = new Dictionary<string, Dictionary<string, string>>();

            XmlDocument doc = new XmlDocument();
            doc.Load(_file);

            XmlNodeList sectionList = doc.SelectNodes("/ConfigurationFile/Section");

            foreach(XmlNode sectionNode in sectionList)
            {
                string sectionName = sectionNode.Attributes["Name"].Value;
                _settings.Add(sectionName, new Dictionary<string, string>());

                XmlNodeList settingList = sectionNode.SelectNodes("Setting");

                foreach(XmlNode settingNode in settingList)
                {
                    string settingName = settingNode.Attributes["Name"].Value;
                    string settingValue = settingNode.Attributes["Value"].Value;
                    _settings[sectionName].Add(settingName, settingValue);
                }
            }
        }

        #endregion
    }
}