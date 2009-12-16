using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Tests
{
    public class CustomActionsManagerAPITest : Kistl.API.ICustomActionsManager
    {
        public void Init(IKistlContext ctx)
        {
            // Do nothing!
        }
    }
}
