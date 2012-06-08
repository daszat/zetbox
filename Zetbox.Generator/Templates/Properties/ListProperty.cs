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

namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;

    public partial class ListProperty
    {
        //public static void Call(IGenerationHost host, IZetboxContext ctx, Serialization.SerializationMembersList serList,
        //    DataType dataType, string propertyName, Property prop)
        //{
        //    if (host == null) { throw new ArgumentNullException("host"); }
        //    if (String.IsNullOrEmpty(propertyName)) { throw new ArgumentNullException("propertyName"); }
        //    if (prop == null) { throw new ArgumentNullException("prop"); }

        //    host.CallTemplate("Properties.ListProperty", ctx,
        //         serList,
        //         dataType,
        //         prop.Name,
        //         prop);
        //}

        /// <summary>
        /// Is called to insert requisites into the containing class, like wrappers or similar.
        /// </summary>
        protected virtual void ApplyRequisitesTemplate() { }

        /// <summary>
        /// Is called to apply optional decoration in front of the property declaration, like Attributes.
        /// </summary>
        protected virtual void ApplyAttributesTemplate() { }

        protected virtual string BackingMemberFromName(string name)
        {
            return "_" + name;
        }

        protected virtual void ApplySettor()
        {
        }

        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string name)
        {
        }

        /// <returns>the type of the property as string</returns>
        protected virtual string GetPropertyTypeString()
        {
            return property.GetPropertyTypeString();
        }

        /// <returns>the type of the backing store as string</returns>
        protected virtual string GetBackingTypeString()
        {
            return GetPropertyTypeString();
        }

        /// <returns>an expression which can be used to initialise the backing store</returns>
        protected virtual string GetInitialisationExpression()
        {
            return String.Format("new List<{0}>()", property.GetPropertyTypeString());
        }
    }
}
