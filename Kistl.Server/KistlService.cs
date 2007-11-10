using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml;
using Kistl.API.Server;
using Kistl.API;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des KistServices
    /// </summary>
    public class KistlService : IKistlService
    {
        /// <summary>
        /// Helper Method for generic object access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IServerObject GetServerObject(ObjectType type)
        {
            if (type == null) throw new ArgumentException("Type is null");
            if (string.IsNullOrEmpty(type.FullNameServerObject)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type.FullNameServerObject);
            if (t == null) throw new ApplicationException("Invalid Type");

            IServerObject obj = Activator.CreateInstance(t) as IServerObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }

        /// <summary>
        /// Implementierung der GetList Methode
        /// Holt sich vom ObjektBroker das richtige Server BL Objekt & 
        /// delegiert den Aufruf weiter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetList(ObjectType type)
        {
            try
            {
                using (KistlDataContext ctx = KistlDataContext.InitSession())
                {
                    return GetServerObject(type).GetList();
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Implementierung der GetListOf Methode
        /// Holt sich vom ObjektBroker das richtige Server BL Objekt & 
        /// delegiert den Aufruf weiter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetListOf(ObjectType type, int ID, string property)
        {
            try
            {
                using (KistlDataContext ctx = KistlDataContext.InitSession())
                {
                    return GetServerObject(type).GetListOf(ID, property);
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Implementierung der GetObject Methode
        /// Holt sich vom ObjektBroker das richtige Server BL Objekt & 
        /// delegiert den Aufruf weiter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetObject(ObjectType type, int ID)
        {
            try
            {
                using (KistlDataContext ctx = KistlDataContext.InitSession())
                {
                    return GetServerObject(type).GetObject(ID);
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Implementierung der SetObject Methode
        /// Holt sich vom ObjektBroker das richtige Server BL Objekt & 
        /// delegiert den Aufruf weiter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SetObject(ObjectType type, string obj)
        {
            try
            {
                using (KistlDataContext ctx = KistlDataContext.InitSession())
                {
                    return GetServerObject(type).SetObject(obj);
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Implementierung der HelloWorld Methode
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string HelloWorld(string name)
        {
            try
            {
                return "Hello " + name;
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
