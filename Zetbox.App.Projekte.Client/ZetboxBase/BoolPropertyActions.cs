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
    public static class BoolPropertyActions
    {
        [Invocation]
        public static void NotifyCreated(Zetbox.App.Base.BoolProperty obj)
        {
            obj.ValueModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableBoolPropertyViewModel.Find(obj.Context);
        }
    }
}
