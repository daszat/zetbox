using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.SchemaManagement
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
