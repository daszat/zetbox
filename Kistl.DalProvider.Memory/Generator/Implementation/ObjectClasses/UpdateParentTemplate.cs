
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

    public class UpdateParentTemplate
        : ClientObjects.Implementation.ObjectClasses.UpdateParentTemplate
    {
        public UpdateParentTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, List<ObjectReferenceProperty> props)
            : base(_host, ctx, props)
        {
        }
    }
}
