// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator.Templates.ObjectClasses
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class Method
    {
        protected Method(Arebis.CodeGeneration.IGenerationHost host)
            : base(host)
        {
            throw new NotSupportedException("this constructor only exists to allow overrinding in a CST");
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, DataType implementor, Zetbox.App.Base.Method m, int index)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            string indexSuffix = index == 0 ? String.Empty : index.ToString();
            string eventName = "On" + m.Name + indexSuffix + "_" + implementor.Name;

            Call(host, ctx, implementor, m, index, indexSuffix, eventName);
        }

        protected virtual IEnumerable<string> GetMethodAttributes()
        {
            return new string[] { String.Format("[EventBasedMethod(\"{0}\")]", this.eventName) };
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            // Methods are usually virtual ...
            var result = base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;

            // ... and override on derived types
            if (this.m.ObjectClass != this.dt)
                result = result | MemberAttributes.Override;

            return result;
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

        protected virtual void ApplyBodyTemplate()
        {
            Host.WriteOutput(";");
        }
    }
}
