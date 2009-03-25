
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface PresenterInfo : IDataObject 
    {

        /// <summary>
        /// which controls are handled by this Presenter
        /// </summary>
		Kistl.App.GUI.VisualType ControlType {
			get;
			set;
		}
        /// <summary>
        /// The Assembly of the Data Type
        /// </summary>
		Kistl.App.Base.Assembly DataAssembly {
			get;
			set;
		}
        /// <summary>
        /// The CLR namespace and class name of the Data Type
        /// </summary>
		string DataTypeName {
			get;
			set;
		}
        /// <summary>
        /// Where to find the implementation of the Presenter
        /// </summary>
		Kistl.App.Base.Assembly PresenterAssembly {
			get;
			set;
		}
        /// <summary>
        /// The CLR namespace and class name of the Presenter
        /// </summary>
		string PresenterTypeName {
			get;
			set;
		}
    }
}