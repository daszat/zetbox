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

namespace Zetbox.DalProvider.Client.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public class Template : Templates.ObjectClasses.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject list property");
            Templates.Properties.ValueCollectionProperty.Call(Host, ctx,
                this.MembersToSerialize,
                prop,
                "ClientValueCollectionAsListWrapper",
                "ClientValueListWrapper");
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            Templates.Properties.ValueCollectionProperty.Call(Host, ctx,
                MembersToSerialize,
                prop,
                "ClientValueCollectionAsListWrapper",
                "ClientValueListWrapper");
        }

        protected override void ApplyMethodTemplate(Method m, int index)
        {
            if (m.InvokeOnServer == true)
            {
                ObjectClasses.InvokeServerMethod.Call(Host, ctx, this.DataType, m, index);                
            }
            else
            {
                base.ApplyMethodTemplate(m, index);
            }
        }
    }
}
