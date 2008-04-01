using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

namespace Kistl.GUI
{
    /// <summary>
    /// The abstract entity representing an actual visual element in the tree. 
    /// Usually displaying a single Property.
    /// </summary>
    public class Visual
    {
        public BaseProperty Property { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ControlImplementation GetImplementationInfo(Platform p)
        {
            return (from ci in ControlImplementation.Implementations
                    where ci.Control == Name
                        && ci.Platform == p
                    select ci).Single();
        }
    }
}
