
namespace Zetbox.DalProvider.Client.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public class CollectionSerialization
        : Templates.Serialization.CollectionSerialization
    {
        public CollectionSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Templates.Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool orderByValue, bool inPlace)
            : base(_host, ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByValue, inPlace)
        {
        }

        public override bool ShouldSerialize()
        {
            // Do not serialize collection entries from client to server
            // they will be sent by the Client ZetboxContext as separate objects
            // from server to client they will be serialized - some kind of eager loading
            return inPlace || direction != Templates.Serialization.SerializerDirection.ToStream;
        }
    }
}
