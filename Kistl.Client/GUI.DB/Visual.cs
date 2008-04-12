using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

namespace Kistl.GUI.DB
{
    /// <summary>
    /// The abstract entity representing an actual visual element in the tree. 
    /// Usually displaying a single Property.
    /// </summary>
    public class Visual
    {
        /// <summary>
        /// The Property to display
        /// </summary>
        public BaseProperty Property { get; set; }

        /// <summary>
        /// Which visual is represented here
        /// TODO: should become a enum or similar
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// if this is a container, here are the visually contianed/controlled children of this Visual
        /// </summary>
        public IList<Visual> Children { get; set; }
    }

}
