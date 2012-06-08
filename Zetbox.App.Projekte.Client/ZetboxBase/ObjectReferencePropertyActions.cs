namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.App.Extensions;
    using Kistl.Client;

    /// <summary>
    /// Client implementation
    /// Sets the Property it's default ValueModelDescriptor.
    /// GUI Tasks - so Client Actions.
    /// Note: OnNotifyCreated should always be implemented on the client side. importing or deploying also calls this event.
    /// </summary>
    [Implementor]
    public static class ObjectReferencePropertyActions
    {
        [Invocation]
        public static void NotifyCreated(Kistl.App.Base.ObjectReferenceProperty obj)
        {
            // Is implemented by CreateNavigator
            // At creating time there is no way to discover if the navigator is a Reference or List
        }
    }
}
