
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Describes one end of a relation between two object classes
    /// </summary>
    public interface RelationEnd : IDataObject 
    {

        /// <summary>
        /// The Relation using this RelationEnd as A
        /// </summary>
		Kistl.App.Base.Relation AParent {
			get;
			set;
		}
        /// <summary>
        /// The Relation using this RelationEnd as B
        /// </summary>
		Kistl.App.Base.Relation BParent {
			get;
			set;
		}
        /// <summary>
        /// Is true, if this RelationEnd persists the order of its elements
        /// </summary>
		bool HasPersistentOrder {
			get;
			set;
		}
        /// <summary>
        /// Specifies how many instances may occur on this end of the relation.
        /// </summary>
		Kistl.App.Base.Multiplicity Multiplicity {
			get;
			set;
		}
        /// <summary>
        /// The ORP to navigate FROM this end of the relation. MAY be null.
        /// </summary>
		Kistl.App.Base.ObjectReferenceProperty Navigator {
			get;
			set;
		}
        /// <summary>
        /// Which RelationEndRole this End has
        /// </summary>
		int Role {
			get;
			set;
		}
        /// <summary>
        /// This end's role name in the relation
        /// </summary>
		string RoleName {
			get;
			set;
		}
        /// <summary>
        /// Specifies which type this End of the relation has. MUST NOT be null.
        /// </summary>
		Kistl.App.Base.ObjectClass Type {
			get;
			set;
		}
    }
}