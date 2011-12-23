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
    using ViewModelDescriptors = Kistl.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// Sets the Property it's default ValueModelDescriptor.
    /// GUI Tasks - so Client Actions.
    /// Note: OnNotifyCreated should always be implemented on the client side. importing or deploying also calls this event.
    /// </summary>
    [Implementor]
    public static class IndexConstraintActions
    {
        [Invocation]
        public static void NotifyDeleting(IndexConstraint obj)
        {
            var ctx = obj.Context;
            obj.Properties.Clear();
        }
    }
}
