
namespace Kistl.DalProvider.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Test;
    
    public class ProxyMock : IProxy
    {
        private int newID = 10;
        private InterfaceType.Factory _iftFactory;

        public ProxyMock(InterfaceType.Factory iftFactory)
        {
            this._iftFactory = iftFactory;
        }

        public void Generate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDataObject> GetList(IKistlContext ctx, InterfaceType ifType, int maxListCount, bool withEagerLoading, IEnumerable<Expression> filter, IEnumerable<OrderBy> orderBy, out List<IStreamable> auxObjects)
        {
            if (orderBy != null) throw new ArgumentException("OrderBy is not supported yet");

            auxObjects = new List<IStreamable>();
            IEnumerable<IDataObject> query;

            if (ifType == typeof(TestObjClass))
            {
                query = GetList_TestObjClass(ctx);
            }
            else if (ifType == typeof(TestCustomObject))
            {
                query = GetList_TestCustomObject(ctx);
            }
            else if (ifType == typeof(Muhblah))
            {
                query = GetList_Muhblah(ctx);
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

        private T CreateInstance<T>(IKistlContext ctx, int id)
        {
            InterfaceType ifType = _iftFactory(typeof(T));
            return (T)CreateInstance(ctx, ifType, id);
        }

        private static IPersistenceObject CreateInstance(IKistlContext ctx, InterfaceType ifType, int id)
        {
            var result = (IPersistenceObject)Activator.CreateInstance(ctx.ToImplementationType(ifType).Type);
            result.SetPrivatePropertyValue<int>("ID", id);
            return result;
        }

        private IEnumerable<IDataObject> GetList_Muhblah(IKistlContext ctx)
        {
            var result = new List<Muhblah>();
            result.Add(CreateInstance<Muhblah>(ctx, 1));
            result.Add(CreateInstance<Muhblah>(ctx, 2));
            result.Add(CreateInstance<Muhblah>(ctx, 3));
            result.Add(CreateInstance<Muhblah>(ctx, 4));
            result.Add(CreateInstance<Muhblah>(ctx, 5));
            return result.Cast<IDataObject>();
        }

        private IEnumerable<IDataObject> GetList_TestCustomObject(IKistlContext ctx)
        {
            var result = new List<TestCustomObject>();
            result.Add(CreateInstance<TestCustomObject>(ctx, 1));
            result.Add(CreateInstance<TestCustomObject>(ctx, 2));
            result.Add(CreateInstance<TestCustomObject>(ctx, 3));
            result.Add(CreateInstance<TestCustomObject>(ctx, 4));
            result.Add(CreateInstance<TestCustomObject>(ctx, 5));
            return result.Cast<IDataObject>();
        }

        private IEnumerable<IDataObject> GetList_TestObjClass(IKistlContext ctx)
        {
            var result = new List<TestObjClass>();
            result.Add(CreateInstance<TestObjClass>(ctx, 1));
            result.Add(CreateInstance<TestObjClass>(ctx, 2));
            result.Add(CreateInstance<TestObjClass>(ctx, 3));
            result.Add(CreateInstance<TestObjClass>(ctx, 4));
            result.Add(CreateInstance<TestObjClass>(ctx, 5));

            result[0].StringProp = "String 1";

            result[1].StringProp = "String 2";
            //result[1].SetPrivatePropertyValue<int>("fk_Parent", 1);

            result[2].StringProp = "String 3";
            //result[2].SetPrivatePropertyValue<int>("fk_Parent", 1);

            result[3].StringProp = "String 4";

            result[4].StringProp = "String 5";

            return result.Cast<IDataObject>();
        }

        public IEnumerable<IDataObject> GetListOf(IKistlContext ctx, InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            if (ifType != typeof(TestObjClass)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");
            auxObjects = new List<IStreamable>();

            List<TestObjClass> result = new List<TestObjClass>();
            if (ID == 1)
            {
                result.Add(CreateInstance<TestObjClass>(ctx, 2));
                result.Add(CreateInstance<TestObjClass>(ctx, 3));

                result[0].StringProp = "String 2";
                //result[0].SetPrivatePropertyValue<int>("fk_Parent", 1);

                result[1].StringProp = "String 3";
                //result[1].SetPrivatePropertyValue<int>("fk_Parent", 1);
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
                var type = ctx.GetInterfaceType(obj);

                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    var newObj = CreateInstance(ctx, type, 0);

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

        public IEnumerable<T> FetchRelation<T>(IKistlContext ctx, Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects) where T : class, IRelationEntry
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

        public object InvokeServerMethod(IKistlContext ctx, InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests, out IEnumerable<IPersistenceObject> changedObjects)
        {
            throw new NotImplementedException();
        }
    }
}
