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
using Microsoft.ApplicationInsights.TraceListener;
using System.Diagnostics;

namespace AIASPNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Application Insights ILogger implementation
            builder.Logging.AddApplicationInsights(
                configureTelemetryConfiguration: (config) =>
                    config.ConnectionString = builder.Configuration.GetConnectionString("APPLICATIONINSIGHTS_CONNECTION_STRING"),
                    configureApplicationInsightsLoggerOptions: (options) => { }
            );

            // Application Insights TraceListener implementation
            Trace.Listeners.Add(new ApplicationInsightsTraceListener(builder.Configuration.GetValue<string>("APPLICATIONINSIGHTS_INSTRUMENTATION_KEY")));

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
