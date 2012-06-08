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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class UpdateParentTemplate : Templates.ObjectClasses.UpdateParentTemplate
    {
        public UpdateParentTemplate(Arebis.CodeGeneration.IGenerationHost _host, List<Templates.ObjectClasses.UpdateParentTemplateParams> props)
            : base(_host, props)
        {
        }

        // NHibernate has different structure, accesses Proxy directly
        protected override void ApplyCase(Templates.ObjectClasses.UpdateParentTemplateParams prop)
        {
            string implType = prop.IfType+ ImplementationSuffix;

            this.WriteObjects("                case \"", prop.PropertyName, "\":");
            this.WriteLine();
            this.WriteObjects("                    {");
            this.WriteLine();
            this.WriteObjects("                        var __oldValue = (", implType, ")OurContext.AttachAndWrap(this.Proxy.", prop.PropertyName, ");");
            this.WriteLine();
            this.WriteObjects("                        var __newValue = (", implType, ")parentObj;");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanging(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                        this.Proxy.", prop.PropertyName, " = __newValue == null ? null : __newValue.Proxy;");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanged(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                    }");
            this.WriteLine();
            this.WriteObjects("                    break;");
            this.WriteLine();
        }
    }
}
