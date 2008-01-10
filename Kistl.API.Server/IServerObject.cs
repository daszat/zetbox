using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using System.Data.Metadata.Edm;

namespace Kistl.API.Server
{
    /// <summary>
    /// Interface für das Server BL Objekt.
    /// </summary>
    public interface IServerObject
    {
        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        string GetList();

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        string GetListOf(int ID, string property);

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        string GetObject(int ID);

        /// <summary>
        /// Läd generisch eine Instanz
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        IDataObject GetObjectInstanceGeneric(int ID);

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        string SetObject(string xml);
    }

    public class ServerObjectFactory
    {
        /// <summary>
        /// Helper Method for generic object access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        // [System.Diagnostics.DebuggerStepThrough]
        public static IServerObject GetServerObject(ObjectType type)
        {
            if (type == null) throw new ArgumentException("Type is null");
            if (string.IsNullOrEmpty(type.FullNameServerDataObject)) throw new ArgumentException("Type is empty");

            Type objType = Type.GetType(type.FullNameServerDataObject);
            if (objType == null) throw new ApplicationException("Invalid Type");

            Type t = typeof(ServerObject<,,>);
            Type xmlCollection = Type.GetType("Kistl.API.XMLObjectCollection, Kistl.Objects.Server");
            Type xmlObj = Type.GetType("Kistl.API.XMLObject, Kistl.Objects.Server");

            Type result = t.MakeGenericType(objType, xmlCollection, xmlObj);

            IServerObject obj = Activator.CreateInstance(result) as IServerObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;

        }
    }


    /// <summary>
    /// Basis Objekt für die generische Server BL. Implementiert Linq
    /// Das ist nur für den generischen Teil gedacht, alle anderen Custom Actions
    /// können mit Linq auf den Context direkt zugreifen, da die Actions am Objekt & am Context
    /// selbst implementiert sind
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServerObject<T, XMLCOLLECTION, XMLOBJECT> : IServerObject 
        where T : BaseServerDataObject, IDataObject, new()
        where XMLCOLLECTION: IXmlObjectCollection, new()
        where XMLOBJECT : IXmlObject, new()
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public ServerObject()
        {
            _type = new ObjectType(typeof(T).Namespace, typeof(T).Name);
        }

        protected ObjectType _type = null;

        public ObjectType Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public string GetList()
        {
            var result = from a in KistlDataContext.Current.GetTable<T>()
                         select a;

            XMLCOLLECTION list = new XMLCOLLECTION();
            list.Objects.AddRange(result.ToList().OfType<object>());
            return list.ToXmlString();
        }

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetListOf(int ID, string property)
        {
            if (ID == API.Helper.INVALIDID) throw new ArgumentException("ID must not be invalid");
            T obj = GetObjectInstance(ID);
            if (obj == null) throw new ApplicationException("Object not found");

            PropertyInfo pi = typeof(T).GetProperty(property);
            if (pi == null) throw new ArgumentException("Property does not exist");

            XMLCOLLECTION result = new XMLCOLLECTION();
            IEnumerable v = (IEnumerable)pi.GetValue(obj, null);
            result.Objects.AddRange(v.OfType<object>());
            return result.ToXmlString();
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T GetObjectInstance(int ID)
        {
            var result = from a in KistlDataContext.Current.GetTable<T>()
                         where a.ID == ID
                         select a;
            return result.FirstOrDefault<T>();
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IDataObject GetObjectInstanceGeneric(int ID)
        {
            return GetObjectInstance(ID);
        }

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetObject(int ID)
        {
            XMLOBJECT result = new XMLOBJECT();
            result.Object = (BaseServerDataObject)GetObjectInstance(ID);
            return result.ToXmlString();
        }

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string SetObject(string xml)
        {
            T obj = xml.FromXmlString<XMLOBJECT>().Object as T;

            if (obj.ObjectState == DataObjectState.Deleted)
            {
                KistlDataContext.Current.AttachTo(obj.EntitySetName, obj);
                KistlDataContext.Current.DeleteObject(obj);
            }
            else
            {
                if (obj.ObjectState == DataObjectState.New)
                {
                    KistlDataContext.Current.AddObject(obj.EntitySetName, obj);
                }
                else
                {
                    KistlDataContext.Current.AttachTo(obj.EntitySetName, obj);
                    MarkEveryPropertyAsModified(obj);
                }

                UpdateRelationships(obj);
            }

            KistlDataContext.Current.SubmitChanges();

            XMLOBJECT result = new XMLOBJECT();
            result.Object = obj;
            return result.ToXmlString();
        }

        /// <summary>
        /// Update Relationships
        /// TODO: Bad hack, weil EF keine Relationen serialisieren kann
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateRelationships(T obj)
        {
            Type type = obj.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.GetCustomAttributes(typeof(EdmRelationshipNavigationPropertyAttribute), true).Length > 0)
                {
                    // Bingo!
                    PropertyInfo pifk = type.GetProperty("fk_" + pi.Name);
                    if (pifk != null)
                    {
                        int fk = (int)pifk.GetValue(obj, null);

                        IServerObject so = ServerObjectFactory.GetServerObject(new ObjectType(pi.PropertyType.FullName));
                        IDataObject other = so.GetObjectInstanceGeneric(fk);
                        pi.SetValue(obj, other, null);
                    }
                }
            }
        }

        /// <summary>
        /// TODO: Bad Hack!
        /// Es gibt angeblich jetzt eine bessere MEthode
        /// </summary>
        /// <param name="obj"></param>
        private void MarkEveryPropertyAsModified(BaseServerDataObject obj)
        {
            ObjectStateEntry stateEntry = KistlDataContext.Current.ObjectStateManager.GetObjectStateEntry(obj.EntityKey);
            MetadataWorkspace workspace = KistlDataContext.Current.MetadataWorkspace;
            EntityType entityType = workspace.GetItem<EntityType>("Model." + obj.GetType().Name, DataSpace.CSpace);

            foreach (EdmProperty property in entityType.Properties)
            {
                stateEntry.SetModifiedProperty(property.Name);
            }
        }
    }
}
