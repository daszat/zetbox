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
        private DataContext GetDataContext()
        {
            return new System.Data.Linq.DataContext("Data Source=localhost\\sqlexpress; Initial Catalog=Kistl;Integrated Security=true");
        }

        public string GetList(string type)
        {
            try
            {
                return ObjectBroker.GetServerObject(type).GetList(GetDataContext());
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
                return ObjectBroker.GetServerObject(type).GetListOf(GetDataContext(), ID, property);
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
                return ObjectBroker.GetServerObject(type).GetObject(GetDataContext(), ID);
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
                throw new Exception(ex.Message, ex);
            }
        }

        public string SetObject(string type, string obj)
        {
            try
            {
                return ObjectBroker.GetServerObject(type).SetObject(GetDataContext(), obj);
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
