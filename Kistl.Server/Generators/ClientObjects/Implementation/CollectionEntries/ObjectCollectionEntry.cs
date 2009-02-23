using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{

    /// <summary>
    /// Client-side template for Object-Object CollectionEntries. Since the 
    /// client uses lazily loaded BackReferenceCollections, this Template 
    /// generates no class definition.
    /// </summary>
    public partial class ObjectCollectionEntry
        : Templates.Implementation.CollectionEntries.ObjectCollectionEntry
    {

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, NewRelation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override string GetCeBaseClassName()
        {
            return "BaseClientCollectionEntry";
        }
    }
}
