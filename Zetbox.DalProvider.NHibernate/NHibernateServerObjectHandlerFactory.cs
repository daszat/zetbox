
namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;

    public sealed class NHibernateServerObjectHandlerFactory
        : ServerObjectHandlerFactory
    {
        public NHibernateServerObjectHandlerFactory() { }

        public override IServerCollectionHandler GetServerCollectionHandler(
            IZetboxContext ctx, 
            InterfaceType aType,
            InterfaceType bType,
            RelationEndRole endRole)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            return GetServerCollectionHandlerHelper(
                typeof(NHibernateServerCollectionHandler<,,,>),
                ctx.ToImplementationType(aType),
                ctx.ToImplementationType(bType),
                endRole);
        }

        public override IServerObjectHandler GetServerObjectHandler(InterfaceType type)
        {
            return GetServerObjectHandlerHelper(typeof(NHibernateServerObjectHandler<>), type);
        }

        public override IServerObjectSetHandler GetServerObjectSetHandler()
        {
            return new NHibernateServerObjectSetHandler();
        }

        public override IServerDocumentHandler GetServerDocumentHandler()
        {
            return new ServerDocumentHandler();
        }
    }
}
