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

namespace Kistl.API
{
    /// <summary>
    /// Message for the WCF Stream Interface
    /// </summary>
    public class KistlServiceStreamsMessage
    {
        /// <summary>
        /// Type of the IDataObject
        /// </summary>
        public SerializableType Type {get; set;}
        /// <summary>
        /// ID of the IDataObject
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Propertyname, used in the GetListOf Call
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Max. size of a Result Set.
        /// </summary>
        public int MaxListCount { get; set; }
        /// <summary>
        /// Filter of a ResultSet as a Expression Tree
        /// </summary>
        public SerializableExpression Filter { get; set; }
        /// <summary>
        /// OrderBy of a ResultSet as a Expression Tree
        /// </summary>
        public List<SerializableExpression> OrderBy { get; set; }

        /// <summary>
        /// Create a new Message
        /// </summary>
        public KistlServiceStreamsMessage()
        {
            MaxListCount = Helper.MAXLISTCOUNT;
        }

        /// <summary>
        /// Deserializes a Message from the given Stream.
        /// </summary>
        /// <param name="msg">Stream</param>
        public KistlServiceStreamsMessage(Stream msg)
        {
            FromStream(msg);
        }

        /// <summary>
        /// Serializes the Message to a Stream.
        /// </summary>
        /// <param name="msg">Stream.</param>
        public void ToStream(Stream msg)
        {
            BinaryWriter sw = new BinaryWriter(msg);

            BinarySerializer.ToStream(Type, sw);
            sw.Write(ID);
            sw.Write(Property ?? "");

            sw.Write(MaxListCount);
            BinarySerializer.ToStream(Filter, sw);
            if (OrderBy != null)
            {
                foreach (var o in OrderBy)
                {
                    BinarySerializer.ToStream(true, sw);
                    BinarySerializer.ToStream(o, sw);
                }
            }
            BinarySerializer.ToStream(false, sw);
        }

        /// <summary>
        /// Serializes the Message to a Stream.
        /// </summary>
        /// <returns>new MemoryStream</returns>
        public MemoryStream ToStream()
        {
            MemoryStream s = new MemoryStream();
            ToStream(s);
            return s;
        }

        /// <summary>
        /// Deserializes a Message from the given Stream.
        /// </summary>
        /// <param name="msg">Stream</param>
        public void FromStream(Stream msg)
        {
            BinaryReader sr = new BinaryReader(msg);

            SerializableType tmpType;
            BinarySerializer.FromStream(out tmpType, sr); Type = tmpType;
            ID = sr.ReadInt32();
            Property = sr.ReadString();

            MaxListCount = sr.ReadInt32();
            SerializableExpression tmp;
            BinarySerializer.FromStream(out tmp, sr);
            Filter = tmp;

            OrderBy = new List<SerializableExpression>();
            bool cont = true;
            while (cont)
            {
                BinarySerializer.FromStream(out cont, sr);
                if (cont)
                {
                    BinarySerializer.FromStream(out tmp, sr);
                    OrderBy.Add(tmp);
                }
            }             
        }
    }

    /// <summary>
    /// Kist Stream Service Contract
    /// TODO: Add FaultContracts
    /// TODO: Remove GetObject
    /// TODO: Remove GetListOf
    /// </summary>
    [ServiceContract]
    public interface IKistlServiceStreams
    {
        /// <summary>
        /// Gets a single object from the datastore. This method is superseded by using GetList with a (o => o.ID == $id) filter.
        /// </summary>
        /// <param name="msg">the message should containt only a Type and an ID</param>
        /// <returns>a memory stream containing the serialized object, rewound to the beginning</returns>
        /// <exception cref="ArgumentOutOfRangeException">when the specified object was not found</exception>
        [Obsolete]
        [OperationContract]
        MemoryStream GetObject(MemoryStream msg);

        /// <summary>
        /// Puts a number of changed objects into the database. The resultant objects are sent back to the client.
        /// </summary>
        /// <param name="msg">a streamable list of <see cref="IPersistenceObject"/>s</param>
        /// <returns>a streamable list of <see cref="IPersistenceObject"/>s</returns>
        [OperationContract]
        MemoryStream SetObjects(MemoryStream msg);

        /// <summary>
        /// Returns a list of objects from the datastore, matching the specified filters.
        /// </summary>
        /// <param name="msg">a KistlServiceStreamsMessage specifying the type and filters to use for the query</param>
        /// <returns>the found objects</returns>
        [OperationContract]
        MemoryStream GetList(MemoryStream msg);

        /// <summary>
        /// returns a list of objects referenced by a specified Property. Use an equivalent query in GetList() instead.
        /// </summary>
        /// <param name="msg">a KistlServiceStreamsMessage specifying the type, object and property to use for the query</param>
        /// <returns>the referenced objects</returns>
        [Obsolete]
        [OperationContract]
        MemoryStream GetListOf(MemoryStream msg);

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

    /// <summary>
    /// Servicekontrakt für das Kistl Service
    /// </summary>
    [Obsolete]
    [ServiceContract]
    public interface IKistlService
    {
        /// <summary>
        /// Gets a List of Objects
        /// </summary>
        /// <param name="type">Type of the result Objects</param>
        /// <param name="maxListCount">Max. size of the resultset</param>
        /// <param name="filter">Filter as LinqExpression</param>
        /// <param name="orderBy">OrderBy as LinqExpression</param>
        /// <returns>A List ob Objects as XML</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetList(SerializableType type, int maxListCount, SerializableExpression filter, List<SerializableExpression> orderBy);

        /// <summary>
        /// Liste aller Objekte eines Objektes "ID" im Property "property".
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="ID">Das Basisobjekt</param>
        /// <param name="property">Die Eigenschaft, welches die Liste enthält.</param>
        /// <returns>XML</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetListOf(SerializableType type, int ID, string property);

        /// <summary>
        /// Gibt ein Objekt zurück
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="ID">ID des Objektes</param>
        /// <returns>XML</returns>
        [Obsolete]
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetObject(SerializableType type, int ID);
        
        /// <summary>
        /// Update/Insert eines Objektes. Gibt das geänderte Objekt wieder zurück.
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="xmlObj">Das zu ändernde Objekt als XML</param>
        /// <returns>XML</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string SetObject(SerializableType type, string xmlObj);

        /// <summary>
        /// Fetches a list of CollectionEntry objects of the Type <paramref name="ceType"/> which are owned by the object with the ID <paramref name="ID"/> in the role <paramref name="role"/>.
        /// </summary>
        /// <param name="ceType">the requested collection entry type</param>
        /// <param name="role">the parent role (1 == A, 2 == B)</param>
        /// <param name="ID">the ID of the parent object</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string FetchRelation(SerializableType ceType, int role, Guid ID);

        /// <summary>
        /// Instructs the Server to generate Objects &amp; Database. Throws an Exception on failures.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void Generate();

        /// <summary>
        /// Hello World.
        /// </summary>
        /// <param name="name">Ein Name</param>
        /// <returns>Gibt "Hello " + name zurück.</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string HelloWorld(string name);
    }
}
