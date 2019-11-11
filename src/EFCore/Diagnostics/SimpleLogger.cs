// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.EntityFrameworkCore.Diagnostics
{
    [Flags]
    public enum SimpleLoggerFormatOptions
    {
        /// <summary>
        /// X
        /// </summary>
        None = 0,

        /// <summary>
        /// X
        /// </summary>
        SingleLine = 1,

        /// <summary>
        /// X
        /// </summary>
        Level = 1 << 1,

        /// <summary>
        /// X
        /// </summary>
        Category = 1 << 2,

        /// <summary>
        /// X
        /// </summary>
        Name = 1 << 3,

        /// <summary>
        /// X
        /// </summary>
        Id = 1 << 4,

        /// <summary>
        /// X
        /// </summary>
        UtcTime = 1 << 5,

        /// <summary>
        /// X
        /// </summary>
        LocalTime = 1 << 6,

        /// <summary>
        /// X
        /// </summary>
        Default = Level | Category | Name | Id | LocalTime
    }

    public class SimpleLogger : ISimpleLogger
    {
        public SimpleLogger(
            [NotNull] Action<string> sink,
            [NotNull] Func<EventId, LogLevel, bool> filter,
            SimpleLoggerFormatOptions formatOptions)
        {
            FormatOptions = formatOptions;
            Sink = sink;
            Filter = filter;
        }

        public virtual SimpleLoggerFormatOptions FormatOptions { get; }
        public virtual Action<string> Sink { get; }
        public virtual Func<EventId, LogLevel, bool> Filter { get; }

        public virtual void Log(EventData eventData)
        {
            var message = eventData.ToString();
            var logLevel = eventData.LogLevel;
            var eventId = eventData.EventId;

            if (FormatOptions != SimpleLoggerFormatOptions.None)
            {
                var singleLine = (FormatOptions & SimpleLoggerFormatOptions.SingleLine) != 0;
                if (singleLine)
                {
                    message = message.Replace(Environment.NewLine, "");
                }

                if ((int)FormatOptions > 1)
                {
                    var messageBuilder = new StringBuilder();
                    if ((FormatOptions & SimpleLoggerFormatOptions.Level) != 0)
                    {
                        messageBuilder.Append(GetLogLevelString(logLevel));
                    }

                    if ((FormatOptions & SimpleLoggerFormatOptions.LocalTime) != 0)
                    {
                        var now = DateTime.Now;
                        messageBuilder.Append(now.ToShortDateString()).Append(now.ToString(" HH:mm:ss.fff")).Append(": ");
                    }

                    if ((FormatOptions & SimpleLoggerFormatOptions.UtcTime) != 0)
                    {
                        messageBuilder.Append(DateTime.UtcNow.ToString("o")).Append(": ");
                    }

                    var lastDot = eventId.Name.LastIndexOf('.');
                    if (lastDot > 0)
                    {
                        var includeCategory = (FormatOptions & SimpleLoggerFormatOptions.Category) != 0;
                        var includeName = (FormatOptions & SimpleLoggerFormatOptions.Name) != 0;

                        if (includeCategory && includeName)
                        {
                            messageBuilder.Append(eventId.Name);
                        }
                        else if (includeCategory)
                        {
                            messageBuilder.Append(eventId.Name.Substring(0, lastDot));
                        }
                        else if (includeName)
                        {
                            messageBuilder.Append(eventId.Name.Substring(lastDot + 1));
                        }
                    }

                    if ((FormatOptions & SimpleLoggerFormatOptions.Id) != 0)
                    {
                        messageBuilder.Append('[').Append(eventId.Id).Append(']');
                    }

                    const string padding = "      ";

                    message = singleLine
                        ? messageBuilder
                            .Append(" => ")
                            .Append(message)
                            .ToString()
                        : messageBuilder
                            .AppendLine()
                            .Append(padding)
                            .Append(message.Replace(Environment.NewLine, Environment.NewLine + padding))
                            .ToString();
                }
            }

            Sink(message);
        }

        public virtual bool ShouldLog(EventId eventId, LogLevel logLevel) => Filter(eventId, logLevel);

        private static string GetLogLevelString(LogLevel logLevel)
            => logLevel switch
            {
                LogLevel.Trace => "trce: ",
                LogLevel.Debug => "dbug: ",
                LogLevel.Information => "info: ",
                LogLevel.Warning => "warn: ",
                LogLevel.Error => "fail: ",
                LogLevel.Critical => "crit: ",
                _ => "none",
            };
    }
}

