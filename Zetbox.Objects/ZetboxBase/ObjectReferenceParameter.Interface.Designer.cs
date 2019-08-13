// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for Object Parameter.
    /// </summary>
    [Zetbox.API.DefinitionGuid("3fb8bf11-cab6-478f-b9b8-3f6d70a70d37")]
    public interface ObjectReferenceParameter : Zetbox.App.Base.BaseParameter 
    {

        /// <summary>
        /// Zetbox-Typ des Parameters
        /// </summary>
        [Zetbox.API.DefinitionGuid("9bd64c60-7282-47f0-8069-528a175fcc92")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.ObjectClass ObjectClass {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_ObjectClass 
		{ 
			get; 
			set;
		}
    }
}
