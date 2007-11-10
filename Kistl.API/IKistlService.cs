using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace Kistl.API
{
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
        string GetList(ObjectType type);

        /// <summary>
        /// Liste aller Objekte eines Objektes "ID" im Property "property".
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="ID">Das Basisobjekt</param>
        /// <param name="property">Die Eigenschaft, welches die Liste enthält.</param>
        /// <returns>XML</returns>
        [OperationContract]
        string GetListOf(ObjectType type, int ID, string property);

        /// <summary>
        /// Gibt ein Objekt zurück
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="ID">ID des Objektes</param>
        /// <returns>XML</returns>
        [OperationContract]
        string GetObject(ObjectType type, int ID);
        
        /// <summary>
        /// Update/Insert eines Objektes. Gibt das geänderte Objekt wieder zurück.
        /// </summary>
        /// <param name="type">ServerBL Typ als AssemblyQualifiedName</param>
        /// <param name="obj">Das zu ändernde Objekt als XML</param>
        /// <returns>XML</returns>
        [OperationContract]
        string SetObject(ObjectType type, string obj);

        /// <summary>
        /// Hello World.
        /// </summary>
        /// <param name="name">Ein Name</param>
        /// <returns>Gibt "Hello " + name zurück.</returns>
        [OperationContract]
        string HelloWorld(string name);
    }
}
