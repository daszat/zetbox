using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API
{

    public static class ZetboxContextDebuggerSingleton
    {
        private readonly static object _lock = new object();

        private static IZetboxContextDebugger _Current;

        public static void SetDebugger(IZetboxContextDebugger debugger)
        {
            lock (_lock)
            {
                _Current = debugger;
            }
        }

        // TODO: Replace by central ServiceDiscoveryService
        public static IZetboxContextDebugger GetDebugger()
        {
            return _Current;
        }

        public static void Created(IZetboxContext ctx)
        {
            lock (_lock)
            {
                if (_Current != null)
                {
                    _Current.Created(ctx);
                }
            }
        }
    }
}
