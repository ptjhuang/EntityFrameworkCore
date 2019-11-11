// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Diagnostics
{
    public interface ISimpleLogger
    {
        void Log([NotNull] EventData eventData);
        bool ShouldLog(EventId eventId, LogLevel logLevel);
    }
}
