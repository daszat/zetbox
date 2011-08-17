namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client;

    [Implementor]
    public static class CalculatedObjectReferencePropertyActions
    {
        [Invocation]
        public static void NotifyCreated(Kistl.App.Base.CalculatedObjectReferenceProperty obj)
        {
            // At creating time there is no way to discover if the navigator is a Reference or List
        }
    }
}
