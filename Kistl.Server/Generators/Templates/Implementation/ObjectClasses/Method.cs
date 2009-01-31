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
        protected DataType DataType { get; private set; }

        public Method(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, DataType cls, Kistl.App.Base.Method m)
            : base(_host, ctx, m)
        {
            this.DataType = cls;
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
            Host.CallTemplate("Implementation.ObjectClasses.MethodBody", ctx, DataType, m);
        }

    }
}
