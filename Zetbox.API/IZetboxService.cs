// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
#if NETFULL
    using System.ServiceModel;
#endif
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Zetbox Stream Service Contract
    /// TODO: Add FaultContracts
    /// TODO: Remove GetListOf
    /// </summary>
#if NETFULL
    [ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace = "http://dasz.at/Zetbox/")]
#endif
    public interface IZetboxService
    {
        /// <summary>
        /// Puts a number of changed objects into the database. The resultant objects are sent back to the client.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="msg">a streamable list of <see cref="IPersistenceObject"/>s</param>
        /// <param name="notificationRequests">A list of objects the client wants to be notified about, if they change.</param>
        /// <returns>a streamable list of <see cref="IPersistenceObject"/>s</returns>
#if NETFULL
        [OperationContract]
        [FaultContract(typeof(Exception))]
        [FaultContract(typeof(ZetboxContextExceptionMessage))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionExceptionMessage))]
#endif
        byte[] SetObjects(Guid version, byte[] msg, ObjectNotificationRequest[] notificationRequests);

        /// <summary>
        /// Returns a list of objects from the datastore, as requested by the query.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="query">A full LINQ query returning zero, one or more objects (FirstOrDefault, Single, Where, Skip, Take, etc.)</param>
        /// <returns>the found objects</returns>
#if NETFULL
        [OperationContract]
        [FaultContract(typeof(Exception))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionException))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionExceptionMessage))]
#endif
        byte[] GetObjects(Guid version, SerializableExpression query);

        /// <summary>
        /// returns a list of objects referenced by a specified Property. Use an equivalent query in GetObjects() instead.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">Object id</param>
        /// <param name="property">Property</param>
        /// <returns>the referenced objects</returns>
#if NETFULL
        [OperationContract]
        [FaultContract(typeof(Exception))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionException))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionExceptionMessage))]
#endif
        byte[] GetListOf(Guid version, SerializableType type, int ID, string property);

        /// <summary>
        /// Fetches a list of CollectionEntry objects of the Relation 
        /// <paramref name="relId"/> which are owned by the object with the 
        /// ID <paramref name="ID"/> in the role <paramref name="role"/>.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="relId">the requested Relation</param>
        /// <param name="role">the parent role (1 == A, 2 == B)</param>
        /// <param name="ID">the ID of the parent object</param>
        /// <returns>the requested collection entries</returns>
#if NETFULL
        [OperationContract]
        [FaultContract(typeof(Exception))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionException))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionExceptionMessage))]
#endif
        byte[] FetchRelation(Guid version, Guid relId, int role, int ID);

        /// <summary>
        /// Gets the content stream of the given Blob instance ID
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="ID">ID of an valid Blob instance</param>
        /// <returns>Stream containing the Blob content</returns>
#if NETFULL
        [OperationContract]
        [FaultContract(typeof(Exception))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionException))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionExceptionMessage))]
#endif
        Stream GetBlobStream(Guid version, int ID);

        /// <summary>
        /// Sets the content stream and creates a new Blob instance
        /// </summary>
        /// <param name="blob">Information about the given blob</param>
        /// <returns>the newly created Blob instance</returns>
#if NETFULL
        [OperationContract]
        [FaultContract(typeof(Exception))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionException))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionExceptionMessage))]
#endif
        BlobResponse SetBlobStream(BlobMessage blob);


        /// <summary>
        /// Invokes a server side method
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="type">Type of the object to invoke method</param>
        /// <param name="ID">ID of the object</param>
        /// <param name="method">Method name</param>
        /// <param name="parameterTypes">Array of method parameter type</param>
        /// <param name="parameter">Array of method parameters</param>
        /// <param name="changedObjects">Array of changed objects to restore context</param>
        /// <param name="notificationRequests">A list of objects the client wants to be notified about, if they change.</param>
        /// <param name="retChangedObjects">Array of changed objects on the server side</param>
        /// <returns></returns>
#if NETFULL
        [OperationContract]
        [FaultContract(typeof(Exception))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionException))]
        [FaultContract(typeof(InvalidZetboxGeneratedVersionExceptionMessage))]
#endif
        byte[] InvokeServerMethod(Guid version, SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests, out byte[] retChangedObjects);
    }

#if NETFULL
    [MessageContract]
#endif
    public class BlobMessage
    {
#if NETFULL
        [MessageHeader]
#endif
        public Guid Version { get; set; }
#if NETFULL
        [MessageHeader]
#endif
        public string FileName { get; set; }
#if NETFULL
        [MessageHeader]
#endif
        public string MimeType { get; set; }
#if NETFULL
        [MessageBodyMember]
#endif
        public System.IO.Stream Stream { get; set; }
    }

#if NETFULL
    [MessageContract]
#endif
    public class BlobResponse
    {
#if NETFULL
        [MessageHeader]
#endif
        public int ID { get; set; }
#if NETFULL
        [MessageBodyMember]
#endif
        public System.IO.Stream BlobInstance { get; set; }
    }


