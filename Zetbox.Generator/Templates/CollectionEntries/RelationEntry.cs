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

namespace Zetbox.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    /// <summary>
    /// Client-side template for Object-Object CollectionEntries. Since the 
    /// client uses lazily loaded OneNRelationList, this Template 
    /// generates no class definition.
    /// </summary>
    public partial class RelationEntry
        : CollectionEntryTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, Relation rel)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("CollectionEntries.RelationEntry", ctx, rel);
        }

        protected Relation rel { get; private set; }

        public RelationEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Relation rel)
            : base(_host, ctx)
        {
            this.rel = rel;
        }

        protected override string GetCeClassName()
        {
            return rel.GetRelationClassName() + ImplementationSuffix;
        }

        protected override string GetCeBaseClassName()
        {
            return String.Format("{0}.RelationEntry{1}<{2}, {2}{1}, {3}, {3}{1}>",
                ImplementationNamespace,
                ImplementationSuffix,
                rel.A.Type.GetDescribedInterfaceTypeName(),
                rel.B.Type.GetDescribedInterfaceTypeName());
        }

        protected override bool IsExportable()
        {
            return rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable();
        }

        protected override string[] GetIExportableInterfaces()
        {
            if (IsExportable())
            {
                return new string[] { "Zetbox.API.IExportableInternal", "Zetbox.App.Base.IExportable" };
            }
            else
            {
                return new string[0];
            }
        }

        protected override string GetCeInterface()
        {
            return rel.GetRelationClassName();
        }

        protected override void ApplyClassHeadTemplate()
        {
            base.ApplyClassHeadTemplate();
            this.WriteLine("        #region RelationEntry.ApplyClassHeadTemplate");
            this.WriteLine();

            // Implement RelationID by using a static backing store
            this.WriteLine("        private static readonly Guid _relationID = new Guid(\"{0}\");", rel.ExportGuid);
            this.WriteLine("        public override Guid RelationID { get { return _relationID; } }");
            this.WriteLine();

            // Implement untyped IRelationEntry
            this.WriteLine("        IDataObject IRelationEntry.AObject");
            this.WriteLine("        {");
            this.WriteLine("            get");
            this.WriteLine("            {");
            this.WriteLine("                return A;");
            this.WriteLine("            }");
            this.WriteLine("            set");
            this.WriteLine("            {");
            this.WriteLine("                // settor will do checking for us");
            this.WriteLine("                A = ({0})value;", rel.A.Type.GetDescribedInterfaceTypeName());
            this.WriteLine("            }");
            this.WriteLine("        }");
            this.WriteLine();
            this.WriteLine("        IDataObject IRelationEntry.BObject");
            this.WriteLine("        {");
            this.WriteLine("            get");
            this.WriteLine("            {");
            this.WriteLine("                return B;");
            this.WriteLine("            }");
            this.WriteLine("            set");
            this.WriteLine("            {");
            this.WriteLine("                // settor will do checking for us");
            this.WriteLine("                B = ({0})value;", rel.B.Type.GetDescribedInterfaceTypeName());
            this.WriteLine("            }");
            this.WriteLine("        }");
            this.WriteLine();
            this.WriteLine("        #endregion // RelationEntry.ApplyClassHeadTemplate");
            this.WriteLine();
        }

        protected virtual void ApplyManageObjectState()
        {
            ManageObjectState.Call(Host);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            ApplyManageObjectState();
        }

        protected override bool IsOrdered()
        {
            return rel.NeedsPositionStorage(RelationEndRole.A) || rel.NeedsPositionStorage(RelationEndRole.B);
        }

        protected override void ApplyChangesFromBody()
        {
            this.WriteLine("            me._fk_A = other._fk_A;");
            if (rel.NeedsPositionStorage(RelationEndRole.A))
            {
                this.WriteLine("            me.AIndex = other.AIndex;");
            }

            this.WriteLine("            me._fk_B = other._fk_B;");
            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                this.WriteLine("            me.BIndex = other.BIndex;");
            }
        }

        protected override void ApplyReloadReferenceBody()
        {
            base.ApplyReloadReferenceBody();

            {
                string referencedInterface = rel.A.Type.Module.Namespace + "." + rel.A.Type.Name;
                string referencedImplementation = referencedInterface + ImplementationSuffix;
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "A", "A" + ImplementationPropertySuffix, "_fk_A", "_fk_guid_A", IsExportable());
            }
            {
                string referencedInterface = rel.B.Type.Module.Namespace + "." + rel.B.Type.Name;
                string referencedImplementation = referencedInterface + ImplementationSuffix;
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "B", "B" + ImplementationPropertySuffix, "_fk_B", "_fk_guid_B", IsExportable());
            }
        }
    }
}
