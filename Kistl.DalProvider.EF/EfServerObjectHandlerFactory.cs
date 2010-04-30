namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Server;

    public sealed class EfServerObjectHandlerFactory
        : ServerObjectHandlerFactory
    {
        public EfServerObjectHandlerFactory() { }

        public override IServerCollectionHandler GetServerCollectionHandler(
            InterfaceType aType,
            InterfaceType bType,
            RelationEndRole endRole)
        {
            return GetServerCollectionHandlerHelper(
                typeof(ServerCollectionHandler<,,,>),
                aType.ToImplementationType(),
                bType.ToImplementationType(),
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
