namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;

    public sealed class EfServerObjectHandlerFactory
        : ServerObjectHandlerFactory
    {
        public EfServerObjectHandlerFactory() { }

        public override IServerCollectionHandler GetServerCollectionHandler(
            IZetboxContext ctx, 
            InterfaceType aType,
            InterfaceType bType,
            RelationEndRole endRole)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            return GetServerCollectionHandlerHelper(
                typeof(ServerCollectionHandler<,,,>),
                ctx.ToImplementationType(aType),
                ctx.ToImplementationType(bType),
                endRole);
        }

        public override IServerObjectHandler GetServerObjectHandler(InterfaceType type)
        {
            return GetServerObjectHandlerHelper(typeof(ServerObjectHandler<>), type);
        }

        public override IServerObjectSetHandler GetServerObjectSetHandler()
        {
            return new ServerObjectSetHandler();
        }

        public override IServerDocumentHandler GetServerDocumentHandler()
        {
            return new ServerDocumentHandler();
        }
    }
}
