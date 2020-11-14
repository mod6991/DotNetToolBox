#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012-2020 Josué Clément
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

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetToolBox.Scripting
{
    public class PythonScripting
    {
        private ScriptEngine _engine;
        private Dictionary<string, ScriptSource> _sources;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="searchPaths">Search paths to python lib</param>
        public PythonScripting(List<string> searchPaths = null)
        {
            _sources = new Dictionary<string, ScriptSource>();
            _engine = Python.CreateEngine();

            if (searchPaths != null && searchPaths.Count > 0)
            {
                ICollection<string> paths = _engine.GetSearchPaths();

                foreach (string path in searchPaths)
                    paths.Add(path);

                _engine.SetSearchPaths(paths);
            }
        }

        /// <summary>
        /// Add a script from file
        /// </summary>
        /// <param name="name">Script name</param>
        /// <param name="scriptPath">Script path</param>
        /// <param name="encoding">Script file encoding</param>
        public void AddScriptFromFile(string name, string scriptPath, Encoding encoding)
        {
            if (_sources.ContainsKey(name))
                throw new InvalidOperationException($"Script '{name}' already added");

            ScriptSource source = _engine.CreateScriptSourceFromFile(scriptPath, encoding);
            _sources.Add(name, source);
        }

        /// <summary>
        /// Add a script from string
        /// </summary>
        /// <param name="name">Script name</param>
        /// <param name="scriptContent">Script content</param>
        public void AddScriptFromString(string name, string scriptContent)
        {
            if (_sources.ContainsKey(name))
                throw new InvalidOperationException($"Script '{name}' already added");

            ScriptSource source = _engine.CreateScriptSourceFromString(scriptContent);
            _sources.Add(name, source);
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inputVariables"></param>
        /// <param name="outputVariables"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> ExecuteScript(string name, Dictionary<string, object> inputVariables, List<string> outputVariables)
        {
            if (!_sources.ContainsKey(name))
                throw new InvalidOperationException($"Script '{name}' not registered. Use AddScript before");

            Dictionary<string, dynamic> ret = new Dictionary<string, dynamic>();

            // Set a new scope
            ScriptScope scope = _engine.CreateScope(inputVariables);

            // Execute script
            dynamic result = _sources[name].Execute(scope);

            // Get output variables
            foreach (string outputVariable in outputVariables)
                ret.Add(outputVariable, scope.GetVariable(outputVariable));

            return ret;
        }
    }
}