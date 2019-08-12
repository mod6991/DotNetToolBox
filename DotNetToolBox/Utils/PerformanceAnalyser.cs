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
using System.Diagnostics;

namespace DotNetToolBox.Utils
{
    public static class PerformanceAnalyser
    {
        /// <summary>
        /// Measure time in ticks
        /// </summary>
        /// <param name="action">Action to measure</param>
        /// <param name="iterations">Iterations</param>
        public static long MeasureTicks(Action action, int iterations)
        {
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
                action();

            sw.Stop();
            return sw.ElapsedTicks;
        }

        /// <summary>
        /// Measure time in milliseconds
        /// </summary>
        /// <param name="action">Action to measure</param>
        /// <param name="iterations">Iterations</param>
        public static long MeasureMilliseconds(Action action, int iterations)
        {
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
                action();

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
