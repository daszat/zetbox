
namespace Kistl.App.Base
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
        /// 
        /// </summary>
		Kistl.App.Base.TypeRef LayoutRef {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
		Kistl.App.GUI.Toolkit Toolkit {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
		Kistl.App.Base.TypeRef ViewRef {
			get;
			set;
		}
    }
}