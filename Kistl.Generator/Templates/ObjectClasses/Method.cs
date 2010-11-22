
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;

    public partial class Method
    {
        protected Method(Arebis.CodeGeneration.IGenerationHost host)
            : base(host)
        {
            throw new NotSupportedException();
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, DataType implementor, Kistl.App.Base.Method m, int index)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            string indexSuffix = index == 0 ? String.Empty : index.ToString();
            string eventName = "On" + m.Name + indexSuffix + "_" + implementor.Name;

            host.CallTemplate("ObjectClasses.Method", ctx, implementor, m, index, indexSuffix, eventName);
        }

        protected virtual IEnumerable<string> GetMethodAttributes() { return new string[] { String.Format("[EventBasedMethod(\"{0}\")]", this.eventName) }; }

        protected string GetModifiers()
        {
            MemberAttributes attrs = ModifyMethodAttributes(0);
            return attrs.ToCsharp();
        }

        /// <summary>
        /// Gets a set of <see cref="MemberAttributes"/> for this method and returns an appropriate set for output.
        /// </summary>
        protected virtual MemberAttributes ModifyMethodAttributes(MemberAttributes methodAttributes)
        {
            var result = methodAttributes | MemberAttributes.Public;

            // only override on derived types
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
                return ret.ReturnedTypeAsCSharp();
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
