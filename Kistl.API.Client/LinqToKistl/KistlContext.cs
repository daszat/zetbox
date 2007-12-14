using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.API.Client
{
    public class KistlContext : IDisposable
    {
        public KistlContextQuery<BaseClientDataObject> GetQuery(ObjectType type)
        {
            return new KistlContextQuery<BaseClientDataObject>(this, type);
        }

        public KistlContextQuery<T> GetQuery<T>() where T : BaseClientDataObject
        {
            return new KistlContextQuery<T>(this, new ObjectType(typeof(T)));
        }

        public List<T> GetListOf<T>(ObjectType type, int ID, string propertyName)
        {
            KistlContextQuery<T> query = new KistlContextQuery<T>(this, type);
            return ((KistlContextProvider<T>)query.Provider).GetListOf(ID, propertyName);
        }

        public List<T> GetListOf<T>(BaseClientDataObject obj, string propertyName)
        {
            KistlContextQuery<T> query = new KistlContextQuery<T>(this, obj.Type);
            return ((KistlContextProvider<T>)query.Provider).GetListOf(obj.ID, propertyName);
        }

        public T Create<T>() where T : BaseClientDataObject, new()
        {
            T obj = new T();
            Attach(obj);
            return obj;
        }

        public BaseClientDataObject Create(ObjectType type)
        {
            IClientObject client = ClientObjectFactory.GetClientObject(type);
            BaseClientDataObject obj = client.CreateNewGeneric();
            Attach(obj);
            return obj;
        }

        public void Attach(BaseClientDataObject obj)
        {
            obj.AttachToContext(this);
        }

        public void Dettach(BaseClientDataObject obj)
        {
            obj.DetachFromContext(this);
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
