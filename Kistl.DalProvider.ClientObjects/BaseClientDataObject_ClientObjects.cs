
namespace Kistl.DalProvider.ClientObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// ClientObjects specific BaseClientDataObject functionality.
    /// </summary>
    public abstract class BaseClientDataObject_ClientObjects
        : BaseClientDataObject
    {
        protected BaseClientDataObject_ClientObjects() : base(null) { }
        protected BaseClientDataObject_ClientObjects(Func<IReadOnlyKistlContext> lazyCtx) : base(lazyCtx) { }
    }
}
