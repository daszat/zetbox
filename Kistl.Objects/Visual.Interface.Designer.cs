
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface Visual : IDataObject 
    {

        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>

        ICollection<Kistl.App.GUI.Visual> Children { get; }
        /// <summary>
        /// The Property to display
        /// </summary>

		Kistl.App.Base.BaseProperty Property { get; set; }
        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>

		Kistl.App.Base.Method Method { get; set; }
        /// <summary>
        /// The context menu for this Visual
        /// </summary>

        ICollection<Kistl.App.GUI.Visual> ContextMenu { get; }
        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>

		string Description { get; set; }
        /// <summary>
        /// Which visual is represented here
        /// </summary>

		Kistl.App.GUI.VisualType ControlType { get; set; }
    }
}