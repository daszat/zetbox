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
        void SetObject(DataContext ctx, string xml);
    }

    public class ServerObjectHelper
    {
        public static List<T> GetList<T>(DataContext ctx) where T : class, IDataObject
        {
            var result = from a in ctx.GetTable<T>()
                         select a;

            return result.ToList<T>();
        }

        public static IEnumerable GetListOf<T>(DataContext ctx, int ID, string property) where T : class, IDataObject
        {
            T obj = GetObject<T>(ctx, ID);

            PropertyInfo pi = typeof(T).GetProperty(property);
            if (pi == null) throw new ArgumentException("Property does not exist");

            return pi.GetValue(obj, null) as IEnumerable;
        }

        public static T GetObject<T>(DataContext ctx, int ID) where T : class, IDataObject
        {
            var result = from a in ctx.GetTable<T>()
                         where a.ID == ID
                         select a;
            return result.Single<T>();
        }

        public static void SetObject<T>(DataContext ctx, T obj) where T : class, IDataObject
        {
            ctx.GetTable<T>().Attach(obj, true);
            ctx.SubmitChanges();
        }
    }
}
