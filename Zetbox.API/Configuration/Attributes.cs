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

namespace Zetbox.API.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class FeatureAttribute : Attribute
    {
        public FeatureAttribute()
        {
            NotOnFallback = false;
        }

        /// <summary>
        /// Will not be loaded in fallback mode
        /// </summary>
        [DefaultValue(false)]
        public bool NotOnFallback { get; set; }
    }

    /// <summary>
    /// Modules marked with this attribute are automatically loaded.
    /// </summary>
    /// <remarks>
    /// The AssemblyResolver loads such modules after manually configured modules. They are loaded in the order defined by the AssemblySearchPath.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class AutoLoadAttribute : Attribute
    {
        public AutoLoadAttribute()
        {
            NotOnFallback = false;
        }

        /// <summary>
        /// Will not be loaded in fallback mode
        /// </summary>
        [DefaultValue(false)]
        public bool NotOnFallback { get; set; }
    }
}
