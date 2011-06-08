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
    public static class CompoundObjectPropertyActions
    {
        [Invocation]
        public static void NotifyCreated(Kistl.App.Base.CompoundObjectProperty obj)
        {
            obj.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_CompoundObjectPropertyViewModel);
        }

        [Invocation]
        public static void postSet_CompoundObjectDefinition(CompoundObjectProperty obj, PropertyPostSetterEventArgs<CompoundObject> e)
        {
            var def = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_CompoundObjectPropertyViewModel);
            if (obj.ValueModelDescriptor == def && e.OldValue == null && e.NewValue != null && e.NewValue.DefaultPropertyViewModelDescriptor != null)
            {
                // Only once, during initialize
                obj.ValueModelDescriptor = e.NewValue.DefaultPropertyViewModelDescriptor;
            }
        }
    }
}
