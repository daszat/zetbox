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
    /// TODO: Remove GetListOf
    /// </summary>
    [ServiceContract(SessionMode=SessionMode.NotAllowed, Namespace="http://dasz.at/ZBox/")]
    public interface IKistlService
    {
        /// <summary>
        /// Puts a number of changed objects into the database. The resultant objects are sent back to the client.
        /// </summary>
        /// <param name="msg">a streamable list of <see cref="IPersistenceObject"/>s</param>
        /// <param name="notificationRequests">A list of objects the client wants to be notified about, if they change.</param>
        /// <returns>a streamable list of <see cref="IPersistenceObject"/>s</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        byte[] SetObjects(byte[] msg, ObjectNotificationRequest[] notificationRequests);

        /// <summary>
        /// Returns a list of objects from the datastore, matching the specified filters.
        /// </summary>
        /// <param name="type">Type of Objects</param>
        /// <param name="maxListCount">Max. ammount of objects</param>
        /// <param name="eagerLoadLists">If true list properties will be eager loaded</param>
        /// <param name="filter">Serializable linq expression used a filter</param>
        /// <param name="orderBy">List of derializable linq expressions used as orderby</param>
        /// <returns>the found objects</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        byte[] GetList(SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy);

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
        byte[] GetListOf(SerializableType type, int ID, string property);

        /// <summary>
        /// Fetches a list of CollectionEntry objects of the Relation 
        /// <paramref name="relId"/> which are owned by the object with the 
        /// ID <paramref name="ID"/> in the role <paramref name="role"/>.
        /// </summary>
        /// <param name="relId">the requested Relation</param>
        /// <param name="role">the parent role (1 == A, 2 == B)</param>
        /// <param name="ID">the ID of the parent object</param>
        /// <returns>the requested collection entries</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        byte[] FetchRelation(Guid relId, int role, int ID);

        /// <summary>
        /// Gets the content stream of the given Blob instance ID
        /// </summary>
        /// <param name="ID">ID of an valid Blob instance</param>
        /// <returns>Stream containing the Blob content</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        Stream GetBlobStream(int ID);

        /// <summary>
        /// Sets the content stream and creates a new Blob instance
        /// </summary>
        /// <param name="blob">Information about the given blob</param>
        /// <returns>the newly created Blob instance</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BlobResponse SetBlobStream(BlobMessage blob);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        byte[] InvokeServerMethod(SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests, out byte[] retChangedObjects);
    }

    [MessageContract]
    public class BlobMessage
    {
        [MessageHeader]
        public string FileName { get; set; }
        [MessageHeader]
        public string MimeType { get; set; }
        [MessageBodyMember]
        public System.IO.Stream Stream { get; set; }
    }

    [MessageContract]
    public class BlobResponse
    {
        [MessageHeader]
        public int ID { get; set; }
        [MessageBodyMember]
        public System.IO.Stream BlobInstance { get; set; }
    }


    [DataContract(Namespace = "http://dasz.at/ZBox/")]
    [Serializable]
    [DebuggerDisplay("{IDs.Length} reqs for {Type.TypeName}")]
    [KnownType(typeof(SerializableType))]
    public class ObjectNotificationRequest
    {
        [DataMember(Name = "Type")]
        public SerializableType Type { get; set; }

        [DataMember(Name = "IDs")]
        public int[] IDs { get; set; }
    }

    [DataContract(Namespace = "http://dasz.at/ZBox/", Name="OrderBy")]
    [KnownType(typeof(SerializableType))]
    [KnownType(typeof(SerializableBinaryExpression))]
    [KnownType(typeof(SerializableConditionalExpression))]
    [KnownType(typeof(SerializableConstantExpression))]
    [KnownType(typeof(SerializableCompoundExpression))]
    [KnownType(typeof(SerializableLambdaExpression))]
    [KnownType(typeof(SerializableMemberExpression))]
    [KnownType(typeof(SerializableMethodCallExpression))]
    [KnownType(typeof(SerializableNewExpression))]
    [KnownType(typeof(SerializableParameterExpression))]
    [KnownType(typeof(SerializableUnaryExpression))]
    public class OrderByContract
    {
        [DataMember(Name = "Type")]
        public OrderByType Type { get; set; }

        [DataMember(Name = "Expression")]
        public SerializableExpression Expression { get; set; }
    }
}
