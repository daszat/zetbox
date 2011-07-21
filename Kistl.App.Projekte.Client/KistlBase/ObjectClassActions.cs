
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

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public static class ObjectClassActions
    {
        [Invocation]
        public static void NotifyCreated(ObjectClass obj)
        {
            obj.DefaultViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_DataObjectViewModel);
        }
    }
}
