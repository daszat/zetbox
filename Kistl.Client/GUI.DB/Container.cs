using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.GUI
{
    /// <summary>
    /// The abstract entity representing a container element in the visual tree
    /// </summary>
    public class Container : Visual
    {
        public IList<Visual> Children { get; set; }
    }
}
