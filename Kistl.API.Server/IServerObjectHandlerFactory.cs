
namespace Kistl.API.Server
{
    using System;

    public interface IServerObjectHandlerFactory
    {
        /// <summary>
        /// Creates an <see cref="IServerCollectionHandler"/> for the specified Relation.
        /// </summary>
        /// <param name="aType">The type of the A-side of the Relation.</param>
        /// <param name="bType">The type of the B-side of the Relation.</param>
        /// <param name="endRole">The "parent"-side of the collection.</param>
        /// <returns>A newly initialised <see cref="IServerCollectionHandler"/>.</returns>
        IServerCollectionHandler GetServerCollectionHandler(InterfaceType aType, InterfaceType bType, RelationEndRole endRole);

        /// <summary>
        /// Creates an <see cref="IServerObjectHandler"/> for the specified Type.
        /// </summary>
        /// <param name="type">The Type which should be handled by the <see cref="IServerObjectHandler"/>.</param>
        /// <returns>A newly initialised <see cref="IServerObjectHandler"/>.</returns>
        IServerObjectHandler GetServerObjectHandler(InterfaceType type);

        /// <summary>
        /// Creates an <see cref="IServerObjectSetHandler"/>.
        /// </summary>
        /// <returns>A newly initialised <see cref="IServerObjectSetHandler"/>.</returns>
        IServerObjectSetHandler GetServerObjectSetHandler();

        /// <summary>
        /// Creates an <see cref="IServerDocumentHandler"/>.
        /// </summary>
        /// <returns>A newly initialised <see cref="IServerDocumentHandler"/>.</returns>
        IServerDocumentHandler GetServerDocumentHandler();
    }
}
