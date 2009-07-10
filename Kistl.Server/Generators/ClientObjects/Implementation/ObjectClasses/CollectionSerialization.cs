using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public class CollectionSerialization : Templates.Implementation.ObjectClasses.CollectionSerialization
    {
        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName)
            : base(_host, ctx, direction, streamName, xmlnamespace, xmlname, collectionName)
        {
        }

        public override bool ShouldSerialize()
        {
            return direction != Kistl.Server.Generators.Templates.Implementation.SerializerDirection.ToStream;
        }
    }
}
