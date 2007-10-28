using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml;

namespace Kistl.Server
{
    public class KistlService : API.IKistlService
    {
        private API.IServerObject GetServerObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");
            
            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type");

            API.IServerObject obj = Activator.CreateInstance(t) as API.IServerObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }

        private DataContext GetDataContext()
        {
            return new System.Data.Linq.DataContext("Data Source=localhost\\sqlexpress; Initial Catalog=Kistl;Integrated Security=true");
        }

        public string GetList(string type)
        {
            try
            {
                return GetServerObject(type).GetList(GetDataContext());
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        public string GetListOf(string type, int ID, string property)
        {
            try
            {
                return GetServerObject(type).GetListOf(GetDataContext(), ID, property);
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        public string GetObject(string type, int ID)
        {
            try
            {
                return GetServerObject(type).GetObject(GetDataContext(), ID);
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        public void SetObject(string type, string obj)
        {
            try
            {
                GetServerObject(type).SetObject(GetDataContext(), obj);
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

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
