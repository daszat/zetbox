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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public class UpdateParentTemplateParams
    {
        public string IfType { get; set; }
        public string PropertyName { get; set; }
    }

    public partial class UpdateParentTemplate
    {
        public static void Call(IGenerationHost host, IZetboxContext ctx,
            DataType dataType)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (dataType == null) { throw new ArgumentNullException("dataType"); }

            var props = dataType
                .Properties
                .OfType<ObjectReferenceProperty>()
                .OrderBy(p => p.Name)
                .Where(p =>
                {
                    Relation rel = RelationExtensions.Lookup(ctx, p);
                    //RelationEnd relEnd = rel.GetEnd(p);

                    return (rel.Storage == StorageType.MergeIntoA && rel.A.Navigator == p)
                        || (rel.Storage == StorageType.MergeIntoB && rel.B.Navigator == p);
                })
                .Select(p => new UpdateParentTemplateParams()
                {
                    PropertyName = p.Name,
                    IfType = string.Format("{0}.{1}", p.GetReferencedObjectClass().Module.Namespace, p.GetReferencedObjectClass().Name)
                })
                .ToList();

            host.CallTemplate("ObjectClasses.UpdateParentTemplate",
                props);
        }

        protected virtual string GetPropertyBackingStore(string propName)
        {
            return "_fk_" + propName;
        }

        protected virtual string GetParentObjExpression(string ifType)
        {
            return "parentObj == null ? (int?)null : parentObj.ID";
        }

        protected virtual void ApplyCase(UpdateParentTemplateParams prop)
        {
            string propertyBackingStore = GetPropertyBackingStore(prop.PropertyName);
            string parentObjExpression = GetParentObjExpression(prop.IfType);

            this.WriteObjects("                case \"", prop.PropertyName, "\":");
            this.WriteLine();
            this.WriteObjects("                    {");
            this.WriteLine();
            this.WriteObjects("                        var __oldValue = ", propertyBackingStore, ";");
            this.WriteLine();
            this.WriteObjects("                        var __newValue = ", parentObjExpression, ";");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanging(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                        ", propertyBackingStore, " = __newValue;");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanged(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                    }");
            this.WriteLine();
            this.WriteObjects("                    break;");
            this.WriteLine();
        }
    }
}
