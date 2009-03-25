
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for BackReference Properties.
    /// </summary>
    public interface BackReferenceProperty : Kistl.App.Base.BaseProperty 
    {

        /// <summary>
        /// Serialisierung der Liste zum Client
        /// </summary>
		bool PreFetchToClient {
			get;
			set;
		}
        /// <summary>
        /// Das Property, welches auf diese Klasse zeigt
        /// </summary>
		Kistl.App.Base.ObjectReferenceProperty ReferenceProperty {
			get;
			set;
		}
    }
}