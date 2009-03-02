using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;

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
        public KistlServiceStreamsMessage(System.IO.Stream msg)
        {
            FromStream(msg);
        }

        /// <summary>
        /// Serializes the Message to a Stream.
        /// </summary>
        /// <param name="msg">Stream.</param>
        public void ToStream(System.IO.Stream msg)
        {
            System.IO.BinaryWriter sw = new System.IO.BinaryWriter(msg);

            BinarySerializer.ToStream(Type, sw);
            sw.Write(ID);
            sw.Write(Property ?? "");

            sw.Write(MaxListCount);
            BinarySerializer.ToStream(Filter, sw);
            foreach (var o in OrderBy)
            {
                BinarySerializer.ToStream(true, sw);
                BinarySerializer.ToStream(o, sw);
            }
            BinarySerializer.ToStream(false, sw);
        }

        /// <summary>
        /// Serializes the Message to a Stream.
        /// </summary>
        /// <returns>new MemoryStream</returns>
        public System.IO.MemoryStream ToStream()
        {
            System.IO.MemoryStream s = new System.IO.MemoryStream();
            ToStream(s);
            return s;
        }

        /// <summary>
        /// Deserializes a Message from the given Stream.
        /// </summary>
        /// <param name="msg">Stream</param>
        public void FromStream(System.IO.Stream msg)
        {
            System.IO.BinaryReader sr = new System.IO.BinaryReader(msg);

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
    /// </summary>
    [ServiceContract]
    public interface IKistlServiceStreams
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract]
        System.IO.MemoryStream GetObject(System.IO.MemoryStream msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract]
        System.IO.MemoryStream SetObjects(System.IO.MemoryStream msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract]
        System.IO.MemoryStream GetList(System.IO.MemoryStream msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract]
        System.IO.MemoryStream GetListOf(System.IO.MemoryStream msg);


        /// <summary>
        /// Fetches a list of CollectionEntry objects of the Relation <paramref name="relID"/> which are owned by the object with the ID <paramref name="ID"/> in the role <paramref name="role"/>.
        /// </summary>
        /// <param name="relId">the requested Relation</param>
        /// <param name="role">the parent role (1 == A, 2 == B)</param>
        /// <param name="ID">the ID of the parent object</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        System.IO.MemoryStream FetchRelation(int relId, int role, int ID);
    }

    /// <summary>
    /// Servicekontrakt für das Kistl Service
    /// </summary>
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
        string FetchRelation(SerializableType ceType, int role, int ID);

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
