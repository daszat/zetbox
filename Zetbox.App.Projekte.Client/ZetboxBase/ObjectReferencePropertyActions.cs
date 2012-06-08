namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.App.Extensions;
    using Zetbox.Client;

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
        public static void NotifyCreated(Zetbox.App.Base.ObjectReferenceProperty obj)
        {
            // Is implemented by CreateNavigator
            // At creating time there is no way to discover if the navigator is a Reference or List
        }
    }
}
