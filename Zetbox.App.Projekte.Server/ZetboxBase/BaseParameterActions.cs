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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class BaseParameterActions
    {
        [Invocation]
        public static void NotifyPreSave(Zetbox.App.Base.BaseParameter obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.Name))
            {
                throw new ArgumentException(string.Format("Name {0} has some illegal chars", obj.Name));
            }

            // TODO: replace with constraint
            if (obj.Method != null && obj.Method.Parameter.Count(p => p.IsReturnParameter) > 1)
            {
                throw new ArgumentException(string.Format("Method {0}.{1}.{2} has more then one Return Parameter",
                    obj.Method.ObjectClass.Module.Namespace,
                    obj.Method.ObjectClass.Name,
                    obj.Method.Name));
            }
        }
    }
}
