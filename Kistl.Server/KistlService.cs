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
using System.ServiceModel.Dispatcher;
using System.ServiceModel;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des KistlServices
    /// Error are handled by the KistlServiceErrorHandler
    /// </summary>
    public class KistlService : IKistlService
    {
        #region XmlSerializer
        private interface IXmlSerializer
        {
            string XmlFromList(IEnumerable lst);
            string XmlFromObject(IDataObject obj);
            IDataObject ObjectFromXml(string xml);
        }

        private class XmlSerializer<XMLCOLLECTION, XMLOBJECT> : IXmlSerializer
            where XMLCOLLECTION : IXmlObjectCollection, new()
            where XMLOBJECT : IXmlObject, new()
        {
            public string XmlFromList(IEnumerable lst)
            {
                XMLCOLLECTION list = new XMLCOLLECTION();
                list.Objects.AddRange(lst.OfType<object>());
                return list.ToXmlString();
            }

            public string XmlFromObject(IDataObject obj)
            {
                XMLOBJECT result = new XMLOBJECT();
                result.Object = obj;
                return result.ToXmlString();
            }

            public IDataObject ObjectFromXml(string xml)
            {
                return xml.FromXmlString<XMLOBJECT>().Object as IDataObject;
            }
        }

        private static IXmlSerializer _CurrentSerializer = null;
        private static IXmlSerializer CurrentSerializer
        {
            get
            {
                if (_CurrentSerializer == null)
                {
                    Type t = typeof(XmlSerializer<,>);
                    Type xmlCollection = Type.GetType("Kistl.API.XMLObjectCollection, Kistl.Objects.Server");
                    Type xmlObj = Type.GetType("Kistl.API.XMLObject, Kistl.Objects.Server");

                    Type result = t.MakeGenericType(xmlCollection, xmlObj);

                    _CurrentSerializer = Activator.CreateInstance(result) as IXmlSerializer;
                    if (_CurrentSerializer == null) throw new InvalidOperationException("Cannot create instance of KistlService.XmlSerializer");
                }

                return _CurrentSerializer;
            }
        }
        #endregion

        /// <summary>
        /// Implementierung der GetList Methode
        /// Holt sich vom ObjektBroker das richtige Server BL Objekt & 
        /// delegiert den Aufruf weiter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetList(SerializableType type, int maxListCount, SerializableExpression filter, SerializableExpression orderBy)
        {
            try
            {
                using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
                {
                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(type.GetSystemType())
                            .GetList(maxListCount,
                            filter != null ? SerializableExpression.ToExpression(filter) : null,
                            orderBy != null ? SerializableExpression.ToExpression(orderBy) : null);
                        return CurrentSerializer.XmlFromList(lst);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
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
        public string GetListOf(SerializableType type, int ID, string property)
        {
            try
            {
                if (type == null) throw new ArgumentNullException("type");
                if (ID <= API.Helper.INVALIDID) throw new ArgumentOutOfRangeException("ID");
                if (string.IsNullOrEmpty(property)) throw new ArgumentNullException("property");

                using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}].{2}", type, ID, property))
                {
                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(type.GetSystemType()).GetListOf(ID, property);
                        return CurrentSerializer.XmlFromList(lst);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
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
        public string GetObject(SerializableType type, int ID)
        {
            try
            {
                if (type == null) throw new ArgumentNullException("type");
                if (ID <= API.Helper.INVALIDID) throw new ArgumentOutOfRangeException("ID");

                using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}]", type, ID))
                {
                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        IDataObject obj = ServerObjectHandlerFactory.GetServerObjectHandler(type.GetSystemType()).GetObject(ID);
                        return CurrentSerializer.XmlFromObject(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
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
        public string SetObject(SerializableType type, string xmlObj)
        {
            try
            {
                if (type == null) throw new ArgumentNullException("type");
                if (string.IsNullOrEmpty(xmlObj)) throw new ArgumentNullException("xmlObj");

                using (TraceClient.TraceHelper.TraceMethodCall("{0}", type))
                {
                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        IDataObject obj = CurrentSerializer.ObjectFromXml(xmlObj);
                        throw new NotImplementedException();
                        //ServerObjectHandlerFactory.GetServerObjectHandler(type.GetSystemType()).SetObjects(obj);
                        // return CurrentSerializer.XmlFromObject(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
            }
        }

        /// <summary>
        /// Implements the Generate Method
        /// </summary>
        public void Generate()
        {
            try
            {
                using (TraceClient.TraceHelper.TraceMethodCall())
                {
                    Generators.Generator.GenerateCode();
                    Generators.Generator.GenerateDatabase();
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
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
                using (TraceClient.TraceHelper.TraceMethodCall(name))
                {
                    return "Hello " + name;
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
            }
        }
    }
}
