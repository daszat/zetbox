
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface ViewDescriptor : IDataObject 
    {

        /// <summary>
        /// The control implementing this View
        /// </summary>
		Kistl.App.Base.TypeRef ControlRef {
			get;
			set;
		}
        /// <summary>
        /// The PresentableModel usable by this View
        /// </summary>
		Kistl.App.GUI.PresentableModelDescriptor PresentedModelDescriptor {
			get;
			set;
		}
        /// <summary>
        /// Which toolkit provides this View
        /// </summary>
		Kistl.App.GUI.Toolkit Toolkit {
			get;
			set;
		}
        /// <summary>
        /// The visual type of this View
        /// </summary>
		Kistl.App.GUI.VisualType VisualType {
			get;
			set;
		}
    }
}