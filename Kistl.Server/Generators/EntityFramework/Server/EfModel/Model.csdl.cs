using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.GeneratorsOld;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Server.EfModel
{
    public partial class ModelCsdl
    {
        private static string GetAssociationChildEntitySetName(Property prop)
        {
            TypeMoniker childType = Construct.AssociationChildType(prop);
            if (!prop.IsList)
            {
                return prop.Context.GetQuery<ObjectClass>().First(c => childType.ClassName == c.ClassName).GetRootClass().ClassName;
            }
            else
            {
                return childType.ClassName;
            }
        }

        protected virtual void ApplyEntityTypeFieldDefs(IEnumerable<BaseProperty> properties)
        {
            CallTemplate("Server.EfModel.ModelCsdlEntityTypeFields", properties);
        }
    }
}
