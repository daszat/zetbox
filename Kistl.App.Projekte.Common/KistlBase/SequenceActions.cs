
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

    [Implementor]
    public static class SequenceActions
    {
        [Invocation]
        public static void GetName(Sequence obj, MethodReturnEventArgs<string> e)
        {
            // TODO: Add "Name" property
            //e.Result = "Base.Sequences." + obj.Name;
        }
    }
}
