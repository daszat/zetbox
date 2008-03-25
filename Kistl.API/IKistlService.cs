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
    public class KistlServiceStreamsMessage
    {
        public ObjectType Type {get; set;}
        public int ID { get; set; }
        public string Property { get; set; }

        public SerializableExpression Filter { get; set; }

        public KistlServiceStreamsMessage()
        {
        }

        public KistlServiceStreamsMessage(System.IO.Stream msg)
        {
            FromStream(msg);
        }

        public void ToStream(System.IO.Stream msg)
        {
            System.IO.BinaryWriter sw = new System.IO.BinaryWriter(msg);

            sw.Write(Type.Namespace);
            sw.Write(Type.Classname);
            sw.Write(ID);
            sw.Write(Property ?? "");
            BinarySerializer.ToBinary(Filter, sw);
        }

        public System.IO.MemoryStream ToStream()
        {
            System.IO.MemoryStream s = new System.IO.MemoryStream();
            ToStream(s);
            return s;
        }

        public void FromStream(System.IO.Stream msg)
        {
            System.IO.BinaryReader sr = new System.IO.BinaryReader(msg);

            Type = new ObjectType();
            Type.Namespace = sr.ReadString();
            Type.Classname = sr.ReadString();
            ID = sr.ReadInt32();
            Property = sr.ReadString();

            SerializableExpression tmp;
            BinarySerializer.FromBinary(out tmp, sr);
            Filter = tmp;
        }
    }

    [ServiceContract]
    public interface IKistlServiceStreams
    {
        [OperationContract]
        System.IO.MemoryStream GetObject(System.IO.MemoryStream msg);

        [OperationContract]
        System.IO.MemoryStream SetObject(System.IO.MemoryStream msg);

        [OperationContract]
        System.IO.MemoryStream GetList(System.IO.MemoryStream msg);

        [OperationContract]
        System.IO.MemoryStream GetListOf(System.IO.MemoryStream msg);
    }

    /// <summary>
    /// Servicekontrakt für das Kistl Service
    /// </summary>
    [ServiceContract]
    public interface IKistlService
    {
        /// <summary>
        /// Liste aller (später parametrisierter) Objekte
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <returns>XML</returns>
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        string GetList(ObjectType type);

        /// <summary>
        /// Liste aller Objekte eines Objektes "ID" im Property "property".
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="ID">Das Basisobjekt</param>
        /// <param name="property">Die Eigenschaft, welches die Liste enthält.</param>
        /// <returns>XML</returns>
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        string GetListOf(ObjectType type, int ID, string property);

        /// <summary>
        /// Gibt ein Objekt zurück
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="ID">ID des Objektes</param>
        /// <returns>XML</returns>
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        string GetObject(ObjectType type, int ID);
        
        /// <summary>
        /// Update/Insert eines Objektes. Gibt das geänderte Objekt wieder zurück.
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="obj">Das zu ändernde Objekt als XML</param>
        /// <returns>XML</returns>
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        string SetObject(ObjectType type, string xmlObj);

        /// <summary>
        /// Generates Objects & Database. Throws a Exception if failed.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        void Generate();

        /// <summary>
        /// Hello World.
        /// </summary>
        /// <param name="name">Ein Name</param>
        /// <returns>Gibt "Hello " + name zurück.</returns>
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
        string HelloWorld(string name);
    }
}
