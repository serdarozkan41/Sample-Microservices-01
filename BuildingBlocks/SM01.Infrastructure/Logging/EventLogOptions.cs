﻿namespace SM01.Infrastructure.Logging
{
    public class EventLogOptions
    {
        public bool IsEnabled { get; set; }

        public string LogName { get; set; }

        public string SourceName { get; set; }
    }
}
