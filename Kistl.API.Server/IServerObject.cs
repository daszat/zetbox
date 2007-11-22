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
        [System.Diagnostics.DebuggerStepThrough]
        public static IServerObject GetServerObject(ObjectType type)
        {
            if (type == null) throw new ArgumentException("Type is null");
            if (string.IsNullOrEmpty(type.FullNameServerObject)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type.FullNameServerObject);
            if (t == null) throw new ApplicationException("Invalid Type");

            IServerObject obj = Activator.CreateInstance(t) as IServerObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }
    }


    /// <summary>
    /// Basis Objekt für Server BL. Implementiert Linq to SQL.
    /// TODO: Für die Custom Actions ist das noch nicht gut genug typisiert. 
    /// Es reicht einmal für den generischen Teil.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServerObject<T> : IServerObject where T : BaseDataObject, IDataObject, new()
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

            XMLObjectCollection list = new XMLObjectCollection();
            list.Objects.AddRange(result.ToList().OfType<BaseDataObject>());
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

            // die Properties liefern eine EntityCollection<> zurück
            // diese lässt sich nicht XML serialisieren
            // daher -> umwandeln in eine List<>
            // Zuerst rausfinden, um welchen Typ es sich handelt
            // danach die List<> erzeugen & im Konstruktor die EntityCollection<> übergeben
            // -> fertig!
            // TODO: Rausfinden ob das langsam ist & wenn ja, dann Cachen!
            // Type[] innerTypes = pi.PropertyType.GetGenericArguments();
            // Type tList = typeof(List<>).MakeGenericType(innerTypes);
            // object list = Activator.CreateInstance(tList, pi.GetValue(obj, null));

            // return list.ToXmlString();

            XMLObjectCollection result = new XMLObjectCollection();
            IEnumerable v = (IEnumerable)pi.GetValue(obj, null);
            result.Objects.AddRange(v.OfType<BaseDataObject>());
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
            return result.First<T>();
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
            XMLObject result = new XMLObject();
            result.Object = (BaseDataObject)GetObjectInstance(ID);
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
            T obj = xml.FromXmlString<XMLObject>().Object as T;

            if (obj.ID != API.Helper.INVALIDID)
            {
                KistlDataContext.Current.AttachTo(obj.EntitySetName, obj);
                MarkEveryPropertyAsModified(obj);
            }
            else
            {
                KistlDataContext.Current.AddObject(obj.EntitySetName, obj);
            }
            UpdateRelationships(obj);
            KistlDataContext.Current.SubmitChanges();

            XMLObject result = new XMLObject();
            result.Object = obj;
            return result.ToXmlString();
        }

        /// <summary>
        /// Update Relationships
        /// TODO: Bad hack, weil EF keine Releasion serialisieren kann
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateRelationships(T obj)
        {
            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                if (pi.GetCustomAttributes(typeof(EdmRelationshipNavigationPropertyAttribute), true).Length > 0)
                {
                    // Bingo!
                    PropertyInfo pifk = typeof(T).GetProperty("fk_" + pi.Name);
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

        private void MarkEveryPropertyAsModified(IDataObject obj)
        {
            ObjectStateEntry stateEntry = KistlDataContext.Current.ObjectStateManager.GetObjectStateEntry(obj);
            MetadataWorkspace workspace = KistlDataContext.Current.MetadataWorkspace;
            EntityType entityType = workspace.GetItem<EntityType>("Model." + obj.GetType().Name, DataSpace.CSpace);

            foreach (EdmProperty property in entityType.Properties)
            {
                stateEntry.SetModifiedProperty(property.Name);
            }
        }
    }
}
