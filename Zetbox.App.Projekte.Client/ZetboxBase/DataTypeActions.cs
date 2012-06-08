
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
    public static class DataTypeActions
    {
        [Invocation]
        public static void NotifyDeleting(DataType obj)
        {
            var ctx = obj.Context;
            foreach (var prop in obj.Properties)
            {
                ctx.Delete(prop);
            }

            foreach (var m in obj.Methods)
            {
                ctx.Delete(m);
            }

            foreach (var c in obj.Constraints)
            {
                ctx.Delete(c);
            }

            obj.ImplementsInterfaces.Clear();
        }
    }
}
