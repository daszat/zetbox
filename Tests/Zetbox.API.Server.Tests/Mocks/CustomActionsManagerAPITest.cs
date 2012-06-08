using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API;

namespace Zetbox.API.Server.Mocks
{
    public class CustomActionsManagerAPITest : ICustomActionsManager
    {
        public void Reset()
        {
        }

        public void Init(IReadOnlyZetboxContext ctx)
        {
            Reset();
        }
    }
}
