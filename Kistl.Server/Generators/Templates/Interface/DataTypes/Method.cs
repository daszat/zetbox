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

namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    public partial class Method
    {
        protected string GetModifiers()
        {
            MemberAttributes attrs = ModifyMethodAttributes(0);
            return attrs.ToCsharp();
        }

        /// <summary>
        /// Gets a set of <see cref="MethodAttributs"/> and returns an appropriate set for output.
        /// </summary>
        protected virtual MemberAttributes ModifyMethodAttributes(MemberAttributes methodAttributes)
        {
            // interface methods cannot be overridden
            return methodAttributes | MemberAttributes.Final;
        }

        protected virtual string GetReturnType()
        {
            // TODO: repair after implementing a common (Client&Server) MethodInvocation assembly
            var ret = m.Parameter.SingleOrDefault(param => param.IsReturnParameter);
            if (ret == null)
            {
                return "void";
            }
            else
            {
                return ret.GetParameterTypeString();
            }
        }

        protected virtual string GetParameterDefinition(BaseParameter param)
        {
            return param.GetParameterDefinition();
        }

        protected virtual string GetParameterDefinitions()
        {
            return m.GetParameterDefinitions();
        }

        protected virtual void ApplyBodyTemplate() {
            Host.WriteOutput(";");
        }
    }
}
