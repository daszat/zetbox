namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

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
