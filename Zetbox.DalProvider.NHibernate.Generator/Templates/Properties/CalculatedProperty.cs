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


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Serialization = Zetbox.Generator.Templates.Serialization;

    public class CalculatedProperty : Zetbox.Generator.Templates.Properties.CalculatedProperty
    {
        public CalculatedProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
            : base(_host, ctx, serializationList, modulenamespace, className, referencedType, propertyName, getterEventName, isCompound)
        {
        }

        //protected override string ApplyBackingStorageDefinition()
        //{
        //    return string.Empty;
        //}

        //protected override string ApplyResultExpression()
        //{
        //    return string.Format("{0}", propertyName);
        //}

        //protected override string ApplyStorageStatement(string valueExpression)
        //{
        //    return string.Format("{0} = {1};", propertyName, valueExpression);
        //}
    }
}
