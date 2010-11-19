
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;

    public class NHibernateServerObjectHandler<T>
       : BaseServerObjectHandler<T>
       where T : class, IDataObject
    {
        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected override T GetObjectInstance(IKistlContext ctx, int ID)
        {
            if (ID < Kistl.API.Helper.INVALIDID)
            {
                //// new object -> look in current context
                //ObjectContext efCtx = ((KistlDataContext)ctx).ObjectContext;
                //return (T)efCtx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added)
                //    .FirstOrDefault(e => e.Entity is IDataObject && ((IDataObject)e.Entity).ID == ID).Entity;
                throw new NotImplementedException();
            }
            else
            {
                return ctx.GetQuery<T>().FirstOrDefault<T>(a => a.ID == ID);
            }
        }
    }

}
