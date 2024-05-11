
using System;
using System.Collections.Generic;

namespace Cr7Sund.Logger
{
    public class UnityEditorLogProvider : ILogProvider, IDisposable
    {
        private List<ILogDecorator> _logDecorators = new();
        private string _logChannel;

        public void Dispose()
        {
            for (int i = _logDecorators.Count - 1; i >= 0; i--)
            {
                _logDecorators[i]?.Dispose();
            }
        }

        public virtual void Init(LogSinkType logSinkType, string logChannel)
        {
            _logChannel = logChannel;

            if ((logSinkType & LogSinkType.Local) == LogSinkType.Local)
            {
                _logDecorators.Add(new UnityLogDecorator());
            }
            if ((logSinkType & LogSinkType.Net) == LogSinkType.Net)
            {
                // _logDecorators.Add(new RpcLogDecorator());
            }
            if ((logSinkType & LogSinkType.File) == LogSinkType.File)
            {
                // _logDecorators.Add(new FileLogDecorator());
            }
            for (int i = _logDecorators.Count - 1; i >= 0; i--)
            {
                _logDecorators[i]?.Initialize();
            }
        }


        public void WriteException(LogLevel logLevel, Exception ex)
        {
            string format = ex.ToString();
            switch (logLevel)
            {
                case LogLevel.Trace:
                    UnityEditorDebug(format);
                    break;
                case LogLevel.Debug:
                    UnityEditorDebug(format);
                    break;
                case LogLevel.Info:
                    UnityEditorDebug(format);
                    break;
                case LogLevel.Warn:
                    UnityEditorWarning(format);
                    break;
                case LogLevel.Error:
                    UnityEditorError(format);
                    break;
                case LogLevel.Fatal:
                    UnityEditorError(format);
                    break;
                default:
                    break;
            }
        }

        public void WriteException(LogLevel logLevel,  Exception e, string prefix)
        {
            string format = $"{prefix} \n{e.ToString()}";
            switch (logLevel)
            {
                case LogLevel.Trace:
                    UnityEditorDebug(format);
                    break;
                case LogLevel.Debug:
                    UnityEditorDebug(format);
                    break;
                case LogLevel.Info:
                    UnityEditorDebug(format);
                    break;
                case LogLevel.Warn:
                    UnityEditorWarning(format);
                    break;
                case LogLevel.Error:
                    UnityEditorError(format);
                    break;
                case LogLevel.Fatal:
                    UnityEditorError(format);
                    break;
                default:
                    break;
            }
        }

        public void WriteLine(LogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    UnityEditorDebug(message);
                    break;
                case LogLevel.Debug:
                    UnityEditorDebug(message);
                    break;
                case LogLevel.Info:
                    UnityEditorDebug(message);
                    break;
                case LogLevel.Warn:
                    UnityEditorWarning(message);
                    break;
                case LogLevel.Error:
                    UnityEditorError(message);
                    break;
                case LogLevel.Fatal:
                    UnityEditorError(message);
                    break;
                default:
                    break;
            }
        }


        public void WriteLine<T0>(LogLevel logLevel, string message, T0 propertyValue0)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    UnityEditorDebug(message, propertyValue0);
                    break;
                case LogLevel.Debug:
                    UnityEditorDebug(message, propertyValue0);
                    break;
                case LogLevel.Info:
                    UnityEditorDebug(message, propertyValue0);
                    break;
                case LogLevel.Warn:
                    UnityEditorWarning(message, propertyValue0);
                    break;
                case LogLevel.Error:
                    UnityEditorError(message, propertyValue0);
                    break;
                case LogLevel.Fatal:
                    UnityEditorError(message, propertyValue0);
                    break;
                default:
                    break;
            }
        }

        public void WriteLine<T0, T1>(LogLevel logLevel, string message, T0 propertyValue0, T1 propertyValue1)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    UnityEditorDebug(message, propertyValue0, propertyValue1);
                    break;
                case LogLevel.Debug:
                    UnityEditorDebug(message, propertyValue0, propertyValue1);
                    break;
                case LogLevel.Info:
                    UnityEditorDebug(message, propertyValue0, propertyValue1);
                    break;
                case LogLevel.Warn:
                    UnityEditorWarning(message, propertyValue0, propertyValue1);
                    break;
                case LogLevel.Error:
                    UnityEditorError(message, propertyValue0, propertyValue1);
                    break;
                case LogLevel.Fatal:
                    UnityEditorError(message, propertyValue0, propertyValue1);
                    break;
                default:
                    break;
            }
        }

        public void WriteLine<T0, T1, T2>(LogLevel logLevel, string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    UnityEditorDebug(message, propertyValue0, propertyValue1, propertyValue2);
                    break;
                case LogLevel.Debug:
                    UnityEditorDebug(message, propertyValue0, propertyValue1, propertyValue2);
                    break;
                case LogLevel.Info:
                    UnityEditorDebug(message, propertyValue0, propertyValue1, propertyValue2);
                    break;
                case LogLevel.Warn:
                    UnityEditorWarning(message, propertyValue0, propertyValue1, propertyValue2);
                    break;
                case LogLevel.Error:
                    UnityEditorError(message, propertyValue0, propertyValue1, propertyValue2);
                    break;
                case LogLevel.Fatal:
                    UnityEditorError(message, propertyValue0, propertyValue1, propertyValue2);
                    break;
                default:
                    break;
            }
        }

        #region Debug
        private void UnityEditorDebug(string format, params object[] args)
        {
            format = Format(LogLevel.Trace, _logChannel, format, args);

            if (args.Length <= 0)
                UnityEngine.Debug.Log(format);
            else
                UnityEngine.Debug.LogFormat(format, args);
        }

        private void UnityEditorWarning(string format, params object[] args)
        {
            format = Format(LogLevel.Trace, _logChannel, format, args);

            if (args.Length <= 0)
                UnityEngine.Debug.LogWarning(format);
            else
                UnityEngine.Debug.LogWarningFormat(format, args);
        }

        private void UnityEditorError(string format, params object[] args)
        {
            format = Format(LogLevel.Trace, _logChannel, format, args);

            if (args.Length <= 0)
                UnityEngine.Debug.LogError(format);
            else
                UnityEngine.Debug.LogErrorFormat(format, args);
        }

        private string Format(LogLevel level, string logChannel, string format, params object[] args)
        {
            string result = string.Empty;
            for (int i = _logDecorators.Count - 1; i >= 0; i--)
            {
                var formatLog = _logDecorators[i]?.Format(level, logChannel, format, args);
                if (_logDecorators[i] is UnityLogDecorator unityLogDecorator)
                {
                    result = formatLog;
                }
            }
            return result;
        }
        #endregion
    }
}