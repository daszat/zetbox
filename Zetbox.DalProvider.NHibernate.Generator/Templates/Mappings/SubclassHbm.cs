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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.API.Server;

    public partial class SubclassHbm
    {
        public static void Call(IGenerationHost host, IZetboxContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (cls.BaseObjectClass == null)
            {
                var msg = String.Format("Only subclasses can be joined subclasses, but {0} doesn't have a base class",
                        cls.Name);
                throw new ArgumentOutOfRangeException("cls", msg);
            }

            host.CallTemplate("Mappings.SubclassHbm",
                new object[] { ctx }
                    .Concat(ObjectClassHbm.MakeArgs(ctx, cls, host.Settings))
                    .ToArray());
        }

        protected virtual string GetTagName()
        {
            switch (mappingType)
            {
                case TableMapping.TPT:
                    return "joined-subclass";
                case TableMapping.TPH:
                    return "subclass";
                default:
                    throw new NotSupportedException(string.Format("Mapping Type {0} is not supported", mappingType));
            }
        }

        protected virtual void ApplyPropertyDefinitions(List<Property> properties)
        {
            PropertiesHbm.Call(Host, ctx, String.Empty, properties, false);
        }

        protected virtual void ApplyJoinedSubclasses(List<ObjectClass> subClasses)
        {
            foreach (var subClass in subClasses.OrderBy(cls => cls.Name))
            {
                SubclassHbm.Call(Host, ctx, subClass);
            }
        }
    }
}
