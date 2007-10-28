using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace Kistl.API
{
    [ServiceContract]
    public interface IKistlService
    {
        [OperationContract]
        string GetList(string type);

        [OperationContract]
        string GetListOf(string type, int ID, string property);

        [OperationContract]
        string GetObject(string type, int ID);
        
        [OperationContract]
        void SetObject(string type, string obj);

        [OperationContract]
        string HelloWorld(string name);
    }
}
