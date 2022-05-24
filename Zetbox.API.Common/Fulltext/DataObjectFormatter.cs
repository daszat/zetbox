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

namespace Zetbox.API.Common.Fulltext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Extensions;
    using Zetbox.App.Base;
    using System.Threading.Tasks;

    public sealed class DataObjectFormatter
    {
        private IMetaDataResolver _resolver;

        public DataObjectFormatter(IMetaDataResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task<string> Format(IDataObject obj)
        {
            if (obj == null) return string.Empty;

            var cls = _resolver.GetObjectClass(obj.Context.GetInterfaceType(obj));
            var allProps = await cls.GetAllProperties();
            var result = new StringBuilder();

            foreach (var prop in allProps.OfType<StringProperty>().OrderBy(p => p.Name))
            {
                var txtVal = obj.GetPropertyValue<string>(prop.Name);
                if (!string.IsNullOrWhiteSpace(txtVal))
                {
                    result.AppendLine(txtVal);
                }
            }

            foreach (var prop in allProps.OfType<EnumerationProperty>().OrderBy(p => p.Name))
            {
                var enumVal = obj.GetPropertyValue<int>(prop.Name);
                var txtVal = await prop.Enumeration.GetLabelByValue(enumVal);
                if (!string.IsNullOrWhiteSpace(txtVal))
                {
                    result.AppendLine(txtVal);
                }
            }

            return result.ToString();
        }
    }
}
