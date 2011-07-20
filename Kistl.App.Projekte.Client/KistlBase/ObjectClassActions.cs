
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;
    using Kistl.Client.Presentables;
    using Kistl.App.GUI;
    using Kistl.Client;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class ObjectClassActions
    {
        private static IViewModelFactory _mdlFactory = null;

        public ObjectClassActions(IViewModelFactory mdlFactory)
        {
            _mdlFactory = mdlFactory;
        }

        [Invocation]
        public static void NotifyCreated(ObjectClass obj)
        {
            obj.DefaultViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_DataObjectViewModel);
        }
    }
}
