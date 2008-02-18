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
    internal interface IServerObjectHandler
    {
        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        IEnumerable GetList();

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        IEnumerable GetListOf(int ID, string property);

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        BaseServerDataObject GetObject(int ID);

        /// <summary>
        /// Läd generisch eine Instanz
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // BaseServerDataObject GetObjectInstanceGeneric(int ID);

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        BaseServerDataObject SetObject(BaseServerDataObject xml);
    }

    /// <summary>
    /// TODO: Objekte verwaltuen & Serialisierung trennen!!!
    /// </summary>
    internal class ServerObjectHandlerFactory
    {
        /// <summary>
        /// Helper Method for generic object access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        // [System.Diagnostics.DebuggerStepThrough]
        public static IServerObjectHandler GetServerObjectHandler(ObjectType type)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
            {
                if (type == null) throw new ArgumentException("Type is null");
                if (string.IsNullOrEmpty(type.FullNameServerDataObject)) throw new ArgumentException("Type is empty");

                Type objType = Type.GetType(type.FullNameServerDataObject);
                if (objType == null) throw new ApplicationException("Invalid Type");

                Type t = typeof(ServerObjectHandler<>);
                Type result = t.MakeGenericType(objType);

                IServerObjectHandler obj = Activator.CreateInstance(result) as IServerObjectHandler;
                if (obj == null) throw new ApplicationException("Cannot create instance");

                return obj;
            }
        }
    }

    /// <summary>
    /// Basis Objekt für die generische Server BL. Implementiert Linq
    /// Das ist nur für den generischen Teil gedacht, alle anderen Custom Actions
    /// können mit Linq auf den Context direkt zugreifen, da die Actions am Objekt & am Context
    /// selbst implementiert sind
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ServerObjectHandler<T> : IServerObjectHandler 
        where T : BaseServerDataObject, IDataObject, new()
    {
        /// <summary>
        /// Events registrieren
        /// </summary>
        public ServerObjectHandler()
        {
        }

        /// <summary>
        /// Implementiert den GetList Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IEnumerable GetList()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {                
                var result = from a in KistlDataContext.Current.GetTable<T>()
                             select a;

                // XMLCOLLECTION list = new XMLCOLLECTION();
                // list.Objects.AddRange(result.ToList().OfType<object>());
                // return list.ToXmlString();
                return result;
            }
        }

        /// <summary>
        /// Implementiert den GetListOf Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable GetListOf(int ID, string property)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(string.Format("ID = {0}, Property = {1}", ID, property)))
            {
                if (ID == API.Helper.INVALIDID) throw new ArgumentException("ID must not be invalid");
                T obj = GetObjectInstance(ID);
                if (obj == null) throw new ApplicationException("Object not found");

                PropertyInfo pi = typeof(T).GetProperty(property);
                if (pi == null) throw new ArgumentException("Property does not exist");

                IEnumerable v = (IEnumerable)pi.GetValue(obj, null);
                /* XMLCOLLECTION result = new XMLCOLLECTION();
                result.Objects.AddRange(v.OfType<object>());
                return result.ToXmlString();*/
                return v;
            }
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private static T GetObjectInstance(int ID)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(string.Format("ID = {0}", ID)))
            {
                var result = from a in KistlDataContext.Current.GetTable<T>()
                             where a.ID == ID
                             select a;
                return result.FirstOrDefault<T>();
            }
        }

        /// <summary>
        /// Gibt eine typisierte Objektinstanz zurück.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        /*public BaseServerDataObject GetObjectInstanceGeneric(int ID)
        {
            return GetObjectInstance(ID);
        }*/

        /// <summary>
        /// Implementiert den GetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BaseServerDataObject GetObject(int ID)
        {
            //XMLOBJECT result = new XMLOBJECT();
            //result.Object = (BaseServerDataObject)GetObjectInstance(ID);
            //return result.ToXmlString();
            return GetObjectInstance(ID);
        }

        /// <summary>
        /// Implementiert den SetObject Befehl.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public BaseServerDataObject SetObject(BaseServerDataObject obj)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                if (obj.ObjectState == DataObjectState.Deleted)
                {
                    //KistlDataContext.Current.AttachTo(obj.EntitySetName, obj);
                    KistlDataContext.Current.DeleteObject(obj);
                }
                else
                {
                    if (obj.ObjectState == DataObjectState.New)
                    {
                        //KistlDataContext.Current.AddObject(obj.EntitySetName, obj);
                    }
                    else
                    {
                        //KistlDataContext.Current.AttachTo(obj.EntitySetName, obj);
                        MarkEveryPropertyAsModified(obj);
                    }

                    UpdateRelationships(obj as T);
                }

                KistlDataContext.Current.SubmitChanges();

                return obj;
            }
        }

        /// <summary>
        /// Update Relationships
        /// TODO: Bad hack, weil EF keine Relationen serialisieren kann
        /// </summary>
        /// <param name="obj"></param>
        private static void UpdateRelationships(T obj)
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

                        IServerObjectHandler so = ServerObjectHandlerFactory.GetServerObjectHandler(new ObjectType(pi.PropertyType.FullName));
                        IDataObject other = so.GetObject(fk);
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
        private static void MarkEveryPropertyAsModified(BaseServerDataObject obj)
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
