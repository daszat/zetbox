using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Server.GeneratorsOld
{
    public class TypeMoniker
    {
        public TypeMoniker()
        {
            Namespace = "";
            Classname = "";
        }

        public TypeMoniker(string type)
        {
            Namespace = "";
            Classname = "";

            string[] segments = type.Split('.');
            Classname = segments.Last();
            Namespace = string.Join(".", segments.Take(segments.Length - 1).ToArray());
        }

        public TypeMoniker(string @namespace, string classname)
        {
            this.Namespace = @namespace;
            this.Classname = classname;
        }

        public override bool Equals(object obj)
        {
            TypeMoniker b = obj as TypeMoniker;
            if (b == null) return false;
            return this.Namespace == b.Namespace && this.Classname == b.Classname;
        }

        public override int GetHashCode()
        {
            return (Namespace + Classname).GetHashCode();
        }

        public string Namespace { get; set; }
        public string Classname { get; set; }


        public string NameDataObject
        {
            get
            {
                if (string.IsNullOrEmpty(Namespace) && string.IsNullOrEmpty(Classname))
                    return "";
                else
                    return string.Format("{0}.{1}", Namespace, Classname);
            }
        }

        public override string ToString()
        {
            return NameDataObject;
        }
    }
}
