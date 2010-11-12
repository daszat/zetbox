
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    public class CollectionSerialization
        : Templates.Serialization.CollectionSerialization
    {
        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByB)
            : base(_host, ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByB)
        {
        }

        public override bool ShouldSerialize()
        {
            // Do not serialize collection entries from client to server
            // they will be sent by the Client KistlContext as separate objects
            // from server to client they will be serialized - some kind of eager loading
            return direction != Serialization.SerializerDirection.ToStream;
        }
    }
}
