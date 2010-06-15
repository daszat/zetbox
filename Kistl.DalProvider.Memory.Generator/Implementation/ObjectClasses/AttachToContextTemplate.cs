
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

    public class AttachToContextTemplate
        : ClientObjects.Implementation.ObjectClasses.AttachToContextTemplate
    {
        public AttachToContextTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }
    }
}
