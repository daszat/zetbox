using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{

    /// <summary>
    /// Client-side template for Object-Object CollectionEntries. Since the 
    /// client uses lazily loaded OneNRelationCollection, this Template 
    /// generates no class definition.
    /// </summary>
    public partial class ObjectCollectionEntry
        : Templates.Implementation.CollectionEntries.ObjectCollectionEntry
    {

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override string GetCeBaseClassName()
        {
            return "BaseClientCollectionEntry";
        }
    }
}
