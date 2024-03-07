# Cr7Sund Log framework

Status: FrameWork

# About

Cr7SundLog is a logging framework designed for use in Unity. It enables structured logging and can be customized to meet various needs while having a minimal impact on application performance. The framework includes two log providers: a common one based on the widely-used [Serilog](https://github.com/serilog/serilog/wiki/Getting-Started), which offers a range of sinks and enrichers for appending data.And the other basic one that is lightweight and does not require any plugins. Cr7SundLog supports both local and remote logging, so you can receive log messages from other devices over the air. Additionally, decorators can be added to support more data formats or devices.

# **Quick Start**

Use the [SampleLoggerProxy](https://github.com/Cr7Sund/LogPipeline/blob/bc9537c6897c94fe085a2b9ec2d3abe8aa85b358/Sample~/SampleLoggerProxy.cs) directyly. 

```csharp
            string customChannel = "Sample";
            int userId = 24;
            var logger = new SampleLoggerProxy(customChannel);

            logger.Info("HelloWorld");
            logger.Debug("Hello World");
            logger.Debug("Hello {UserID}", userId);
            logger.Debug("Hello {0} again", userId);
```

[SampleLogger.cs](https://github.com/Cr7Sund/LogPipeline/blob/bc9537c6897c94fe085a2b9ec2d3abe8aa85b358/Sample~/SampleLog.cs)

# Setup

1. Reference Cr7Sund.Logger assembly
2. Create your loggerProxy 

```csharp
         /// <summary>
        /// 
        /// </summary>
        /// <param name="logChannel">each log's custom channel</param>
        public SampleLoggerProxy(string logChannel)
        {
            var logProvider = Logger.LogProviderFactory.Create();
            _logProvider = logProvider;

            // choose your output device
            logProvider.Init(LogSinkType.File | LogSinkType.Net | LogSinkType.LogPlatform, logChannel);
            // the minimum default on log level
            _miniumLogLevel = LogLevel.Trace;
        }
```

You can also use a factory method to create logger saved for later use by business classes. Multiple loggers can be created and used independently if required.

1. Then you you can log as usual. You can find or append the log entris in [interface](https://github.com/Cr7Sund/LogPipeline/blob/bc9537c6897c94fe085a2b9ec2d3abe8aa85b358/Sample~/ISampleInternalLog.cs).

```csharp
logger.Info("HelloWorld");

```

