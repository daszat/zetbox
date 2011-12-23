
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client;
    using Kistl.Client.Presentables;
    using ViewModelDescriptors = Kistl.NamedObjects.Gui.ViewModelDescriptors;

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
        }
    }
}
