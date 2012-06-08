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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class Template
        : Templates.CompoundObjects.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, CompoundObject s)
            : base(_host, ctx, s)
        {
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructor
            // base.ApplyConstructorTemplate();

            string interfaceName = DataType.Name;
            string className = GetTypeName();
            string baseClassName = null;
            Constructors.Call(Host, ctx, this.DataType.Properties.OfType<CompoundObjectProperty>(), interfaceName, className, baseClassName);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string interfaceName = DataType.Name;
            this.WriteObjects("public class ", interfaceName, "Proxy { }");
            this.WriteLine();
            this.WriteObjects("public bool CompoundObject_IsNull { get; set; }");
            this.WriteLine();
    }

        protected override string GetExportGuidBackingStoreReference()
        {
            return "this.Proxy.ExportGuid";
        }
    }
}
