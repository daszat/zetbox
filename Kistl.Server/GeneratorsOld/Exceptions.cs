#if DONOTUSE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Security.Permissions;
using Kistl.API;

namespace Kistl.Server.GeneratorsOld
{
    [Serializable]
    public class DBTypeNotFoundException : Exception
    {
        public DBTypeNotFoundException()
        {
        }

        public DBTypeNotFoundException(string clrType)
        {
            ClrType = clrType;
        }

        public DBTypeNotFoundException(string clrType, Exception ex)
            : base(clrType, ex)
        {
            ClrType = clrType;
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

    [Serializable]
    public class CompilerException : Exception
    {
        public CompilerResults Results { get; set; }

        [NonSerialized]
        private string _realMessage;
        public CompilerException()
        {
            Results = null;
            _realMessage = "Unknown CompilerException";
        }

        public CompilerException(string msg)
        {
            Results = null;
            _realMessage = msg;
        }

        public CompilerException(CompilerResults result)
        {
            Results = result;
            _realMessage = ResultsToMessage(result);
        }

        private static string ResultsToMessage(CompilerResults result)
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine("Unable to compile generated code");
            msg.AppendLine();
            result.Errors.ForEach<CompilerError>(e => msg.AppendLine(e.ToString()));
            return msg.ToString();
        }

        public override string Message
        {
            get
            {
                return _realMessage;
            }
        }

        #region Serialisation

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Results", Results);
        }

        protected CompilerException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            Results = (CompilerResults)info.GetValue("Results", typeof(CompilerResults));
            _realMessage = ResultsToMessage(Results);
        }

        #endregion
    }
}
#endif