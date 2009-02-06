using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DALProvider.EF
{
    public static class EfSerializer
    {
        public static void ToStream<T, AType, BType>(EntityCollection<T> collection, System.IO.BinaryWriter sw)
            where T : class, IEntityWithRelationships, INewCollectionEntry<AType, BType>
        {
            foreach (var obj in collection)
            {
                BinarySerializer.ToStream(true, sw);
                obj.ToStream(sw);
            }

            BinarySerializer.ToStream(false, sw);
        }
    }
}
