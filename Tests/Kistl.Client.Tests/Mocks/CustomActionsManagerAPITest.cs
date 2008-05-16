using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.Mocks
{
    public class CustomActionsManagerAPITest : Kistl.API.ICustomActionsManager
    {
        public void AttachEvents(Kistl.API.IDataObject obj)
        {
            // Do nothing!
        }

        public void Init()
        {
            // Do nothing!
        }
    }
}
