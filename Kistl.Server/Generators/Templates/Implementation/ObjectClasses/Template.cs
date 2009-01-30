using System;
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

        /// <returns>The interfaces this class implements</returns>
        protected override string[] GetInterfaces()
        {
            return base.GetInterfaces().Concat(new string[] { this.ObjectClass.ClassName }).ToArray();
        }

    }
}
