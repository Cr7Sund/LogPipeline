using System;
using System.Text;
using UnityEngine;
namespace Cr7Sund.Logger
{
    internal class EventLogWriter : LogWriter<LogEventData>
    {
        public EventLogWriter(ILogFileFormatting formatter, MMFile mmFile) : base(formatter, mmFile)
        {
        }

        protected override FileLogType FileLogType
        {
            get
            {
                return FileLogType.Event;
            }
        }

        protected override string Formatting(string level, string id, LogEventData obj)
        {
            var sb = new StringBuilder();

            string preKey = string.Format("extra.{0}.{1}", level, id);

            foreach (var current in obj.info)
            {
                string key = string.Format("{0}.{1}", preKey, current.Key);
                sb.AppendLine(key);
            }
            jsonData.log_time = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-ddTHH:mm:ss.fffzzzz");
            jsonData.log_level = level;
            jsonData.log_info = sb.ToString();

            string output = JsonUtility.ToJson(jsonData);
            return output;
        }
    }
}
