// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class SimpleLoggerTests
    {
        [ConditionalTheory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Log_with_default_options(bool async)
        {
            var stream = new StringWriter();

            var actual = await LogTest(async, stream, b => b.LogTo(stream.WriteLine));

            Assert.Equal(
                @"info: <Local Date> HH:mm:ss.fff: Microsoft.EntityFrameworkCore.Infrastructure.ContextInitialized[10403]
      Entity Framework Core X.X.X-any initialized 'LoggingContext' using provider 'Microsoft.EntityFrameworkCore.InMemory' with options: StoreName=SimpleLoggerTests 
dbug: <Local Date> HH:mm:ss.fff: Microsoft.EntityFrameworkCore.Update.SaveChangesStarting[10004]
      SaveChanges starting for 'LoggingContext'.
dbug: <Local Date> HH:mm:ss.fff: Microsoft.EntityFrameworkCore.Update.SaveChangesCompleted[10005]
      SaveChanges completed for 'LoggingContext' with 0 entities written to the database.
dbug: <Local Date> HH:mm:ss.fff: Microsoft.EntityFrameworkCore.Infrastructure.ContextDisposed[10407]
      'LoggingContext' disposed.
",
                actual,
                ignoreLineEndingDifferences: true,
                ignoreWhiteSpaceDifferences: true);
        }

        private static async Task<string> LogTest(
            bool async,
            TextWriter writer,
            Func<DbContextOptionsBuilder, DbContextOptionsBuilder> configureLogging)
        {
            var options = configureLogging(
                    new DbContextOptionsBuilder()
                        .UseInMemoryDatabase("SimpleLoggerTests"))
                .Options;

            string productVersion;

            using (var context = new LoggingContext(options))
            {
                Assert.Equal(0, async ? await context.SaveChangesAsync() : context.SaveChanges());

                productVersion = context.Model.GetProductVersion();
            }

            var lines = writer.ToString()
                .Replace(productVersion, "X.X.X-any")
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var builder = new StringBuilder();
            foreach (var line in lines)
            {
                var normalized = line;

                if (!normalized.StartsWith(" ", StringComparison.Ordinal))
                {
                    var stampEnd = normalized.LastIndexOf(':');
                    normalized = normalized.Substring(0, 6) + "<Local Date> HH:mm:ss.fff" + normalized.Substring(stampEnd);
                }

                builder.AppendLine(normalized);
            }

            return builder.ToString();
        }

        private class LoggingContext : DbContext
        {
            public LoggingContext([NotNull] DbContextOptions options)
                : base(options)
            {
            }
        }
    }
}
