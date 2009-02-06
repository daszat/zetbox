
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface ControlInfo : IDataObject 
    {

        /// <summary>
        /// The assembly containing the Control
        /// </summary>

		Kistl.App.Base.Assembly Assembly { get; set; }
        /// <summary>
        /// The name of the class implementing this Control
        /// </summary>

		string ClassName { get; set; }
        /// <summary>
        /// Whether or not this Control can contain other Controls
        /// </summary>

		bool IsContainer { get; set; }
        /// <summary>
        /// The toolkit of this Control.
        /// </summary>

		Kistl.App.GUI.Toolkit Platform { get; set; }
        /// <summary>
        /// The type of Control of this implementation
        /// </summary>

		Kistl.App.GUI.VisualType ControlType { get; set; }
    }
}