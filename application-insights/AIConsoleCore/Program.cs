//===============================================================================
// Microsoft FastTrack for Azure
// Application Insights Demos
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.TraceListener;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AIConsoleCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Application Insights ILogger Implementation
            using ITelemetryChannel channel = new InMemoryChannel();
            TelemetryConfiguration telemetryConfigurationOptions = new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration()
            {
                ConnectionString = config.GetConnectionString("APPLICATIONINSIGHTS_CONNECTION_STRING"),
                TelemetryChannel = channel
            };
            ApplicationInsightsLoggerOptions loggerOptions = new ApplicationInsightsLoggerOptions();

            ILoggerFactory factory = new LoggerFactory();
            factory.AddProvider(new ApplicationInsightsLoggerProvider(Options.Create(telemetryConfigurationOptions), Options.Create(loggerOptions)));
            ILogger logger = factory.CreateLogger("Program");

            // Application Insights TraceListener implementation
            Trace.Listeners.Add(new ApplicationInsightsTraceListener(config.GetValue<string>("APPLICATIONINSIGHTS_INSTRUMENTATION_KEY")));

            logger.LogInformation("This is an informational message from the ILogger interface.");
            Trace.TraceInformation("This is an informational message from the TraceListener interface.");

            logger.LogError(new ApplicationException("This is an application exception logged by the ILogger interface."), string.Empty, null);
            Trace.TraceError("This is an application exception logged by the TraceListener interface.");

            Console.WriteLine("Hello, World!");
            channel.Flush();
            Trace.Flush();
        }
    }
}
