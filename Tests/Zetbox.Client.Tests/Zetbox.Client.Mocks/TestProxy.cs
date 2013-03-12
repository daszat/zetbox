// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Zetbox.API;
using Zetbox.API.Client;
using System.Reflection;
using Zetbox.App.Test;

namespace Zetbox.Client.Mocks
{
    public class TestProxy 
        : IProxy
    {
        private int newID = 10;

        public void Generate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDataObject> GetList( InterfaceType ifType, int maxListCount, bool withEagerLoading, IEnumerable<Expression> filter, IEnumerable<OrderBy> orderBy, out List<IStreamable> auxObjects)
        {
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
            result.Add(new TestObjClassImpl() { StringProp = "String " + 1 });
            result.Add(new TestObjClassImpl() { StringProp = "String " + 2, _fk_ObjectProp = 1 });
            result.Add(new TestObjClassImpl() { StringProp = "String " + 3, _fk_ObjectProp = 1 });
            result.Add(new TestObjClassImpl() { StringProp = "String " + 4 });
            result.Add(new TestObjClassImpl() { StringProp = "String " + 5 });

            result[0].SetPrivatePropertyValue<int>("ID", 1);
            result[1].SetPrivatePropertyValue<int>("ID", 2);
            result[2].SetPrivatePropertyValue<int>("ID", 3);
            result[3].SetPrivatePropertyValue<int>("ID", 4);
            result[4].SetPrivatePropertyValue<int>("ID", 5);
            return result.Cast<IDataObject>();
        }

        public IEnumerable<IDataObject> GetListOf(InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            if (ifType != typeof(TestObjClass)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");
            auxObjects = new List<IStreamable>();

            List<TestObjClass> result = new List<TestObjClass>();
            if (ID == 1)
            {
                result.Add(new TestObjClassImpl() { StringProp = "String " + 2 });
                result.Add(new TestObjClassImpl() { StringProp = "String " + 3 });
                result[0].SetPrivatePropertyValue<int>("ID", 2);
                result[1].SetPrivatePropertyValue<int>("ID", 3);
            }

            return result.Cast<IDataObject>();
        }

        public string HelloWorld(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> SetObjects(IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            var result = new List<IPersistenceObject>();
            foreach (var obj in objects)
            {
                Type type = obj.GetType();
                if (type == null) throw new ArgumentNullException("type");
                if (type != typeof(TestObjClassImpl)) throw new ArgumentOutOfRangeException("type", "Only TestObjClasses are allowed");

                if (obj.ObjectState != DataObjectState.Deleted)
                {
                    var newObj = new TestObjClassImpl();

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

        public IEnumerable<T> FetchRelation<T>(Guid relationId, RelationEndRole role, int parentId, InterfaceType parentIfType, out List<IStreamable> auxObjects) where T : class, IRelationEntry
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

        public Zetbox.App.Base.Blob SetBlobStream(System.IO.Stream stream, string filename, string mimetype)
        {
            throw new NotImplementedException();
        }

        public object InvokeServerMethod(InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests, out IEnumerable<IPersistenceObject> changedObjects, out List<IStreamable> auxObjects)
        {
            throw new NotImplementedException();
        }
    }
}
