using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Kistl.API
{
    [Serializable]
    public class ObjectType
    {
        public ObjectType()
        {
            Namespace = "";
            Classname = "";
        }

        public ObjectType(string type)
        {
            Namespace = "";
            Classname = "";

            string[] segments = type.Split('.');
            Classname = segments.Last();
            Namespace = string.Join(".", segments.Take(segments.Length-1).ToArray());
        }

        public ObjectType(string @namespace, string classname)
        {
            this.Namespace = @namespace;
            this.Classname = classname;
        }


        public string Namespace { get; set; }
        public string Classname { get; set; }

        public string FullNameDataObject
        {
            get
            {
                return string.Format("{0}.{1}, Kistl.Objects", Namespace, Classname);
            }
        }

        public string FullNameServerObject
        {
            get
            {
                return string.Format("{0}.{1}Server, Kistl.Objects.Server", Namespace, Classname);
            }
        }

        public string FullNameClientObject
        {
            get
            {
                return string.Format("{0}.{1}Client, Kistl.Objects.Client", Namespace, Classname);
            }
        }

        public override string ToString()
        {
            return FullNameDataObject;
        }
    }
}
