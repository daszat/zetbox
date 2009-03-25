
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for Modules.
    /// </summary>
    public interface Module : IDataObject 
    {

        /// <summary>
        /// Assemblies des Moduls
        /// </summary>

        ICollection<Kistl.App.Base.Assembly> Assemblies { get; }
        /// <summary>
        /// Datentypendes Modules
        /// </summary>

        ICollection<Kistl.App.Base.DataType> DataTypes { get; }
        /// <summary>
        /// Description of this Module
        /// </summary>
		string Description {
			get;
			set;
		}
        /// <summary>
        /// Name des Moduls
        /// </summary>
		string ModuleName {
			get;
			set;
		}
        /// <summary>
        /// CLR Namespace des Moduls
        /// </summary>
		string Namespace {
			get;
			set;
		}
    }
}