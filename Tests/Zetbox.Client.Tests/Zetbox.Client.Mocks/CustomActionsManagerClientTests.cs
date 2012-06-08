using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API;

namespace Zetbox.Client.Mocks
{
    internal class CustomActionsManagerClientTests : ICustomActionsManager
    {
        #region ICustomActionsManager Members

        public void Init(IReadOnlyZetboxContext ctx)
        {
            // ignore
        }

        #endregion
    }
}
