
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Describes a Relation between two Object Classes
    /// </summary>
    public interface Relation : IDataObject 
    {

        /// <summary>
        /// Left Part of the Relation
        /// </summary>

		Kistl.App.Base.ObjectReferenceProperty LeftPart { get; set; }
        /// <summary>
        /// Right Part of the Relation
        /// </summary>

		Kistl.App.Base.ObjectReferenceProperty RightPart { get; set; }
        /// <summary>
        /// Storagetype for 1:1 Relations. Must be null for non 1:1 Relations.
        /// </summary>

		Kistl.App.Base.StorageType? Storage { get; set; }
        /// <summary>
        /// Description of this Relation
        /// </summary>

		string Description { get; set; }
    }
}