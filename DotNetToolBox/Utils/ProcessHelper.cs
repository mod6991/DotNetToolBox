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

using System.Diagnostics;

namespace DotNetToolBox.Utils
{
    public class ProcessResult
    {
        public ProcessResult(int exitCode, string standardOutput, string standardError)
        {
            ExitCode = exitCode;
            StandardOutput = standardOutput;
            StandardError = standardError;
        }

        public int ExitCode { get; }
        public string StandardOutput { get; }
        public string StandardError { get; }

        public override string ToString()
        {
            return $"ExitCode: {ExitCode}, Output: '{StandardOutput}', Error: '{StandardError}'";
        }
    }

    public static class ProcessHelper
    {
        /// <summary>
        /// Execute a process with parameters
        /// </summary>
        /// <param name="path">Path of the process</param>
        /// <param name="arguments">Arguments</param>
        /// <param name="workingDirectory">Working directory for the process to be started</param>
        /// <returns>Execution result</returns>
        public static ProcessResult Execute(string path, string arguments = null, string workingDirectory = null)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = path;
            start.Arguments = arguments;
            start.WorkingDirectory = workingDirectory;

            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                return new ProcessResult(proc.ExitCode, proc.StandardOutput.ReadToEnd(), proc.StandardError.ReadToEnd());
            }
        }
    }
}
