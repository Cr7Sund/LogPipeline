using Serilog;

#if ELASTIC_SEARCH
using Serilog.Sinks.Elasticsearch;
#endif


namespace Cr7Sund.Logger
{
    public class SerilogLoggerConfigFactory
    {
        private const string OutputTemplate = "{Timestamp:HH:mm:ss} [{LogChannel}] [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        public static LoggerConfiguration Create(LogSinkType logSinkType, string logChannel)
        {
#if UNITY_EDITOR
            return EditorConfig(logSinkType, logChannel);
#elif PROFILER
            return EditorConfig(logSinkType,logChannel);
#elif FINAL_RELEASE
    return  FinalReleaseConfig(logSinkType,logChannel);
#else
            return new LoggerConfiguration();
#endif
        }

        private static LoggerConfiguration EditorConfig(LogSinkType logSinkType, string logChannel)
        {
            Serilog.Debugging.SelfLog.Enable(msg => UnityEngine.Debug.LogError(msg));

            var loggerConfiguration = new LoggerConfiguration();
            loggerConfiguration.MinimumLevel.Verbose();
            loggerConfiguration.Enrich.WithColorLog();
            loggerConfiguration.Enrich.WithLogChannel(logChannel);

            if ((logSinkType & LogSinkType.Local) == LogSinkType.Local)
            {
                loggerConfiguration = loggerConfiguration.WriteTo.Unity(outputTemplate: OutputTemplate);
            }
            if ((logSinkType & LogSinkType.File) == LogSinkType.File)
            {
                loggerConfiguration.WriteTo.File(
                                    new Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter(renderMessageTemplate: true),
                                    "logs / log.txt",
                                    shared: true,
                                    fileSizeLimitBytes: 524288000,
                                    rollingInterval: RollingInterval.Day,
                                    retainedFileCountLimit: 2,
                                    rollOnFileSizeLimit: true);

            }
            if ((logSinkType & LogSinkType.Net) == LogSinkType.Net)
            {
                loggerConfiguration = loggerConfiguration.WriteTo.UDP("localhost", 7071,
                            System.Net.Sockets.AddressFamily.InterNetwork, enableBroadcast: true,
                            outputTemplate: OutputTemplate);
            }
            if ((logSinkType & LogSinkType.LogPlatform) == LogSinkType.LogPlatform)
            {
#if ELASTIC_SEARCH
                loggerConfiguration = loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")));
#endif
            }


            return loggerConfiguration;

        }

        private static LoggerConfiguration FinalReleaseConfig(LogSinkType logSinkType, string logChannel)
        {
            var loggerConfiguration = new LoggerConfiguration();
            loggerConfiguration.Enrich.WithLogChannel(logChannel);

            if ((logSinkType & LogSinkType.Local) == LogSinkType.Local)
            {
                // loggerConfiguration = loggerConfiguration.WriteTo.Unity3D();
            }
            if ((logSinkType & LogSinkType.File) == LogSinkType.File)
            {

            }
            if ((logSinkType & LogSinkType.Net) == LogSinkType.Net)
            {
#if ELASTIC_SEARCH
                loggerConfiguration = loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")));
#endif
            }

            return loggerConfiguration;
        }
    }
}