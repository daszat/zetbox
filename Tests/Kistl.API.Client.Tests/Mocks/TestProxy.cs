using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using System.Reflection;
using Kistl.App.Test;

namespace Kistl.API.Client.Tests
{
    public class TestProxy 
        : IProxy
    {
        private int newID = 10;

        public void Generate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDataObject> GetList(IKistlContext ctx, InterfaceType ifType, int maxListCount, bool withEagerLoading, IEnumerable<Expression> filter, IEnumerable<Expression> orderBy, out List<IStreamable> auxObjects)
        {
            if (ifType == null) throw new ArgumentNullException("ifType");            
            if (orderBy != null) throw new ArgumentException("OrderBy is not supported yet");

            auxObjects = new List<IStreamable>();
            IEnumerable<IDataObject> query;

            if (ifType == typeof(TestObjClass))
            {
                query = GetList_TestObjClass();
            }
            else
            {
                throw new ArgumentOutOfRangeException("ifType", "Only TestObjClasses are allowed");
            }

            if (filter != null)
            {
                var result = query.AsQueryable().AddCast(ifType.Type);
                filter.ForEach(f => result = result.AddFilter(f.StripQuotes()));
                return result.Cast<IDataObject>().ToList();
            }
            else
            {
                return query.Cast<IDataObject>();
            }
        }

        private static IEnumerable<IDataObject> GetList_TestObjClass()
        {
            List<TestObjClass> result = new List<TestObjClass>();
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 1 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 2, _fk_ObjectProp = 1 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 3, _fk_ObjectProp = 1 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 4 });
            result.Add(new TestObjClass__Implementation__() { StringProp = "String " + 5 });

            result[0].SetPrivatePropertyValue<int>("ID", 1);
            result[1].SetPrivatePropertyValue<int>("ID", 2);
            result[2].SetPrivatePropertyValue<int>("ID", 3);
            result[3].SetPrivatePropertyValue<int>("ID", 4);
            result[4].SetPrivatePropertyValue<int>("ID", 5);
            return result.Cast<IDataObject>();
        }

        public IEnumerable<IDataObject> GetListOf(IKistlContext ctx, InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            if (ifType == null) throw new ArgumentNullException("ifType");
            if (ifType != typeof(TestObjClass)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");
            auxObjects = new List<IStreamable>();

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

        public string HelloWorld(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            var result = new List<IPersistenceObject>();
            foreach (var obj in objects)
            {
                Type type = obj.GetType();
                if (type == null) throw new ArgumentNullException("type");
                if (type != typeof(TestObjClass__Implementation__)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");

                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    var newObj = new TestObjClass__Implementation__();

                    // Copy old object to new object
                    newObj.ApplyChangesFrom(obj);
                    if (newObj.ID < Helper.INVALIDID)
                    {
                        newObj.SetPrivatePropertyValue<int>("ID", ++newID);
                    }
                    result.Add(newObj);
                    SetPrivateFieldValue<DataObjectState>(newObj, "_ObjectState", DataObjectState.Unmodified);
                }
            }

            return result;
        }

        /// <summary>
        /// Set a private Property Value on a given Object. Uses Reflection.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <param name="val">the value to set</param>
        /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
        public static void SetPrivateFieldValue<T>(object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
            fi.SetValue(obj, val);
        }

        public IEnumerable<T> FetchRelation<T>(IKistlContext ctx, Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects) where T : class, IRelationCollectionEntry
        {
            auxObjects = new List<IStreamable>();
            return new List<T>();
        }

        public void Dispose()
        {
        }

        public System.IO.Stream GetBlobStream(int ID)
        {
            throw new NotImplementedException();
        }

        public Kistl.App.Base.Blob SetBlobStream(IKistlContext ctx, System.IO.Stream stream, string filename, string mimetype)
        {
            throw new NotImplementedException();
        }
    }
}
