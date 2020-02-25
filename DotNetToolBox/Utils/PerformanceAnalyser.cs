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
using System.Threading;

namespace DotNetToolBox.Utils
{
    public static class PerformanceAnalyser
    {
        /// <summary>
        /// Measure performance of an action
        /// </summary>
        /// <typeparam name="T">Measurment class</typeparam>
        /// <param name="action">Action</param>
        /// <param name="iterations">Iterations</param>
        public static TimeSpan Benchmark<T>(Action action, int iterations = 10000) where T : IStopWatch, new()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            //warm up
            action();

            IStopWatch stopwatch = new T();
            stopwatch.Start();

            for (int i = 0; i < iterations; i++)
                action();

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        /// <summary>
        /// Measure performance of an action with a Stopwatch
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="iterations">Iterations</param>
        public static TimeSpan BenchmarkTime(Action action, int iterations = 10000)
        {
            return Benchmark<TimeWatch>(action, iterations);
        }

        /// <summary>
        /// Measure performance of an action with the Process TotalProcessorTime
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="iterations">Iterations</param>
        public static TimeSpan BenchmarkCpu(Action action, int iterations = 10000)
        {
            return Benchmark<CpuWatch>(action, iterations);
        }
    }

    public interface IStopWatch
    {
        bool IsRunning { get; }
        TimeSpan Elapsed { get; }
        void Start();
        void Stop();
        void Reset();
    }

    public class TimeWatch : IStopWatch
    {
        private Stopwatch _stopwatch;

        public TimeWatch()
        {
            if (!Stopwatch.IsHighResolution)
                throw new NotSupportedException("Your hardware doesn't support high resolution counter");

            _stopwatch = new Stopwatch();

            //prevent the JIT Compiler from optimizing Fkt calls away
            long seed = Environment.TickCount;

            //prevent "Normal" processes from interrupting Threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            //prevent "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

        public bool IsRunning
        {
            get { return _stopwatch.IsRunning; }
        }

        public TimeSpan Elapsed
        {
            get { return _stopwatch.Elapsed; }
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }
    }

    public class CpuWatch : IStopWatch
    {
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private bool _isRunning;

        public CpuWatch()
        {
            _startTime = TimeSpan.Zero;
            _endTime = TimeSpan.Zero;
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public TimeSpan Elapsed
        {
            get
            {
                if (_isRunning)
                    throw new NotSupportedException("Getting elapsed span while watch is running is not supported");

                return _endTime - _startTime;
            }
        }

        public void Start()
        {
            _startTime = Process.GetCurrentProcess().TotalProcessorTime;
            _isRunning = true;
        }

        public void Stop()
        {
            _endTime = Process.GetCurrentProcess().TotalProcessorTime;
            _isRunning = false;
        }

        public void Reset()
        {
            _startTime = TimeSpan.Zero;
            _endTime = TimeSpan.Zero;
        }
    }
}
