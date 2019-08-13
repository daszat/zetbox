// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for a CompoundObject Parameter.
    /// </summary>
    [Zetbox.API.DefinitionGuid("3915cfbf-33c4-4a25-bc5f-b2dd07a9439d")]
    public interface CompoundObjectParameter : Zetbox.App.Base.BaseParameter 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("43d03fec-b595-46d0-b5d5-cf4c5d21fda7")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.CompoundObject CompoundObject {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_CompoundObject 
		{ 
			get; 
			set;
		}
    }
}
