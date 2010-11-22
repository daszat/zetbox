

namespace Kistl.DalProvider.Client.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    
    public partial class InvokeServerMethod
    {
        public InvokeServerMethod(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.DataType dt, Kistl.App.Base.Method m, int index, string indexSuffix, string eventName)
            : base(_host, ctx, dt, m, index, indexSuffix, eventName)
        {
        }

        public static new void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, DataType implementor, Kistl.App.Base.Method m, int index)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            string indexSuffix = index == 0 ? String.Empty : index.ToString();
            string eventName = "On" + m.Name + indexSuffix + "_" + implementor.Name;

            host.CallTemplate("ObjectClasses.InvokeServerMethod", ctx, implementor, m, index, indexSuffix, eventName);
        }
    }
}
