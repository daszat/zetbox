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
    public static class PropertyActions
    {
        [Invocation]
        public static void NotifyDeleting(Property obj)
        {
            var ctx = obj.Context;
            foreach (var c in obj.Constraints)
            {
                ctx.Delete(c);
            }

            if (obj.DefaultValue != null) ctx.Delete(obj.DefaultValue);
            if (obj.FilterConfiguration != null) ctx.Delete(obj.FilterConfiguration);
        }
    }
}
