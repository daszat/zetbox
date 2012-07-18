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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class CalculatedProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serList,
            CalculatedObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            // IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound, string backingName)
            Call(host, ctx, serList,
                prop.Module.Namespace, 
                prop.ObjectClass.GetDataTypeString(), 
                prop.GetElementTypeString(), 
                prop.Name, "On" + prop.Name + "_Getter", 
                prop.ObjectClass is CompoundObject,
                prop.DisableExport == true);
        }

        protected virtual string ApplyBackingStorageDefinition()
        {
            return string.Format("{0} {1}_Store;", referencedType, propertyName);
        }

        protected virtual string ApplyResultExpression()
        {
            return string.Format("{0}_Store", propertyName);
        }

        protected virtual string ApplyStorageStatement(string valueExpression)
        {
            return string.Format("{0}_Store = {1};", propertyName, valueExpression);
        }

        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
                list.Add(disableExport ? Serialization.SerializerType.Binary : Serialization.SerializerType.All, modulenamespace, name, referencedType, ApplyResultExpression());
        }
    }
}