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
        private static bool _asClient = false;
        private static bool _asServer = false;

        public static void AsClient()
        {
            _asClient = true;
        }

        public static void AsServer()
        {
            _asServer = true;
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
                return string.Format("{0}.{1}", Namespace, Classname);
            }
        }

        public string FullNameClientDataObject
        {
            get
            {
                return string.Format("{0}.{1}, Kistl.Objects.Client", Namespace, Classname);
            }
        }

        public string FullNameServerDataObject
        {
            get
            {
                return string.Format("{0}.{1}, Kistl.Objects.Server", Namespace, Classname);
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

            Type t;
            if (_asClient) t = Type.GetType(this.FullNameClientDataObject);
            else if (_asServer) t = Type.GetType(this.FullNameServerDataObject);
            else throw new InvalidOperationException("ObjectType not set to Server or Client Object Type. Pleas call AsServer() or AsClient() during startup phase.");

            if (t == null) throw new ApplicationException("Invalid Type " + this.ToString());

            IDataObject obj = Activator.CreateInstance(t) as IDataObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }
    }
}
