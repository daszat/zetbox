using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des KistServices
    /// </summary>
    public class KistlService : API.IKistlService
    {
        /// <summary>
        /// Helper Method for generic object access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static API.IServerObject GetServerObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type");

            API.IServerObject obj = Activator.CreateInstance(t) as API.IServerObject;
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
        public string GetList(string type)
        {
            try
            {
                return GetServerObject(type).GetList(Helper.GetDataContext());
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
        public string GetListOf(string type, int ID, string property)
        {
            try
            {
                return GetServerObject(type).GetListOf(Helper.GetDataContext(), ID, property);
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
        public string GetObject(string type, int ID)
        {
            try
            {
                return GetServerObject(type).GetObject(Helper.GetDataContext(), ID);
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
        public string SetObject(string type, string obj)
        {
            try
            {
                return GetServerObject(type).SetObject(Helper.GetDataContext(), obj);
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
