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

namespace Zetbox.Generator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;

    public static class MethodExtensions
    {
        public static string GetParameterDefinitions(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            return String.Join(", ",
                method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetParameterDefinition(p))
                .ToArray());
        }

        public static string GetParameterDefinition(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            return String.Format("{0} {1}", param.GetParameterTypeString(), param.Name);
        }

        public static string GetArguments(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            return String.Join(", ",
                method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetArgument(p))
                .ToArray());
        }

        public static string GetArgument(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            return param.Name;
        }

        public static async Task<string> GetArgumentTypes(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            return String.Join(", ",
                (await method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(async p => await GetArgumentType(p))
                .WhenAll())
                .ToArray());
        }

        public static async Task<string> GetArgumentType(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            return "typeof(" + await param.GetParameterTypeString() + ")";
        }


        public static Task<IOrderedEnumerable<Method>> OrderByDefault(this IEnumerable<Method> methods)
        {
            if (methods == null) { throw new ArgumentNullException("methods"); }

            return Task.FromResult(methods
                .OrderBy(m => m.Name)
                .ThenBy(m => m.Parameter.Count)
                .ThenBy(m => m.ID));
        }
    }
}
