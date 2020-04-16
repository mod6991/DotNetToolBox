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

namespace DotNetToolBox.Configuration
{
    public class INIConfigFileManager : IConfigFileManager
    {
        private string _file;
        private Dictionary<string, Dictionary<string, string>> _settings;

        #region Constructors

        public INIConfigFileManager(string file)
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
            if (section.Contains("[") || section.Contains("]"))
                throw new ArgumentException("An INI section name cannot contain '[' or ']'");

            if (name.Contains("="))
                throw new ArgumentException("An INI setting name cannot contain '='");

            if (!_settings.ContainsKey(section))
                _settings.Add(section, new Dictionary<string, string>());

            if (_settings[section].ContainsKey(name))
                throw new SettingAlreadyExistsException($"The setting '{name}' already exists in section '{section}'");

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
                using (StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false)))
                {
                    foreach(KeyValuePair<string, Dictionary<string, string>> kvp in _settings)
                    {
                        sw.WriteLine($"[{kvp.Key}]");
                        foreach(KeyValuePair<string, string> kvp2 in kvp.Value)
                            sw.WriteLine($"{kvp2.Key}={kvp2.Value}");
                        sw.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Load configuration file
        /// </summary>
        public void LoadConfigurationFile()
        {
            if (!System.IO.File.Exists(_file))
                throw new FileNotFoundException("Config file not found", _file);

            _settings = new Dictionary<string, Dictionary<string, string>>();

            using (FileStream fs = new FileStream(_file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    string sectionName = string.Empty;

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine().Trim();

                        if(line.StartsWith(";") || string.IsNullOrWhiteSpace(line)) { }
                        else if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            int endingPos = line.IndexOf("]");
                            sectionName = line.Substring(1, endingPos - 1);
                            _settings.Add(sectionName, new Dictionary<string, string>());
                        }
                        else
                        {
                            int sepPos = line.IndexOf("=");
                            string settingName = line.Substring(0, sepPos);
                            string settingValue = line.Substring(sepPos + 1, line.Length - sepPos - 1);
                            _settings[sectionName].Add(settingName, settingValue);
                        }
                    }
                }
            }
        }

        #endregion
    }
}