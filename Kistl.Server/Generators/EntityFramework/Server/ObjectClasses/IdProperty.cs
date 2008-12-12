using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Server.ObjectClasses
{
    public partial class IdProperty
    {
#if INTELLISENSE
        protected Arebis.CodeGeneration.IGenerationHost Host;
        protected string ResolveResourceUrl(string template) { return "mock";  }

        protected Type type;
        protected string name;

        protected NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost h, Type type, string name) { }
#endif

        /// <summary>
        /// Is called to apply optional decoration in front of the property declaration, like Attributes.
        /// </summary>
        protected virtual void ApplyAttributeTemplate() { }

        protected virtual string MungeNameToBacking(string name)
        {
            return "_" + name;
        }

    }
}
