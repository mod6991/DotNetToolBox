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

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Collections.Generic;

namespace DotNetToolBox.Scripting
{
    public class PythonEngineWrapper
    {
        private ScriptEngine _engine;
        private ScriptSource _source;

        public PythonEngineWrapper(string scriptPath, List<string> searchPaths = null)
        {
            _engine = Python.CreateEngine();

            if (searchPaths != null && searchPaths.Count > 0)
            {
                ICollection<string> paths = _engine.GetSearchPaths();

                foreach (string path in searchPaths)
                    paths.Add(path);

                _engine.SetSearchPaths(paths);
            }

            _source = _engine.CreateScriptSourceFromFile(scriptPath);
        }

        public Dictionary<string, dynamic> GetValues(Dictionary<string, object> inputVariables, List<string> outputVariables)
        {
            Dictionary<string, dynamic> ret = new Dictionary<string, dynamic>();

            // Set a new scope
            ScriptScope scope = _engine.CreateScope();

            // Set input variables
            foreach (KeyValuePair<string, object> inputKVP in inputVariables)
                scope.SetVariable(inputKVP.Key, inputKVP.Value);

            // Execute script
            object result = _source.Execute(scope);

            // Get output variables
            foreach (string outputVariable in outputVariables)
                ret.Add(outputVariable, scope.GetVariable(outputVariable));

            return ret;
        }
    }
}