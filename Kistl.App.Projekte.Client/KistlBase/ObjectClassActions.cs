
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
    public static class ObjectClassActions
    {
        [Invocation]
        public static void NotifyCreated(ObjectClass obj)
        {
            obj.DefaultViewModelDescriptor = ViewModelDescriptors.Kistl_Client_Presentables_DataObjectViewModel.Find(obj.Context);
        }
    }
}
