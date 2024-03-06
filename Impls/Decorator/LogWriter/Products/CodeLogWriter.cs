using System;
using UnityEngine;
namespace Cr7Sund.Logger
{
    public class JsonData
    {
        public string log_time;
        public string log_level;
        public string log_info;
    }
    internal class CodeLogWriter : LogWriter<string>
    {
        public CodeLogWriter(ILogFileFormatting formatter, MMFile mmFile) : base(formatter, mmFile)
        {
        }

        protected override FileLogType FileLogType
        {
            get
            {
                return FileLogType.Code;
            }
        }

        protected override string Formatting(string level, string id, string msg)
        {
            jsonData.log_time = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-ddTHH:mm:ss.fffzzzz");
            jsonData.log_level = level;
            jsonData.log_info = msg;

            string output = JsonUtility.ToJson(jsonData);
            return output;
        }
    }
}
