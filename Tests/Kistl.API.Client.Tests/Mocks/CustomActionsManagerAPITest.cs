using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.API.Client.Mocks
{
    public class CustomActionsManagerAPITest : ICustomActionsManager
    {
        public void Reset()
        {
        }

        public void Init(IKistlContext ctx)
        {
            Reset();
        }
    }
}
