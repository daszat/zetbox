
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Template : IDataObject 
    {

        /// <summary>
        /// Assembly of the Type that is displayed with this Template
        /// </summary>
		Kistl.App.Base.Assembly DisplayedTypeAssembly {
			get;
			set;
		}
        /// <summary>
        /// FullName of the Type that is displayed with this Template
        /// </summary>
		string DisplayedTypeFullName {
			get;
			set;
		}
        /// <summary>
        /// a short name to identify this Template to the user
        /// </summary>
		string DisplayName {
			get;
			set;
		}
        /// <summary>
        /// The main menu for this Template
        /// </summary>

        ICollection<Kistl.App.GUI.Visual> Menu { get; }
        /// <summary>
        /// The visual representation of this Template
        /// </summary>
		Kistl.App.GUI.Visual VisualTree {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>

		 void PrepareDefault(Kistl.App.Base.ObjectClass cls) ;
    }
}