#if NETFULL
    [DataContract(Namespace = "http://dasz.at/Zetbox/")]
#endif
    [Serializable]
    [DebuggerDisplay("{IDs.Length} reqs for {Type.TypeName}")]
    [KnownType(typeof(SerializableType))]
    public class ObjectNotificationRequest
    {
#if NETFULL
        [DataMember(Name = "Type")]
#endif
        public SerializableType Type { get; set; }

#if NETFULL
        [DataMember(Name = "IDs")]
#endif
        public int[] IDs { get; set; }
    }

#if NETFULL
    [DataContract(Namespace = "http://dasz.at/Zetbox/", Name = "OrderBy")]
#endif
    [Serializable]
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
#if NETFULL
        [DataMember(Name = "Type")]
#endif
        public OrderByType Type { get; set; }

#if NETFULL
        [DataMember(Name = "Expression")]
#endif
        public SerializableExpression Expression { get; set; }
    }

#region Exception messages
    [Serializable]
    [XmlRoot(Namespace = "http://dasz.at/zetbox/ZetboxContextExceptionMessage")]
#if NETFULL
    [DataContract(Namespace = "http://dasz.at/Zetbox/")]
#endif
    [KnownType(typeof(ConcurrencyExceptionMessage))]
    [KnownType(typeof(ValidationExceptionMessage))]
    [KnownType(typeof(FKViolationExceptionMessage))]
    [KnownType(typeof(UniqueConstraintViolationExceptionMessage))]
    public class ZetboxContextExceptionMessage
    {
        [XmlElement(typeof(ConcurrencyExceptionMessage), ElementName = "ConcurrencyException")]
        [XmlElement(typeof(ValidationExceptionMessage), ElementName = "ValidationException")]
        [XmlElement(typeof(FKViolationExceptionMessage), ElementName = "FKViolationException")]
        [XmlElement(typeof(UniqueConstraintViolationExceptionMessage), ElementName = "UniqueConstraintViolationException")]
#if NETFULL
        [DataMember]
#endif
        public ZetboxContextExceptionSerializationHelper Exception { get; set; }
    }

    [Serializable]
    public abstract class ZetboxContextExceptionSerializationHelper
    {
        public ZetboxContextExceptionSerializationHelper()
        {
        }

        public ZetboxContextExceptionSerializationHelper(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");
            this.Message = ex.Message;
        }

        public string Message { get; set; }

        public abstract ZetboxContextErrorException ToException();
    }

    [Serializable]
    public class ConcurrencyExceptionMessage : ZetboxContextExceptionSerializationHelper
    {
        public ConcurrencyExceptionMessage()
        {
        }

        public ConcurrencyExceptionMessage(ConcurrencyException ex)
            : base(ex)
        {
            this.Details = ex.Details;
        }

        public override ZetboxContextErrorException ToException()
        {
            return new ConcurrencyException(Message, Details);
        }

#if NETFULL
        [DataMember]
#endif
        public List<ConcurrencyExceptionDetail> Details { get; set; }
    }

    [Serializable]
    public class ValidationExceptionMessage : ZetboxContextExceptionSerializationHelper
    {
        public ValidationExceptionMessage()
        {
        }

        public ValidationExceptionMessage(ZetboxValidationException ex)
            : base(ex)
        {
        }

        public override ZetboxContextErrorException ToException()
        {
            return new ZetboxValidationException(Message);
        }
    }

    [Serializable]
    public class FKViolationExceptionMessage : ZetboxContextExceptionSerializationHelper
    {
        public FKViolationExceptionMessage()
        {

        }

        public FKViolationExceptionMessage(FKViolationException ex)
            : base(ex)
        {
            this.Details = ex.Details;
        }

        public override ZetboxContextErrorException ToException()
        {
            return new FKViolationException(Message, Details);
        }

#if NETFULL
        [DataMember]
#endif
        public List<FKViolationExceptionDetail> Details { get; set; }
    }

    [Serializable]
    public class UniqueConstraintViolationExceptionMessage : ZetboxContextExceptionSerializationHelper
    {
        public UniqueConstraintViolationExceptionMessage()
        {

        }

        public UniqueConstraintViolationExceptionMessage(UniqueConstraintViolationException ex)
            : base(ex)
        {
            this.Details = ex.Details;
        }

        public override ZetboxContextErrorException ToException()
        {
            return new UniqueConstraintViolationException(Message, Details);
        }

#if NETFULL
        [DataMember]
#endif
        public List<UniqueConstraintViolationExceptionDetail> Details { get; set; }
    }

    [Serializable]
    [XmlRoot(Namespace = "http://dasz.at/zetbox/ZetboxContextExceptionMessage")]
#if NETFULL
    [DataContract(Namespace = "http://dasz.at/Zetbox/")]
#endif
    public class InvalidZetboxGeneratedVersionExceptionMessage
    {
#if NETFULL
        [DataMember]
#endif
        public string Message { get; set; }

        public InvalidZetboxGeneratedVersionException ToException()
        {
            return new InvalidZetboxGeneratedVersionException(Message);
        }
    }

#endregion
}
