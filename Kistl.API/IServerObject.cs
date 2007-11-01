using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Linq;
using System.Xml;
using System.Reflection;

namespace Kistl.API
{
    public interface IServerObject
    {
        string GetList(DataContext ctx);
        string GetListOf(DataContext ctx, int ID, string property);
        string GetObject(DataContext ctx, int ID);
        string SetObject(DataContext ctx, string xml);
    }

    public delegate void ServerObjectHandler<T>(DataContext ctx, T obj) where T : class, IDataObject, new();

    public class ServerObject<T> : IServerObject where T : class, IDataObject, new()
    {
        public string GetList(DataContext ctx)
        {
            var result = from a in ctx.GetTable<T>()
                         select a;

            List<T> list = result.ToList<T>();
            return list.ToXmlString();
        }

        public string GetListOf(DataContext ctx, int ID, string property)
        {
            if (ID == API.Helper.INVALIDID) throw new ArgumentException("ID must not be invalid");
            T obj = GetObjectInstance(ctx, ID);
            if (obj == null) throw new ApplicationException("Object not found");

            PropertyInfo pi = typeof(T).GetProperty(property);
            if (pi == null) throw new ArgumentException("Property does not exist");

            IEnumerable list = pi.GetValue(obj, null) as IEnumerable;
            return list.ToXmlString();
        }

        public T GetObjectInstance(DataContext ctx, int ID)
        {
            var result = from a in ctx.GetTable<T>()
                         where a.ID == ID
                         select a;
            return result.Single<T>();
        }

        public string GetObject(DataContext ctx, int ID)
        {
            T obj = GetObjectInstance(ctx, ID);
            return obj.ToXmlString();
        }

        public event ServerObjectHandler<T> OnPreSetObject = null;
        public event ServerObjectHandler<T> OnPostSetObject = null;

        public string SetObject(DataContext ctx, string xml)
        {
            T obj = xml.FromXmlString<T>();

            if (OnPreSetObject != null) OnPreSetObject(ctx, obj);

            if (obj.ID != API.Helper.INVALIDID)
            {
                ctx.GetTable<T>().Attach(obj, true);
            }
            else
            {
                ctx.GetTable<T>().Add(obj);
            }
            ctx.SubmitChanges();

            if (OnPostSetObject != null) OnPostSetObject(ctx, obj);

            return obj.ToXmlString();
        }
    }
}
