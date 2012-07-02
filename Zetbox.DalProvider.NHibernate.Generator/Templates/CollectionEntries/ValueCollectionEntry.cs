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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class ValueCollectionEntry
        : Templates.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop)
            : base(_host, ctx, prop)
        {
        }

        protected override void ApplyAPropertyTemplate()
        {
            string moduleNamespace = prop.GetCollectionEntryNamespace();
            string ownInterface = prop.GetCollectionEntryClassName();
            string name = "Parent";
            string eventName = "unused";
            string fkBackingName = "_fk_Parent";
            string fkGuidBackingName = "unused";
            string referencedInterface = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
            string referencedImplementation = referencedInterface + ImplementationSuffix;
            string positionPropertyName = string.Empty;
            string inverseNavigatorName = prop.Name;

            Properties.ObjectReferencePropertyTemplate.Call(
                Host,
                ctx,
                MembersToSerialize,
                moduleNamespace,
                ownInterface,
                name,
                string.Empty /*implNameUnused*/,
                eventName,
                fkBackingName,
                fkGuidBackingName,
                referencedInterface,
                referencedImplementation,
                string.Empty /*associationNameUnused*/,
                string.Empty /*targetRoleNameUnused*/,
                positionPropertyName,
                inverseNavigatorName,
                true /*inverseNavigatorIsList*/,
                false /*eagerLoading*/,
                false /*relDataTypeExportable*/,
                false /*callGetterSetterEvents*/,
                prop.IsCalculated());

            Templates.Properties.DelegatingProperty.Call(
                Host, ctx,
                "ParentObject", "Zetbox.API.IDataObject",
                "Parent", referencedImplementation);
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();
            var valueInitialisation = prop is CompoundObjectProperty
                ? new ObjectClasses.Constructors.CompoundInitialisationDescriptor[] {
                    new ObjectClasses.Constructors.CompoundInitialisationDescriptor(
                        "Value",
                        "this.Proxy.Value",
                        prop.GetElementTypeString(),
                        prop.GetElementTypeString() + ImplementationSuffix)
                }
                : new ObjectClasses.Constructors.CompoundInitialisationDescriptor[0];
            ObjectClasses.Constructors.Call(Host, ctx,
                valueInitialisation,
                new string[0],
                GetCeInterface(),
                GetCeClassName(),
                null);
        }

        protected override void ApplyIdPropertyTemplate()
        {
            // inherited from Base
            //base.ApplyIdPropertyTemplate();
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            Templates.Properties.DelegatingProperty.Call(Host, ctx, "Index", "int?", "this.Proxy.Value" + Zetbox.API.Helper.PositionSuffix, "int?");
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string interfaceName = GetCeInterface();
            string referencedType = prop.GetElementTypeString();
            if (prop is CompoundObjectProperty)
            {
                referencedType += ImplementationSuffix;
            }
            List<KeyValuePair<string, string>> typeAndNameList = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(Mappings.ObjectClassHbm.GetProxyTypeReference(prop.ObjectClass as ObjectClass, this.Settings), "Parent"),
                new KeyValuePair<string, string>("bool", "ValueIsNull"),
                new KeyValuePair<string, string>(referencedType, "Value"),
            };

            if (IsOrdered())
            {
                typeAndNameList.Add(new KeyValuePair<string, string>("int?", "Value" + Zetbox.API.Helper.PositionSuffix));
            }

            // Even exportable value collection entries do not have an 
            // export guid, since they are serialized in-place in the container
            //if (IsExportable())
            //{
            //    typeAndNameList.Add(new KeyValuePair<string, string>("Guid", "ExportGuid"));
            //}

            this.WriteLine("        public override void SaveOrUpdateTo(NHibernate.ISession session)");
            this.WriteLine("        {");
            this.WriteLine("            // ValueCollectionEntries and CompoundCollectionEntries are saved by cascade");
            this.WriteLine("            //base.SaveOrUpdateTo(session);");
            this.WriteLine("        }");
            this.WriteLine("");

            GetDeletedRelatives.Call(Host, "Parent", null);

            ObjectClasses.ProxyClass.Call(Host, ctx, interfaceName, new KeyValuePair<string, string>[0], typeAndNameList);
        }

        protected override void ApplyReloadReferenceBody()
        {
            string referencedInterface = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
            var cls = prop.ObjectClass as ObjectClass;
            if (cls == null)
            {
                if (prop.ObjectClass == null)
                {
                    Logging.Log.ErrorFormat("tried to create ReloadReferenceBody for unattached property: [{0}]", prop.Name);
                }
                else
                {
                    Logging.Log.ErrorFormat("tried to create ReloadReferenceBody for property [{0}] attached to non-ObjectClass: [{1}]", prop.Name, prop.ObjectClass);
                }
            }
            else
            {
                string referencedImplementation = Mappings.ObjectClassHbm.GetWrapperTypeReference(prop.ObjectClass as ObjectClass, this.Settings);
                // value collections are always exported / serialized inline. no need for parent guid
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "Parent", "Parent", "_fk_Parent", null, false);
            }
        }
    }
}
