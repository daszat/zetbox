using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}
