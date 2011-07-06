
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    public partial class EagerLoadingSerialization
        : Templates.Serialization.EagerLoadingSerialization
    {
        protected string relationEntryCollectionName;

        public EagerLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool serializeIds, bool serializeRelationEntries, string relationEntryCollectionName)
            : base(_host,ctx, direction, streamName, xmlnamespace, xmlname, collectionName, serializeIds, serializeRelationEntries)
        {
            this.relationEntryCollectionName = relationEntryCollectionName;
        }
        
        protected override void ApplyRelationEntrySerialization()
        {
            this.WriteObjects("				foreach(var relEntry in ", relationEntryCollectionName, ")");
            this.WriteLine();
            this.WriteObjects("				{");
            this.WriteLine();
            this.WriteObjects("					auxObjects.Add(OurContext.AttachAndWrap(relEntry));");
            this.WriteLine();
            this.WriteObjects("				}");
            this.WriteLine();
        }
    }
}
