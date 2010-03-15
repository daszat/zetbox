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
            Namespace = String.Empty;
            Name = String.Empty;
        }

        public TypeMoniker(string type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            Namespace = String.Empty;
            Name = String.Empty;

            string[] segments = type.Split('.');
            Name = segments.Last();
            Namespace = string.Join(".", segments.Take(segments.Length - 1).ToArray());
        }

        public TypeMoniker(string @namespace, string classname)
        {
            this.Namespace = @namespace;
            this.Name = classname;
        }

        public override bool Equals(object obj)
        {
            TypeMoniker b = obj as TypeMoniker;
            if (b == null) return false;
            return this.Namespace == b.Namespace && this.Name == b.Name;
        }

        public override int GetHashCode()
        {
            return (Namespace + Name).GetHashCode();
        }

        public string Namespace { get; set; }
        public string Name { get; set; }


        public string NameDataObject
        {
            get
            {
                if (string.IsNullOrEmpty(Namespace) && string.IsNullOrEmpty(Name))
                    return String.Empty;
                else
                    return string.Format("{0}.{1}", Namespace, Name);
            }
        }

        public override string ToString()
        {
            return NameDataObject;
        }
    }
}
