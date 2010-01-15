using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Mocks
{
    internal class CustomActionsManagerClientTests : ICustomActionsManager
    {
        #region ICustomActionsManager Members

        public void Init(IReadOnlyKistlContext ctx)
        {
            // ignore
        }

        #endregion
    }
}
