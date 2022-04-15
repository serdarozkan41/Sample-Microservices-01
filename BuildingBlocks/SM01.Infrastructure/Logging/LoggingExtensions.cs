﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;
using Serilog.Exceptions;
using System.Reflection;

namespace SM01.Infrastructure.Logging
{
    public static class LoggingExtensions
    {
        private static void UseSMLogger(this IWebHostEnvironment env, LoggingOptions options)
        {
            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;

            var logsPath = Path.Combine(env.ContentRootPath, "logs");
            Directory.CreateDirectory(logsPath);
            var loggerConfiguration = new LoggerConfiguration();

            loggerConfiguration = loggerConfiguration
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.With<ActivityEnricher>()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProperty("Assembly", assemblyName)
                .Enrich.WithProperty("Application", env.ApplicationName)
                .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                .Enrich.WithProperty("ContentRootPath", env.ContentRootPath)
                .Enrich.WithProperty("WebRootPath", env.WebRootPath)
                .Enrich.WithExceptionDetails()
                .Filter.ByIncludingOnly((logEvent) =>
                {
                    if (logEvent.Level >= options.File.MinimumLogEventLevel)
                    {
                        var sourceContext = logEvent.Properties.ContainsKey("SourceContext")
                             ? logEvent.Properties["SourceContext"].ToString()
                             : null;

                        var logLevel = GetLogLevel(sourceContext, options);

                        return logEvent.Level >= logLevel;
                    }

                    return false;
                })
                .WriteTo.File(Path.Combine(logsPath, "log.txt"),
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] [TraceId: {TraceId}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: options.File.MinimumLogEventLevel);

           

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static LoggingOptions SetDefault(LoggingOptions options)
        {
            options ??= new LoggingOptions
            {
            };

            options.LogLevel ??= new Dictionary<string, string>();

            if (!options.LogLevel.ContainsKey("Default"))
            {
                options.LogLevel["Default"] = "Warning";
            }

            options.File ??= new FileOptions
            {
                MinimumLogEventLevel = Serilog.Events.LogEventLevel.Warning,
            };

        
            options.EventLog ??= new EventLogOptions
            {
                IsEnabled = false,
            };
            return options;
        }

        private static Serilog.Events.LogEventLevel GetLogLevel(string context, LoggingOptions options)
        {
            context = context.Replace("\"", string.Empty);
            string level = "Default";
            var matches = options.LogLevel.Keys.Where(k => context.StartsWith(k));

            if (matches.Any())
            {
                level = matches.Max();
            }

            return (Serilog.Events.LogEventLevel)Enum.Parse(typeof(Serilog.Events.LogEventLevel), options.LogLevel[level], true);
        }

        public static IWebHostBuilder UseSMLogger(this IWebHostBuilder builder, Func<IConfiguration, LoggingOptions> logOptions)
        {
            builder.ConfigureLogging((context, logging) =>
            {

                logging.AddSerilog();

                LoggingOptions options = SetDefault(logOptions(context.Configuration));

                if (options.EventLog != null && options.EventLog.IsEnabled)
                {
                    logging.AddEventLog(new EventLogSettings
                    {
                        LogName = options.EventLog.LogName,
                        SourceName = options.EventLog.SourceName,
                    });
                }

                context.HostingEnvironment.UseSMLogger(options);
            });

            return builder;
        }

        public static IHostBuilder UseSMLogger(this IHostBuilder builder, Func<IConfiguration, LoggingOptions> logOptions)
        {
            builder.ConfigureLogging((context, logging) =>
            {

                logging.AddSerilog();

                LoggingOptions options = SetDefault(logOptions(context.Configuration));

                if (options.EventLog != null && options.EventLog.IsEnabled)
                {
                    logging.AddEventLog(new EventLogSettings
                    {
                        LogName = options.EventLog.LogName,
                        SourceName = options.EventLog.SourceName,
                    });
                }

                context.HostingEnvironment.UseSMLogger(options);
            });

            return builder;
        }

        private static void UseSMLogger(this IHostEnvironment env, LoggingOptions options)
        {
            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;

            var logsPath = Path.Combine(env.ContentRootPath, "logs");
            Directory.CreateDirectory(logsPath);
            var loggerConfiguration = new LoggerConfiguration();

            loggerConfiguration = loggerConfiguration
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.With<ActivityEnricher>()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProperty("Assembly", assemblyName)
                .Enrich.WithProperty("Application", env.ApplicationName)
                .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                .Enrich.WithProperty("ContentRootPath", env.ContentRootPath)
                .Enrich.WithExceptionDetails()
                .Filter.ByIncludingOnly((logEvent) =>
                {
                    if (logEvent.Level >= options.File.MinimumLogEventLevel)
                    {
                        var sourceContext = logEvent.Properties.ContainsKey("SourceContext")
                             ? logEvent.Properties["SourceContext"].ToString()
                             : null;

                        var logLevel = GetLogLevel(sourceContext, options);

                        return logEvent.Level >= logLevel;
                    }

                    return false;
                })
                .WriteTo.File(Path.Combine(logsPath, "log.txt"),
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] [TraceId: {TraceId}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: options.File.MinimumLogEventLevel);

           
            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}
