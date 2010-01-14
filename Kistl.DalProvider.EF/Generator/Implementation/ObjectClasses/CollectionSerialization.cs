using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public class CollectionSerialization
        : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.CollectionSerialization
    {
        public CollectionSerialization(
            Arebis.CodeGeneration.IGenerationHost _host,
            IKistlContext ctx,
            Kistl.Server.Generators.Templates.Implementation.SerializerDirection direction,
            string streamName,
            string xmlnamespace,
            string xmlname,
            string collectionName,
            bool orderByB)
            : base(_host, ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByB)
        {
        }

        public override bool ShouldSerialize()
        {
            // Do not deserialize colletion entries from client to server
            // they will be send by the Client KistlContext as seperate objects
            // from server to client the will be serialized - some kind of eager loading
            return direction != Kistl.Server.Generators.Templates.Implementation.SerializerDirection.FromStream;
        }
    }
}
