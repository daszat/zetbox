using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// Interface for a LinqToNNN Context.
    /// </summary>
    public interface IKistlContext : IDisposable
    {
        /// <summary>
        /// Attach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Attach(IPersistenceObject obj);
        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Detach(IPersistenceObject obj);
        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        void Delete(IPersistenceObject obj);

        /// <summary>
        /// Checks if the object is already attached to this context by comparin ID & Type
        /// </summary>
        /// <param name="type">ObjectType</param>
        /// <param name="ID">ID</param>
        /// <returns>Object in Context or obj</returns>
        IPersistenceObject IsObjectInContext(Type type, int ID);

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        IQueryable<T> GetQuery<T>() where T : IDataObject;
        /// <summary>
        /// Returns a Query by ObjectType
        /// </summary>
        /// <param name="type">ObjectType</param>
        /// <returns>IQueryable</returns>
        IQueryable<IDataObject> GetQuery(ObjectType type);

        List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject;
        List<T> GetListOf<T>(ObjectType type, int ID, string propertyName) where T : IDataObject;

        int SubmitChanges();

        IDataObject Create(Type type);
        IDataObject Create(ObjectType type);
        T Create<T>() where T : IDataObject, new();

        /// <summary>
        /// Find the Object of the given type by ID
        /// </summary>
        IDataObject Find(ObjectType type, int ID);
        /// <summary>
        /// Find the Object of the given type by ID
        /// </summary>
        T Find<T>(int ID) where T : IDataObject;
    }
}
