
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client;
    using Zetbox.Client.Presentables;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public static class ObjectClassActions
    {
        [Invocation]
        public static void NotifyCreated(ObjectClass obj)
        {
            obj.DefaultViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_DataObjectViewModel.Find(obj.Context);
        }

        [Invocation]
        public static void NotifyDeleting(ObjectClass obj)
        {
            var ctx = obj.Context;
            foreach (var ac in obj.AccessControlList)
            {
                ctx.Delete(ac);
            }

            foreach (var f in obj.FilterConfigurations)
            {
                ctx.Delete(f);
            }

            foreach (var cls in obj.SubClasses)
            {
                ctx.Delete(cls);
            }

            foreach (var rel in obj.GetRelations())
            {
                ctx.Delete(rel);
            }
        }
    }
}
