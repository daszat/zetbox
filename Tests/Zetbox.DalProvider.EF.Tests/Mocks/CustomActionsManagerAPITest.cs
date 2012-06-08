using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DalProvider.Ef.Mocks
{
    public class CustomActionsManagerAPITest : ICustomActionsManager
    {

        public void Reset()
        {
        }

        public void Init(IReadOnlyKistlContext ctx)
        {
            Reset();
        }
    }
}
