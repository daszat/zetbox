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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class DefaultMethods
        : Templates.ObjectClasses.DefaultMethods
    {
        public DefaultMethods(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
            : base(_host, ctx, dt)
        {
        }

        protected override void ApplyPrePreSaveTemplate()
        {
            base.ApplyPrePreSaveTemplate();
            foreach (var prop in dt.Properties.Where(p => p.DefaultValue != null || p.IsCalculated()).OrderBy(p => p.Name))
            {
                if (prop is CalculatedObjectReferenceProperty) continue;

                this.WriteObjects("            Fetch", prop.Name, "OrDefault();");
                this.WriteLine();
            }
        }

        protected override void ApplyPostDeletingTemplate()
        {
            foreach (var prop in dt.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList() && !p.IsCalculated()).OrderBy(p => p.Name).Cast<Property>()
                         .Concat(dt.Properties.OfType<CompoundObjectProperty>().Where(p => p.IsList() && !p.IsCalculated()).OrderBy(p => p.Name).Cast<Property>()))
            {
                this.WriteObjects("            foreach(NHibernatePersistenceObject x in ", prop.Name, "Collection) {\r\n");
                this.WriteObjects("                x.ParentsToDelete.Add(this);\r\n");
                this.WriteObjects("                ChildrenToDelete.Add(x);\r\n");
                this.WriteObjects("            }\r\n");
            }
            this.WriteObjects("\r\n");
            var cls = dt as ObjectClass;
            if (cls != null)
            {
                foreach (var rel in cls.GetRelations().OrderBy(r => r.GetAssociationName()))
                {
                    if (rel.A.Type == cls && rel.GetRelationType() != RelationType.n_m && rel.Storage != StorageType.Separate)
                    {
                        ApplyRememberToDeleteTemplate(rel, rel.A);
                    }
                    else if (rel.B.Type == cls && rel.GetRelationType() != RelationType.n_m && rel.Storage != StorageType.Separate)
                    {
                        ApplyRememberToDeleteTemplate(rel, rel.B);
                    }
                }
                this.WriteObjects("\r\n");
            }

            base.ApplyPostDeletingTemplate();
        }

        private void ApplyRememberToDeleteTemplate(Relation rel, RelationEnd relEnd)
        {
            if (rel.GetOtherEnd(relEnd).Multiplicity == Multiplicity.ZeroOrMore)
            {
                if (relEnd.Navigator != null)
                {
                    var prop = relEnd.Navigator;
                    this.WriteObjects("            // ", rel.GetAssociationName(), " ZeroOrMore\r\n");
                    this.WriteObjects("            foreach(NHibernatePersistenceObject x in ", prop.Name, ") {\r\n");
                    this.WriteObjects("                x.ParentsToDelete.Add(this);\r\n");
                    this.WriteObjects("                ChildrenToDelete.Add(x);\r\n");
                    this.WriteObjects("            }\r\n");
                }
                else
                {
                    this.WriteObjects("            // should fetch && remember parent for ", relEnd.Parent.GetRelationClassName(), "\r\n");
                }
            }
            else
            {
                if (relEnd.Navigator != null)
                {
                    var prop = relEnd.Navigator;
                    this.WriteObjects("            // ", rel.GetAssociationName(), "\r\n");
                    this.WriteObjects("            if (", prop.Name, " != null) {\r\n");
                    this.WriteObjects("                ((NHibernatePersistenceObject)", prop.Name, ").ChildrenToDelete.Add(this);\r\n");
                    this.WriteObjects("                ParentsToDelete.Add((NHibernatePersistenceObject)", prop.Name, ");\r\n");
                    this.WriteObjects("            }\r\n");
                }
                else
                {
                    this.WriteObjects("            // should fetch && remember children for ", relEnd.Parent.GetRelationClassName(), "\r\n");
                }
            }
        }
    }
}
