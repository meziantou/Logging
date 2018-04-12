// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.Logging.Testing
{
    public class TestSink : ITestSink
    {
        public TestSink(
            Func<WriteContext, bool> writeEnabled = null,
            Func<BeginScopeContext, bool> beginEnabled = null)
        {
            WriteEnabled = writeEnabled;
            BeginEnabled = beginEnabled;

            Scopes = new ConcurrentQueue<BeginScopeContext>();
            Writes = new ConcurrentQueue<WriteContext>();
        }

        public Func<WriteContext, bool> WriteEnabled { get; set; }

        public Func<BeginScopeContext, bool> BeginEnabled { get; set; }

        public ConcurrentQueue<BeginScopeContext> Scopes { get; set; }

        public ConcurrentQueue<WriteContext> Writes { get; set; }

        public void Write(WriteContext context)
        {
            if (WriteEnabled == null || WriteEnabled(context))
            {
                Writes.Enqueue(context);
            }
        }

        public void Begin(BeginScopeContext context)
        {
            if (BeginEnabled == null || BeginEnabled(context))
            {
                Scopes.Enqueue(context);
            }
        }

        public static bool EnableWithTypeName<T>(WriteContext context)
        {
            return context.LoggerName.Equals(typeof(T).FullName);
        }

        public static bool EnableWithTypeName<T>(BeginScopeContext context)
        {
            return context.LoggerName.Equals(typeof(T).FullName);
        }
    }
}