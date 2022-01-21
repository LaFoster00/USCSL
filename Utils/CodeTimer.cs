using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace USCSL.Utils
{
    public struct CodeTimer_Single
    {
        private Stopwatch _stopwatch;
        private string _functionName;
        private bool _printFunctionName;
        private Action<string> _logFunction;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CodeTimer_Single(bool printFunctionName, Action<string> logFunction)
        {
            _functionName = new StackFrame(1).GetMethod().Name;
            _printFunctionName = printFunctionName;
            _logFunction = logFunction;
                _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            _logFunction(_printFunctionName
                ? $"Function '{_functionName}' took {_stopwatch.Elapsed.TotalSeconds} seconds."
                : $"Stopwatch measured {_stopwatch.Elapsed.TotalSeconds} seconds.");
        }
    }

    public struct CodeTimer_Average
    {
        private Stopwatch _stopwatch;
        private string _functionName;
        private bool _printFunctionName;
        private Action<string> _logFunction;
        private bool _printTotalTime;
        private bool _printNbInvocations;

        private struct TimingInfo
        {
            public int NbCalls { get; private set; }
            public double TotalTime { get; private set; }
            public double AverageTime { get; private set; }

            public double AddTimeAndGetAverage(double time)
            {
                TotalTime += time;
                NbCalls++;
                AverageTime = TotalTime / NbCalls;
                return AverageTime;
            }
        }
        private static Dictionary<string, TimingInfo> _functionCallTimings;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CodeTimer_Average(bool printFunctionName, bool printTotalTime, bool printNbInvocations, Action<string> logFunction)
        {
            _functionName = new StackFrame(1).GetMethod().Name;
            _printFunctionName = printFunctionName;
            _logFunction = logFunction;
            _printTotalTime = printTotalTime;
            _printNbInvocations = printNbInvocations;

            _functionCallTimings ??= new Dictionary<string, TimingInfo>(64);
            if (!_functionCallTimings.ContainsKey(_functionName))
                _functionCallTimings.Add(_functionName, new TimingInfo());

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            TimingInfo timingInfo = _functionCallTimings[_functionName];
            double average = timingInfo.AddTimeAndGetAverage(_stopwatch.Elapsed.TotalSeconds);
            _functionCallTimings[_functionName] = timingInfo;
            string message = _printFunctionName
                ? $"Function '{_functionName}' took {_stopwatch.Elapsed.TotalSeconds} seconds, {average} on average."
                : $"Stopwatch measured {_stopwatch.Elapsed.TotalSeconds} seconds, {average} on average.";
            
            if (_printTotalTime)
                message += $"\n{timingInfo.TotalTime} seconds in total";
            
            if (_printNbInvocations)
                message += $" {timingInfo.NbCalls} total invocations";
            _logFunction(message);
        }
    }
}