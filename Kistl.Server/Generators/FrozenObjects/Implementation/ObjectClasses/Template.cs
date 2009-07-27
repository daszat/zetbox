using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.ObjectClasses
{
    public class Template
        : Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.DalProvider.Frozen"
            });
        }

        public static string GetClassName(ObjectClass cls)
        {
            return cls.ClassName + Kistl.API.Helper.ImplementationSuffix + "Frozen";
        }

        protected override string MungeClassName(string name)
        {
            return Template.GetClassName(this.ObjectClass);
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return this.ObjectClass.BaseObjectClass.Module.Namespace + "." + Template.GetClassName(this.ObjectClass.BaseObjectClass);
            }
            else
            {
                return "BaseFrozenDataObject";
            }
        }

        protected override void ApplyListProperty(Property prop, Templates.Implementation.SerializationMembersList serList)
        {
            Implementation.ObjectClasses.ListProperty.Call(Host, ctx,
                 serList,
                 this.DataType,
                 prop.PropertyName,
                 prop);
        }

        protected override void ApplyObjectListPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            ApplyListProperty(relEnd.Navigator, MembersToSerialize);
        }

        protected override void ApplyCollectionEntryListTemplate(Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            ApplyListProperty(relEnd.Navigator, MembersToSerialize);
        }

        protected override void ApplyApplyChangesFromMethod()
        {
            // Read only, no overload
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            // implement internal constructor to allow the FrozenContext to initialize the objects
            this.WriteObjects("        internal ", this.GetTypeName(), "(int id)");
            this.WriteLine();
            this.WriteObjects("            : base(id)");
            this.WriteLine();
            this.WriteLine("        { }");

            // TODO: IsFrozen should be set if BaseClass.IsFrozen is set
            if (this.ObjectClass.IsFrozen())
            {
                Implementation.ObjectClasses.DataStore.Call(Host, ctx, this.ObjectClass);
            }
        }
    }
}
