﻿namespace SM01.Infrastructure.Logging
{
    public class LoggingOptions
    {
        public Dictionary<string, string> LogLevel { get; set; }

        public FileOptions File { get; set; }

        public EventLogOptions EventLog { get; set; }
    }
}
