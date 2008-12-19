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

namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    public class Method : Kistl.Server.Generators.Templates.Interface.DataTypes.Method
    {

        public Method(Arebis.CodeGeneration.IGenerationHost _host, Kistl.App.Base.Method m)
            : base(_host, m)
        {
        }

        protected override System.CodeDom.MemberAttributes ModifyMethodAttributes(System.CodeDom.MemberAttributes methodAttributes)
        {
            return base.ModifyMethodAttributes(methodAttributes) | MemberAttributes.Public;
        }

        protected override void ApplyBodyTemplate()
        {
            Host.CallTemplate("Server.ObjectClasses.MethodBody", m);
        }

    }
}
