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

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System.Text;

namespace DotNetToolBox.Utils
{
    public static class LogHelper
    {
        private static ILog InternalCreateRollingFileAppender(string repositoryName, string loggerName, string file, Encoding encoding, string pattern = "%date [%thread] %-5level - %message%newline", string maxFileSize = "100MB")
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = pattern;
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.File = file;
            roller.AppendToFile = true;
            roller.MaxSizeRollBackups = -1;
            roller.MaximumFileSize = maxFileSize;
            roller.StaticLogFileName = true;
            roller.Encoding = encoding;
            roller.Layout = patternLayout;
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.ActivateOptions();

            hierarchy.Root.AddAppender(roller);
            hierarchy.Root.Level = Level.Debug;

            ILoggerRepository repository = LoggerManager.CreateRepository(repositoryName);
            BasicConfigurator.Configure(repository, roller);

            return LogManager.GetLogger(repositoryName, loggerName);
        }

        public static ILog CreateRollingFileAppender(string repositoryName, string loggerName, string file)
        {
            return InternalCreateRollingFileAppender(repositoryName, loggerName, file, Encoding.UTF8);
        }

        public static ILog CreateRollingFileAppender(string repositoryName, string loggerName, string file, string pattern)
        {
            return InternalCreateRollingFileAppender(repositoryName, loggerName, file, Encoding.UTF8, pattern);
        }

        public static ILog CreateRollingFileAppender(string repositoryName, string loggerName, string file, string pattern, string maxFileSize)
        {
            return InternalCreateRollingFileAppender(repositoryName, loggerName, file, Encoding.UTF8, pattern, maxFileSize);
        }

        public static ILog CreateRollingFileAppender(string repositoryName, string loggerName, string file, string pattern, string maxFileSize, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            return InternalCreateRollingFileAppender(repositoryName, loggerName, file, encoding, pattern, maxFileSize);
        }
    }
}
