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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

using Zetbox.App.Base;

namespace Zetbox.Server.SchemaManagement
{
    [Serializable]
    public class DBTypeNotFoundException : Exception
    {
        public DBTypeNotFoundException()
        {
        }

        public DBTypeNotFoundException(string message)
            : base(message)
        {
        }

        public DBTypeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private DBTypeNotFoundException(string clrTypeName, string message, Exception innerException)
            : this(message, innerException)
        {
            ClrType = clrTypeName;
        }

        public DBTypeNotFoundException(Property prop)
            : this(PropertyToClrType(prop), PropertyToMessage(prop), null)
        {
        }

        public DBTypeNotFoundException(Property prop, Exception ex)
            : this(PropertyToClrType(prop), PropertyToMessage(prop), ex)
        {
        }

        public string ClrType { get; private set; }

        #region Serialisation

        protected DBTypeNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ClrType = info.GetString("CLRType");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CLRType", ClrType);
        }

        #endregion

        private static string PropertyToClrType(Property prop)
        {
            return prop.GetType().Name;
        }

        private static string PropertyToMessage(Property prop)
        {
            var type = PropertyToClrType(prop);
            if (string.IsNullOrEmpty(type))
            {
                return "Could not resolve CLR Type to Database Type";
            }
            else
            {
                return string.Format("Could not resolve CLR Type \"{0}\" to Database Type", type);
            }
        }
    }
}
