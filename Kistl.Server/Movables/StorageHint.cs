using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

namespace Kistl.Server.Movables
{
    // TODO: Move to Database

    /// <summary>
    /// A hint to the generator how the physical layout of a relation should be done.
    /// </summary>
    /// The first three are binary compatible to Kistl.App.Base.StorageType
    public enum StorageHint
    {
        /// <summary>
        /// Hints that the relations information should be stored with the A-side entity.
        /// </summary>
        MergeA = 1,
        /// <summary>
        /// Hints that the relations information should be stored with the B-side entity.
        /// </summary>
        MergeB = 2,
        /// <summary>
        /// Hints that the relations information should be stored with both entities.
        /// </summary>
        Replicate = 3,
        /// <summary>
        /// Hints that the relations information should be stored in a separate entity.
        /// </summary>
        Separate = 4,
        /// <summary>
        /// No hint given.
        /// </summary>
        NoHint = 5,
    }


    public static class StorageHintExtensions
    {
        public static StorageHint ToHint(this StorageType? type)
        {
            if (!type.HasValue)
            {
                return StorageHint.NoHint;
            }

            switch (type.Value)
            {
                case StorageType.Left:
                    return StorageHint.MergeA;
                case StorageType.Right:
                    return StorageHint.MergeB;
                case StorageType.Replicate:
                    return StorageHint.Replicate;
                default:
                    return StorageHint.NoHint;
            }
        }
    }
}
