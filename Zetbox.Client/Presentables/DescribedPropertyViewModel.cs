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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;

    public class DescribedPropertyViewModel 
        : DataObjectViewModel
    {
        public new delegate DescribedPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Property prop);

        public DescribedPropertyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Property prop)
            : base(appCtx, dataCtx, parent, prop)
        {
            _prop = prop;
        }
        private Property _prop;

        public Property DescribedProperty { get { return _prop; } }

        public override string Name
        {
            get
            {
                return _prop.Name;
            }
        }

        public string TypeString
        {
            get
            {
                return _prop.GetPropertyTypeString();
            }
        }

        public string ShortTypeString
        {
            get
            {
                if (_prop is BoolProperty)
                {
                    return "bool";
                }
                else if (_prop is IntProperty)
                {
                    return "int";
                }
                else if (_prop is DecimalProperty)
                {
                    return "decimal";
                }
                else if (_prop is DoubleProperty)
                {
                    return "double";
                }
                else if (_prop is StringProperty)
                {
                    return "string";
                }
                else if (_prop is GuidProperty)
                {
                    return "Guid";
                }
                else if (_prop is DateTimeProperty)
                {
                    return "DateTime";
                }
                else
                {
                    return TypeString;
                }
            }
        }

        public bool IsList
        {
            get
            {
                if (_prop is ValueTypeProperty)
                {
                    return ((ValueTypeProperty)_prop).IsList;
                }
                else if (_prop is ObjectReferenceProperty)
                {
                    var orp = (ObjectReferenceProperty)_prop;
                    return orp.GetIsList();
                }
                return false;
            }
        }
    }
}
