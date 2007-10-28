using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public interface IDataObject
    {
        int ID { get; set; }
    }

    public class ServerObjectAttribute : Attribute
    {
        public string FullName { get; set; }
    }

    public class ClientObjectAttribute : Attribute
    {
        public string FullName { get; set; }
    }
}
