using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml;
using Kistl.API.Server;
using Kistl.API;
using System.Diagnostics;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des KistlServices
    /// </summary>
    public class KistlService : IKistlService
    {
        /// <summary>
        /// Implementierung der GetList Methode
        /// Holt sich vom ObjektBroker das richtige Server BL Objekt & 
        /// delegiert den Aufruf weiter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetList(ObjectType type)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
            {
                try
                {
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        return ServerObjectFactory.GetServerObject(type).GetList();
                    }
                }
                catch (Exception ex)
                {
                    Helper.HandleError(ex);
                    throw new Exception(ex.Message, ex);
                }
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
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}].{2}", type, ID, property))
            {
                try
                {
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        return ServerObjectFactory.GetServerObject(type).GetListOf(ID, property);
                    }
                }
                catch (Exception ex)
                {
                    Helper.HandleError(ex);
                    throw new Exception(ex.Message, ex);
                }
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
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}]", type, ID))
            {
                try
                {
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        return ServerObjectFactory.GetServerObject(type).GetObject(ID);
                    }
                }
                catch (Exception ex)
                {
                    Helper.HandleError(ex);
                    throw new Exception(ex.Message, ex);
                }
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
            using (TraceClient.TraceHelper.TraceMethodCall("{0}", type))
            {
                try
                {
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        return ServerObjectFactory.GetServerObject(type).SetObject(obj);
                    }
                }
                catch (Exception ex)
                {
                    Helper.HandleError(ex);
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Implements the Generate Method
        /// </summary>
        public void Generate()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Generators.Generator.GenerateCode();
                Generators.Generator.GenerateDatabase();
            }
        }

        /// <summary>
        /// Implementierung der HelloWorld Methode
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string HelloWorld(string name)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(name))
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
}
