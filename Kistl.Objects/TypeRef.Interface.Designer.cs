
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
    /// </summary>
    public interface TypeRef : IDataObject 
    {

        /// <summary>
        /// The assembly containing the referenced Type.
        /// </summary>
		Kistl.App.Base.Assembly Assembly {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
		string FullName {
			get;
			set;
		}
        /// <summary>
        /// list of type arguments
        /// </summary>

        IList<Kistl.App.Base.TypeRef> GenericArguments { get; }
        /// <summary>
        /// get the referenced <see cref="System.Type"/>
        /// </summary>

		 System.Type AsType(System.Boolean throwOnError) ;
    }
}