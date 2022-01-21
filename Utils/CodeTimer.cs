using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace USCSL.Utils
{
    public static class CodeTimerOptions
    {
        public static bool active;
    }
    
    public struct CodeTimer_Single
    {
        private Stopwatch _stopwatch;
        private string _functionName;
        private bool _printFunctionName;
        private Action<string> _logFunction;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CodeTimer_Single(bool printFunctionName, Action<string> logFunction)
        {
            if (CodeTimerOptions.active)
            {
                _functionName = new StackFrame(1).GetMethod().Name;
                _printFunctionName = printFunctionName;
                _logFunction = logFunction;
                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
            else
            {
                _stopwatch = null;
                _functionName = null;
                _printFunctionName = false;
                _logFunction = null;
            }
        }

        public void Stop(bool printMessage)
        {
            if (!CodeTimerOptions.active) return;

            _stopwatch.Stop();
            if (!printMessage) return;
            
            _logFunction(_printFunctionName
                ? $"Function '{_functionName}' took {_stopwatch.Elapsed.TotalSeconds} seconds."
                : $"Stopwatch measured {_stopwatch.Elapsed.TotalSeconds} seconds.");
        }
    }

    public struct CodeTimer_Average
    {
        private TimerInfo _timerInfo;
        private Stopwatch _stopwatch;
        private Action<string> _logFunction;

        public struct TimerInfo
        {
            public string functionName;
            public bool printFunctionName;
            public bool printTotalTime;
            public bool printNbInvocations;
        }
        
        public struct TimingInfo
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

        public static Dictionary<string, (TimerInfo, TimingInfo)> FunctionCallTimings { get; private set; }

        public static string GetMessage((TimerInfo, TimingInfo) info)
        {
            string message = info.Item1.printFunctionName
                ? $"Function '{info.Item1.functionName}' took {info.Item2.AverageTime} on average."
                : $"Execution took {info.Item2.AverageTime} on average.";
            
            if (info.Item1.printTotalTime)
                message += $"\n{info.Item2.TotalTime} seconds in total.";
            
            if (info.Item1.printNbInvocations)
                message += $" {info.Item2.NbCalls} total invocations";

            return message;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CodeTimer_Average(bool printFunctionName, bool printTotalTime, bool printNbInvocations, Action<string> logFunction)
        {
            if (CodeTimerOptions.active)
            {
                _timerInfo = new TimerInfo()
                {
                    functionName = new StackFrame(1).GetMethod().Name,
                    printFunctionName = printFunctionName,
                    printTotalTime = printTotalTime,
                    printNbInvocations = printNbInvocations,
                };
                
                _logFunction = logFunction;

                FunctionCallTimings ??= new Dictionary<string, (TimerInfo, TimingInfo)>(64);
                if (!FunctionCallTimings.ContainsKey(_timerInfo.functionName))
                    FunctionCallTimings.Add(_timerInfo.functionName, (_timerInfo, new TimingInfo()));

                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
            else
            {
                _timerInfo = new TimerInfo()
                {
                    functionName = null,
                    printFunctionName = false,
                    printTotalTime = false,
                    printNbInvocations = false,
                };
                _logFunction = null;
                _stopwatch = null;
            }
        }
        
        public CodeTimer_Average(bool printFunctionName, bool printTotalTime, bool printNbInvocations, string functionName, Action<string> logFunction)
        {
            if (CodeTimerOptions.active)
            {
                _timerInfo = new TimerInfo()
                {
                    functionName = functionName,
                    printFunctionName = printFunctionName,
                    printTotalTime = printTotalTime,
                    printNbInvocations = printNbInvocations,
                };
                
                _logFunction = logFunction;

                FunctionCallTimings ??= new Dictionary<string, (TimerInfo, TimingInfo)>(64);
                if (!FunctionCallTimings.ContainsKey(_timerInfo.functionName))
                    FunctionCallTimings.Add(_timerInfo.functionName, (_timerInfo, new TimingInfo()));

                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
            else
            {
                _timerInfo = new TimerInfo()
                {
                    functionName = null,
                    printFunctionName = false,
                    printTotalTime = false,
                    printNbInvocations = false,
                };
                
                _logFunction = null;
                _stopwatch = null;
            }
        }

        public void Stop(bool printMessage)
        {
            if (!CodeTimerOptions.active) return;
            
            _stopwatch.Stop();

            (TimerInfo, TimingInfo) timingInfo = FunctionCallTimings[_timerInfo.functionName];
            double average = timingInfo.Item2.AddTimeAndGetAverage(_stopwatch.Elapsed.TotalSeconds);
            FunctionCallTimings[_timerInfo.functionName] = timingInfo;

            if (!printMessage) return;
            string message = $"{_stopwatch.Elapsed.TotalSeconds} seconds.";
            message += GetMessage(timingInfo);
            _logFunction(message);
        }
    }
}