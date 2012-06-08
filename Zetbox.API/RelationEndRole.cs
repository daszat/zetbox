// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API
{

    /// <summary>
    /// Quite arbitrary enum to denote which end an end is. Values are used for serialization
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
