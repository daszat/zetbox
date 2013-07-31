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

namespace Zetbox.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Configuration;
    using System.ComponentModel;
    using Autofac;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.API;
    using Zetbox.API.Client;

    public class DefaultFulltextSupport : IFulltextSupport
    {
        [Description("Marker module for client side fulltext support")]
        [Feature]
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .Register<DefaultFulltextSupport>(c => new DefaultFulltextSupport())
                    .As<IFulltextSupport>()
                    .SingleInstance();
            }
        }

        public bool IsValidSearch(string text)
        {
            return true;
        }

        public bool HasIndexedFields(ObjectClass cls)
        {
            if (cls.ImplementsICustomFulltextFormat()) return true;

            foreach (var prop in cls.GetAllProperties())
            {
                if (prop is StringProperty /* && further restictions */) return true;
                // if (prop is EnumerationProperty) return true;
            }

            return false;
        }
    }
}
