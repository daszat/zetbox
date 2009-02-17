using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public class Template
        : Kistl.Server.Generators.Templates.Implementation.TypeBase
    {
        protected ObjectClass ObjectClass { get; private set; }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass t)
            : base(_host, ctx, t)
        {
            this.ObjectClass = t;
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            var baseClass = this.ObjectClass.BaseObjectClass;
            if (baseClass != null)
            {
                return baseClass.Module.Namespace + "." + baseClass.ClassName;
            }
            else
            {
                return "";
            }
        }

        // HACK: workaround the fact this is missing on the server
        // TODO: remove this and move the client action "OnGetInheritedMethods_ObjectClass" into a common action assembly
        private static void GetMethods(ObjectClass obj, List<Kistl.App.Base.Method> e)
        {
            if (obj.BaseObjectClass != null)
                GetMethods(obj.BaseObjectClass, e);
            e.AddRange(obj.Methods);
        }

        protected override IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            var inherited = new List<Kistl.App.Base.Method>();
            GetMethods(this.ObjectClass, inherited);
            // TODO: fix Default methods in DB, remove the filter here and remove them from TailTemplate
            return inherited.Where(m => !m.IsDefaultMethod());
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            this.Host.CallTemplate("Implementation.ObjectClasses.Tail", ctx, this.DataType);
        }

    }
}
