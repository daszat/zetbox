using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators
{
    public class TypeMoniker
    {
        public TypeMoniker()
        {
            Namespace = "";
            ClassName = "";
        }

        public TypeMoniker(string type)
        {
            Namespace = "";
            ClassName = "";

            string[] segments = type.Split('.');
            ClassName = segments.Last();
            Namespace = string.Join(".", segments.Take(segments.Length - 1).ToArray());
        }

        public TypeMoniker(string @namespace, string classname)
        {
            this.Namespace = @namespace;
            this.ClassName = classname;
        }

        public override bool Equals(object obj)
        {
            TypeMoniker b = obj as TypeMoniker;
            if (b == null) return false;
            return this.Namespace == b.Namespace && this.ClassName == b.ClassName;
        }

        public override int GetHashCode()
        {
            return (Namespace + ClassName).GetHashCode();
        }

        public string Namespace { get; set; }
        public string ClassName { get; set; }


        public string NameDataObject
        {
            get
            {
                if (string.IsNullOrEmpty(Namespace) && string.IsNullOrEmpty(ClassName))
                    return "";
                else
                    return string.Format("{0}.{1}", Namespace, ClassName);
            }
        }

        public override string ToString()
        {
            return NameDataObject;
        }
    }
}
