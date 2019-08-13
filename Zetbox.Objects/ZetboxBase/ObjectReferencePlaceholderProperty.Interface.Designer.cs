// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// A placeholder for data object references in interfaces
    /// </summary>
    [Zetbox.API.DefinitionGuid("93241989-0bb0-435f-b6e5-06fdbbf19e5d")]
    public interface ObjectReferencePlaceholderProperty : Zetbox.App.Base.Property 
    {

        /// <summary>
        /// Whether or not the list has a persistent ordering of elements
        /// </summary>
        [Zetbox.API.DefinitionGuid("7e52aa2a-aa3a-4f5b-8171-c6c2f364108b")]
        bool HasPersistentOrder {
            get;
            set;
        }


        /// <summary>
        /// Suggested implementors role name. If empty, class name will be used
        /// </summary>
        [Zetbox.API.DefinitionGuid("b5fa31d8-ad30-4aeb-b5a0-8b4b117b1d29")]
        string ImplementorRoleName {
            get;
            set;
        }


        /// <summary>
        /// Whether or not this property placeholder is list valued
        /// </summary>
        [Zetbox.API.DefinitionGuid("52692870-0bd4-47b6-99dc-eb8bf4238f24")]
        bool IsList {
            get;
            set;
        }


        /// <summary>
        /// Suggested role name for the referenced item
        /// </summary>
        [Zetbox.API.DefinitionGuid("06d56d44-bc5f-428b-a6b5-4348573425f9")]
        string ItemRoleName {
            get;
            set;
        }


        /// <summary>
        /// The ObjectClass that is referenced by this placeholder
        /// </summary>
        [Zetbox.API.DefinitionGuid("41da7ae6-aff7-44cf-83be-6150bf7578fd")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.ObjectClass ReferencedObjectClass {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_ReferencedObjectClass 
		{ 
			get; 
			set;
		}

        /// <summary>
        /// Suggested verb for the new relation
        /// </summary>
        [Zetbox.API.DefinitionGuid("dd98c4f1-bf83-4d9a-8885-546457fc6591")]
        string Verb {
            get;
            set;
        }

    }
}
