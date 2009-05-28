using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;

namespace Kistl.Server.SchemaManagement
{
    [Serializable]
    public class DBTypeNotFoundException : Exception
    {
        public DBTypeNotFoundException()
        {
        }

        public DBTypeNotFoundException(Kistl.App.Base.Property prop)
        {
            ClrType = prop.GetType().Name;
        }

        public DBTypeNotFoundException(Kistl.App.Base.Property prop, Exception ex)
            : base("", ex)
        {
            ClrType = prop.GetType().Name;
        }

        public string ClrType { get; set; }

        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(ClrType))
                {
                    return "Could not resolve CLR Type to Database Type";
                }
                else
                {
                    return string.Format("Could not resolve CLR Type \"{0}\" to Database Type", ClrType);
                }
            }
        }

        #region Serialisation

        protected DBTypeNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            ClrType = info.GetString("CLRType");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CLRType", ClrType);
        }

        #endregion
    }
}
