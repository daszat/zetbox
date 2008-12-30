using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Server.ObjectClasses
{
    public class ListProperty : Kistl.Server.Generators.Templates.Server.ObjectClasses.ListProperty
    {
        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, DataType containingType, Type type, String name, Property property)
            : base(_host, containingType, type, name, property)
        {
        }

        protected override void ApplyRequisitesTemplate()
        {
            base.ApplyRequisitesTemplate();
            CallTemplate("Server.ObjectClasses.EfListWrapper", containingType, type, name, property);
        }
    }
}
