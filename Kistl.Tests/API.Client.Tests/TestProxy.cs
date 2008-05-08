using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;

namespace API.Client.Tests
{
    public class TestProxy : Kistl.API.Client.IProxy
    {
        #region IProxy Members

        public void Generate()
        {
            throw new NotImplementedException();
        }

        public System.Collections.IEnumerable GetList(Kistl.API.IKistlContext ctx, Kistl.API.ObjectType type, int maxListCount, System.Linq.Expressions.Expression filter, System.Linq.Expressions.Expression orderBy)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (!type.Equals(new ObjectType(typeof(TestObjClass)))) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");
            if (filter != null) throw new ArgumentException("Filter is not supported yet");
            if (orderBy != null) throw new ArgumentException("OrderBy is not supported yet");

            List<TestObjClass> result = new List<TestObjClass>();
            result.Add(new TestObjClass() { ID = 1, StringProp = "String " + 1 });
            result.Add(new TestObjClass() { ID = 2, StringProp = "String " + 2 });
            result.Add(new TestObjClass() { ID = 3, StringProp = "String " + 3 });
            result.Add(new TestObjClass() { ID = 4, StringProp = "String " + 4 });
            result.Add(new TestObjClass() { ID = 5, StringProp = "String " + 5 });

            return result;
        }

        public System.Collections.IEnumerable GetListOf(Kistl.API.IKistlContext ctx, Kistl.API.ObjectType type, int ID, string property)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (!type.Equals(new ObjectType(typeof(TestObjClass)))) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");

            List<TestObjClass> result = new List<TestObjClass>();
            if (ID == 1)
            {
                result.Add(new TestObjClass() { ID = 2, StringProp = "String " + 2 });
                result.Add(new TestObjClass() { ID = 3, StringProp = "String " + 3 });
            }

            return result;
        }

        public Kistl.API.IDataObject GetObject(Kistl.API.IKistlContext ctx, Kistl.API.ObjectType type, int ID)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (!type.Equals(new ObjectType(typeof(TestObjClass)))) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");

            TestObjClass obj = new TestObjClass() { ID = ID, StringProp = "String " + ID };
            return obj;
        }

        public string HelloWorld(string name)
        {
            throw new NotImplementedException();
        }

        public Kistl.API.IDataObject SetObject(Kistl.API.IKistlContext ctx, Kistl.API.ObjectType type, Kistl.API.IDataObject obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
