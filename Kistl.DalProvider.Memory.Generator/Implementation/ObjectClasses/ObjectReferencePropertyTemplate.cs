
namespace Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using ClientObjects = Kistl.Server.Generators.ClientObjects;
    using Templates = Kistl.Server.Generators.Templates;

    public class ObjectReferencePropertyTemplate
        : ClientObjects.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
    {
        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList,
            string name, string implName, string fkBackingName, string fkGuidBackingName, string ownInterface, string referencedInterface,
            Relation rel, RelationEndRole endRole, bool hasInverseNavigator, bool hasPositionStorage, string positionPropertyName, bool relDataTypeExportable, string moduleNamespace, bool callGetterSetterEvents, bool isReloadable)
            : base(_host, ctx, serializationList, name, implName, fkBackingName, fkGuidBackingName, ownInterface, referencedInterface,
            rel, endRole, hasInverseNavigator, hasPositionStorage, positionPropertyName, relDataTypeExportable, moduleNamespace, callGetterSetterEvents, isReloadable)
        {
        }
    }
}
