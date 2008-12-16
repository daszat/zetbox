using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Server.ObjectClasses
{
    public class IdProperty : Kistl.Server.Generators.Templates.Server.ObjectClasses.NotifyingValueProperty
    {


        public IdProperty(Arebis.CodeGeneration.IGenerationHost _host, Type type, String name)
            : base(_host, type, name)
        {

        }

        /// <summary>
        /// Is called to apply optional decoration in front of the property declaration, like Attributes.
        /// </summary>
        protected override void ApplyAttributeTemplate() { }

        protected override string MungeNameToBacking(string name)
        {
            return "_" + name;
        }

    }
}
