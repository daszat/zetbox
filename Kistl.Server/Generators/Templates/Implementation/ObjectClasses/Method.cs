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
    public class Method : Kistl.Server.Generators.Templates.Interface.DataTypes.Method
    {

        public Method(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.App.Base.Method m)
            : base(_host, ctx, m)
        {
        }

        protected override System.CodeDom.MemberAttributes ModifyMethodAttributes(System.CodeDom.MemberAttributes methodAttributes)
        {
            return base.ModifyMethodAttributes(methodAttributes) | MemberAttributes.Public;
        }

        protected override void ApplyBodyTemplate()
        {
            Host.CallTemplate("Implementation.ObjectClasses.MethodBody", ctx, m);
        }

    }
}
