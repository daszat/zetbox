using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.API.Server.Mocks
{
    public class CustomActionsManagerAPITest : Kistl.API.ICustomActionsManager
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
