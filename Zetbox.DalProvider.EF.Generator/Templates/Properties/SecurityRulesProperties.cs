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

namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator;

    public partial class SecurityRulesProperties
    {
        public static void Call(IGenerationHost host, IZetboxContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }

            string assocName = Construct.SecurityRulesFKName(cls);
            string targetRoleName = Construct.SecurityRulesClassName(cls);
            string referencedImplementation = Construct.SecurityRulesClassName(cls) + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;
            string efNameRightsPropertyName = "SecurityRightsCollection" + Zetbox.API.Helper.ImplementationSuffix;

            host.CallTemplate("Properties.SecurityRulesProperties", ctx, cls,
                assocName,
                targetRoleName,
                referencedImplementation,
                efNameRightsPropertyName);
        }
    }
}
