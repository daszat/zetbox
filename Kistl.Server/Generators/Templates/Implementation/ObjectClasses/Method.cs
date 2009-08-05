using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public class Method
        : Kistl.Server.Generators.Templates.Interface.DataTypes.Method
    {
        internal static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, DataType cls, Kistl.App.Base.Method m, int index)
        {
            host.CallTemplate("Implementation.ObjectClasses.Method", ctx, cls, m, index);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, DataType cls, Kistl.App.Base.Method m)
        {
            Call(host, ctx, cls, m, 0);
        }

        protected DataType DataType { get; private set; }

        public Method(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, DataType cls, Kistl.App.Base.Method m, int index)
            : base(_host, ctx, m, index)
        {
            this.DataType = cls;
        }

        protected override IEnumerable<string> GetMethodAttributes()
        {
            string indexSuffix = index == 0 ? String.Empty : index.ToString();
            string eventName = "On" + m.MethodName + indexSuffix + "_" + this.DataType.ClassName;
            
            return base.GetMethodAttributes().Concat(new[] { String.Format("[EventBasedMethod(\"{0}\")]", eventName) });
        }

        protected override System.CodeDom.MemberAttributes ModifyMethodAttributes(System.CodeDom.MemberAttributes methodAttributes)
        {
            var result = base.ModifyMethodAttributes(methodAttributes) | MemberAttributes.Public;

            result = result & ~MemberAttributes.Final;

            // only override on derived types
            if (this.m.ObjectClass != this.DataType)
                result = result | MemberAttributes.Override;

            return result;

        }

        protected override void ApplyBodyTemplate()
        {
            Implementation.ObjectClasses.MethodBody.Call(Host, ctx, this.DataType, m, this.index);
        }
    }
}
