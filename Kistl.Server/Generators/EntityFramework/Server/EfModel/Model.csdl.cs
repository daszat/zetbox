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

        protected virtual IQueryable<ObjectClass> GetBaseClasses()
        {
            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass == null);
        }

        protected virtual IQueryable<ObjectClass> GetDerivedClasses()
        {
            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass != null);
        }

        protected virtual IQueryable<ObjectReferenceProperty> GetObjectReferencePropertiesWithStorage()
        {
            return ctx.GetQuery<ObjectReferenceProperty>()
                .Where(prop => prop.ObjectClass is ObjectClass)
                .ToList() // TODO: once HasStorage is no extension method anymore, delete this line and combine the WHERE
                .Where(prop => prop.HasStorage())
                .AsQueryable();
        }

        protected virtual IQueryable<Property> GetObjectListPropertiesWithStorage()
        {
            return ctx.GetQuery<Property>()
                .Where(prop => prop.ObjectClass is ObjectClass && prop.IsList)
                .ToList() // TODO: once HasStorage is no extension method anymore, delete this line and combine the WHERE
                .Where(prop => prop.HasStorage())
                .AsQueryable();
        }
    }
}
