using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;

namespace Kistl.Server.Generators
{
    public class DBTypeNotFoundException : Exception
    {
        public DBTypeNotFoundException()
        {
        }

        public DBTypeNotFoundException(string clrType)
        {
        }

        public string CLRType { get; set; }

        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(CLRType))
                {
                    return "Could not resolve CLR Type to Database Type";
                }
                else
                {
                    return string.Format("Could not resolve CLR Type \"{0}\" to Database Type", CLRType);
                }
            }
        }
    }

    public class CompilerException : Exception
    {
        private StringBuilder msg = new StringBuilder();

        public CompilerResults Results { get; set; }

        public CompilerException(CompilerResults result)
        {
            Results = result;

            msg.AppendLine("Unable to compile generated code");
            msg.AppendLine();
            result.Errors.OfType<CompilerError>().ToList().ForEach(e => msg.AppendLine(e.ToString()));
        }

        public override string Message
        {
            get
            {
                return msg.ToString();
            }
        }
    }
}
