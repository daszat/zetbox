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
    public static class IntPropertyActions
    {
        [Invocation]
        public static void NotifyCreated(Kistl.App.Base.IntProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Int);
        }
    }
}
