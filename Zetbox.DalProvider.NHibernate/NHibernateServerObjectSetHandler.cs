
namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Server;

    public class NHibernateServerObjectSetHandler
        : BaseServerObjectSetHandler
    {
        /// <inheritdoc/>
        public override IEnumerable<IPersistenceObject> SetObjects(Guid version, IZetboxContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            return base.SetObjects(version, ctx, objects, notificationRequests);
        }
    }

}
