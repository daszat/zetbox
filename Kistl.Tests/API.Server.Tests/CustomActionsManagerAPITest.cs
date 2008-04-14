using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace API.Server.Tests
{
    public class CustomActionsManagerAPITest : Kistl.API.ICustomActionsManager
    {
        private List<IDataObject> attachedObjects = new List<IDataObject>();

        public bool IsObjectAttached(IDataObject obj)
        {
            return attachedObjects.Contains(obj);
        }

        public void Reset()
        {
            attachedObjects.Clear();
        }

        public void AttachEvents(Kistl.API.IDataObject obj)
        {
            attachedObjects.Add(obj);
        }

        public void Init()
        {
            Reset();
        }
    }
}
