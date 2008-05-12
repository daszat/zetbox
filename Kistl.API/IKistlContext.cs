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
        /// Attach an IPersistenceObject. This Method checks, if the Object is already in that Context. 
        /// If so, it returns the Object in that Context.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        /// <returns>The Object in already Context or obj if not</returns>
        IPersistenceObject Attach(IPersistenceObject obj);
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
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (New Object) then null is returned.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
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

        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject;
        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given Type, ID and PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="type">Type of the Object which holds the BackReferenceProperty</param>
        /// <param name="ID">ID of the Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        List<T> GetListOf<T>(ObjectType type, int ID, string propertyName) where T : IDataObject;

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counded.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        int SubmitChanges();

        /// <summary>
        /// Creates a new IDataObject by Type
        /// </summary>
        /// <param name="type">Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        IDataObject Create(Type type);
        /// <summary>
        /// Creates a new IDataObject by ObjectType. Note - this Method is depricated!
        /// </summary>
        /// <param name="type">ObjectType of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        IDataObject Create(ObjectType type);
        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        T Create<T>() where T : IDataObject, new();

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// Note: This Method is depricated.
        /// </summary>
        /// <param name="type">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        IDataObject Find(ObjectType type, int ID);
        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        T Find<T>(int ID) where T : IDataObject;
    }
}
