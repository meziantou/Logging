using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Microsoft.Extensions.Logging.Testing
{
    public interface ITestSink
    {
        Func<WriteContext, bool> WriteEnabled { get; set; }

        Func<BeginScopeContext, bool> BeginEnabled { get; set; }

        ConcurrentQueue<BeginScopeContext> Scopes { get; set; }

        ConcurrentQueue<WriteContext> Writes { get; set; }

        void Write(WriteContext context);

        void Begin(BeginScopeContext context);
    }
}
