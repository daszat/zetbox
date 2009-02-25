using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using System.Linq.Expressions;

namespace Kistl.API.Client.Tests
{
    public class TestProxy : Kistl.API.Client.IProxy
    {
        private int newID = 10;

        #region IProxy Members

        public void Generate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDataObject> GetList(Type type, int maxListCount, System.Linq.Expressions.Expression filter, System.Linq.Expressions.Expression orderBy)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (type != typeof(TestObjClass)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");
            if (orderBy != null) throw new ArgumentException("OrderBy is not supported yet");

            List<TestObjClass> result = new List<TestObjClass>();
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 1 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 2, fk_Parent = 1 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 3, fk_Parent = 1 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 4 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 5 });

            result[0].SetPrivatePropertyValue<int>("ID", 1);
            result[1].SetPrivatePropertyValue<int>("ID", 2);
            result[2].SetPrivatePropertyValue<int>("ID", 3);
            result[3].SetPrivatePropertyValue<int>("ID", 4);
            result[4].SetPrivatePropertyValue<int>("ID", 5);

            if (filter != null)
            {
                filter = filter.StripQuotes();
                if (filter is LambdaExpression && ((LambdaExpression)filter).Parameters[0].Type == typeof(IDataObject))
                {
                    return result.Cast<IDataObject>().AsQueryable<IDataObject>().AddFilter(filter).ToList();
                }
                else
                {
                    result = result.AsQueryable<TestObjClass>().AddFilter(filter).ToList();
                }
            }
            return result.Cast<IDataObject>();
        }

        public IEnumerable<IDataObject> GetListOf(Type type, int ID, string property)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (type != typeof(TestObjClass)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");

            List<TestObjClass> result = new List<TestObjClass>();
            if (ID == 1)
            {
                result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 2 });
                result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 3 });
                result[0].SetPrivatePropertyValue<int>("ID", 2);
                result[1].SetPrivatePropertyValue<int>("ID", 3);
            }

            return result.Cast<IDataObject>();
        }

        public Kistl.API.IDataObject GetObject(Type type, int ID)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (type != typeof(TestObjClass)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");

            TestObjClass obj = new TestObjClass__Implementation__() { StringProp = "String " + ID };
            obj.SetPrivatePropertyValue<int>("ID", ID);
            return obj;
        }

        public string HelloWorld(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Kistl.API.IDataObject> SetObjects(IEnumerable<Kistl.API.IDataObject> objects)
        {
            List<IDataObject> result = new List<IDataObject>();
            foreach (IDataObject obj in objects)
            {
                Type type = obj.GetType();
                if (type == null) throw new ArgumentNullException("type");
                if (type != typeof(TestObjClass__Implementation__)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");

                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    TestObjClass newObj = new TestObjClass__Implementation__();

                    // Copy old object to new object
                    ((BaseClientDataObject)obj).ApplyChanges(newObj);
                    if (newObj.ID < Helper.INVALIDID)
                    {
                        newObj.SetPrivatePropertyValue<int>("ID", ++newID);
                    }
                    result.Add(newObj);
                    newObj.SetPrivatePropertyValue<DataObjectState>("ObjectState", DataObjectState.Unmodified);
                }
            }

            return result;
        }

        public IEnumerable<T> FetchRelation<A, B, T>(Type ceType, RelationEndRole role, IDataObject parent) where T : INewCollectionEntry<A, B>
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
