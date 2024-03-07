using Serilog.Core;
using Serilog.Events;

namespace Cr7Sund.Logger
{
    public sealed class UnityTagEnricher : ILogEventEnricher
    {
        public const string UnityTagKey = "custom.UNITY_LOG_TAG";

        private readonly LogEventProperty _property;

        public UnityTagEnricher(string tag) =>
            _property = new LogEventProperty(UnityTagKey, new ScalarValue(tag));

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) =>
            logEvent.AddPropertyIfAbsent(_property);
    }
}