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

namespace Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public class OnPropertyChange : Templates.ObjectClasses.OnPropertyChange
    {
        public OnPropertyChange(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
            : base(_host, ctx, dt)
        { }

        protected override void ApplyNotifyPropertyChanging(Property prop)
        {
            base.ApplyNotifyPropertyChanging(prop);
            if (prop is CalculatedObjectReferenceProperty)
            {
                // Not implemented yet, maybe in a far, far future in a far, far away galaxy
            }
            else
            {
                this.WriteLine("                    ReportEfPropertyChanging(\"{0}\");", Properties.EfScalarPropHelper.GetEfPropName(prop));
            }
        }

        protected override void ApplyNotifyPropertyChanged(Property prop)
        {
            this.WriteLine("                    ReportEfPropertyChanged(\"{0}\");", Properties.EfScalarPropHelper.GetEfPropName(prop));
            if (prop is CalculatedObjectReferenceProperty)
            {
                // Not implemented yet, maybe in a far, far future in a far, far away galaxy
            }
            else
            {
                base.ApplyNotifyPropertyChanged(prop);
            }
        }
    }
}