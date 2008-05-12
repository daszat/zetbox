using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

namespace Kistl.API
{
    /// TODO: DllIndependentType ???
    /// ODER: gleich durch Type ersetzen
    [Serializable]
    public class ObjectType
    {
        private static string _AssemblyName;
        public static string AssemblyName
        {
            get
            {
                if(string.IsNullOrEmpty(_AssemblyName)) throw new InvalidOperationException("AssemblyName is empty. Missing Init call!");
                return _AssemblyName;
            }
        }

        public static void Init(string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName)) throw new ArgumentException("assemblyName cannot be null or empty");
            _AssemblyName = assemblyName;
        }

        public static void Init(Assembly assembly)
        {
            Init(assembly.FullName);
        }

        public ObjectType()
        {
            Namespace = "";
            Classname = "";
        }

        public ObjectType(IDataObject obj)
        {
            Type t = obj.GetType();
            Namespace = t.Namespace;
            Classname = t.Name;
        }

        public ObjectType(Type type)
        {
            Namespace = "";
            Classname = "";

            string[] segments = type.FullName.Split('.');
            Classname = segments.Last();
            Namespace = string.Join(".", segments.Take(segments.Length - 1).ToArray());
        }

        public ObjectType(string type)
        {
            Namespace = "";
            Classname = "";

            string[] segments = type.Split('.');
            Classname = segments.Last();
            Namespace = string.Join(".", segments.Take(segments.Length - 1).ToArray());
        }

        public ObjectType(string @namespace, string classname)
        {
            this.Namespace = @namespace;
            this.Classname = classname;
        }

        public override bool Equals(object obj)
        {
            ObjectType b = obj as ObjectType;
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

        public string FullNameDataObject
        {
            get
            {
                if (string.IsNullOrEmpty(Namespace) && string.IsNullOrEmpty(Classname))
                    return "";
                else
                    return string.Format("{0}.{1}, {2}", Namespace, Classname, AssemblyName);
            }
        }

        public override string ToString()
        {
            return NameDataObject;
        }

        /// <summary>
        /// Helper Function for generic access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IDataObject NewDataObject()
        {
            if (string.IsNullOrEmpty(this.NameDataObject)) throw new ArgumentException("Type is empty");

            Type t = GetCLRType();

            IDataObject obj = Activator.CreateInstance(t) as IDataObject;
            if (obj == null) throw new InvalidOperationException("Cannot create instance");

            return obj;
        }

        public Type GetCLRType()
        {
            Type t = Type.GetType(this.FullNameDataObject);
            if (t == null) throw new TypeLoadException("Invalid Type " + this.ToString());
            return t;
        }
    }
}
