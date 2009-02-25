using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{

    /// <summary>
    /// Quite arbitrary enum to denote which end a end is.
    /// </summary>
    /// Using A and B avoids any legasthenic problems and also collapses natural and semantic ordering.
    /// Contrast this to "right" and "left" which are usually written in the "wrong" (that is "right" on 
    /// the left side) order.
    public enum RelationEndRole
    {
        A = 1,
        B = 2
    };

}
