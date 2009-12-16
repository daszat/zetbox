using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Kistl.API
{
    /// <summary>
    /// Kist Stream Service Contract
    /// TODO: Add FaultContracts
    /// TODO: Remove GetObject
    /// TODO: Remove GetListOf
    /// </summary>
    [ServiceContract]
    public interface IKistlService
    {
        /// <summary>
        /// Gets a single object from the datastore. This method is superseded by using GetList with a (o => o.ID == $id) filter.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID of Object</param>
        /// <returns>a memory stream containing the serialized object, rewound to the beginning</returns>
        /// <exception cref="ArgumentOutOfRangeException">when the specified object was not found</exception>
        [Obsolete]
        [OperationContract]
        [FaultContract(typeof(Exception))]
        MemoryStream GetObject(SerializableType type, int ID);

        /// <summary>
        /// Puts a number of changed objects into the database. The resultant objects are sent back to the client.
        /// </summary>
        /// <param name="msg">a streamable list of <see cref="IPersistenceObject"/>s</param>
        /// <param name="notificationRequests">A list of objects the client wants to be notified about, if they change.</param>
        /// <returns>a streamable list of <see cref="IPersistenceObject"/>s</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        MemoryStream SetObjects(MemoryStream msg, IEnumerable<ObjectNotificationRequest> notificationRequests);

        /// <summary>
        /// Returns a list of objects from the datastore, matching the specified filters.
        /// </summary>
        /// <param name="type">Type of Objects</param>
        /// <param name="maxListCount">Max. ammount of objects</param>
        /// <param name="filter">Serializable linq expression used a filter</param>
        /// <param name="orderBy">List of derializable linq expressions used as orderby</param>
        /// <returns>the found objects</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        MemoryStream GetList(SerializableType type, int maxListCount, SerializableExpression filter, List<SerializableExpression> orderBy);

        /// <summary>
        /// returns a list of objects referenced by a specified Property. Use an equivalent query in GetList() instead.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">Object id</param>
        /// <param name="property">Property</param>
        /// <returns>the referenced objects</returns>
        [Obsolete]
        [OperationContract]
        [FaultContract(typeof(Exception))]
        MemoryStream GetListOf(SerializableType type, int ID, string property);

        /// <summary>
        /// Fetches a list of CollectionEntry objects of the Relation 
        /// <paramref name="relID"/> which are owned by the object with the 
        /// ID <paramref name="ID"/> in the role <paramref name="role"/>.
        /// </summary>
        /// <param name="relId">the requested Relation</param>
        /// <param name="role">the parent role (1 == A, 2 == B)</param>
        /// <param name="ID">the ID of the parent object</param>
        /// <returns>the requested collection entries</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        MemoryStream FetchRelation(Guid relId, int role, int ID);
    }

    [DataContract]
    [DebuggerDisplay("{IDs.Length} reqs for {Type.TypeName}")]
    public class ObjectNotificationRequest
    {
        [DataMember]
        public SerializableType Type { get; set; }

        [DataMember]
        public int[] IDs { get; set; }
    }
}
