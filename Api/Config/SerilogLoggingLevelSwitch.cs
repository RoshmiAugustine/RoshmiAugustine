// -----------------------------------------------------------------------
// <copyright file="SerilogLoggingLevelSwitch.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Config
{
    using Serilog.Core;
    using Serilog.Events;

    /// <summary>
    /// Serilog Logging Level Switch to allow for dynamic changes to logging level at runtime.
    /// </summary>
    public static class SerilogLoggingLevelSwitch
    {
        /// <summary>
        /// LevelSwitch.
        /// </summary>
        public static LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch();

        /// <summary>
        /// Set minimum logging level.
        /// </summary>
        /// <param name="eventLevel">log event level.</param>
        public static void SetLoggingLevel(int eventLevel)
        {
            switch (eventLevel)
            {
                case 0:
                    levelSwitch.MinimumLevel = LogEventLevel.Verbose;
                    break;
                case 1:
                    levelSwitch.MinimumLevel = LogEventLevel.Debug;
                    break;
                case 2:
                    levelSwitch.MinimumLevel = LogEventLevel.Information;
                    break;
                case 3:
                    levelSwitch.MinimumLevel = LogEventLevel.Warning;
                    break;
                case 4:
                    levelSwitch.MinimumLevel = LogEventLevel.Error;
                    break;
                case 5:
                    levelSwitch.MinimumLevel = LogEventLevel.Fatal;
                    break;
            }
        }
    }
}
