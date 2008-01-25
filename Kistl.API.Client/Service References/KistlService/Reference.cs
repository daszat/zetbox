//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kistl.API.Client.KistlService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="KistlService.IKistlService")]
    public interface IKistlService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKistlService/GetList", ReplyAction="http://tempuri.org/IKistlService/GetListResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ApplicationException), Action="http://tempuri.org/IKistlService/GetListApplicationExceptionFault", Name="ApplicationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        string GetList(Kistl.API.ObjectType type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKistlService/GetListOf", ReplyAction="http://tempuri.org/IKistlService/GetListOfResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ApplicationException), Action="http://tempuri.org/IKistlService/GetListOfApplicationExceptionFault", Name="ApplicationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        string GetListOf(Kistl.API.ObjectType type, int ID, string property);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKistlService/GetObject", ReplyAction="http://tempuri.org/IKistlService/GetObjectResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ApplicationException), Action="http://tempuri.org/IKistlService/GetObjectApplicationExceptionFault", Name="ApplicationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        string GetObject(Kistl.API.ObjectType type, int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKistlService/SetObject", ReplyAction="http://tempuri.org/IKistlService/SetObjectResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ApplicationException), Action="http://tempuri.org/IKistlService/SetObjectApplicationExceptionFault", Name="ApplicationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        string SetObject(Kistl.API.ObjectType type, string obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKistlService/Generate", ReplyAction="http://tempuri.org/IKistlService/GenerateResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ApplicationException), Action="http://tempuri.org/IKistlService/GenerateApplicationExceptionFault", Name="ApplicationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        void Generate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKistlService/HelloWorld", ReplyAction="http://tempuri.org/IKistlService/HelloWorldResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ApplicationException), Action="http://tempuri.org/IKistlService/HelloWorldApplicationExceptionFault", Name="ApplicationException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        string HelloWorld(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IKistlServiceChannel : Kistl.API.Client.KistlService.IKistlService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class KistlServiceClient : System.ServiceModel.ClientBase<Kistl.API.Client.KistlService.IKistlService>, Kistl.API.Client.KistlService.IKistlService {
        
        public KistlServiceClient() {
        }
        
        public KistlServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public KistlServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public KistlServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public KistlServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetList(Kistl.API.ObjectType type) {
            return base.Channel.GetList(type);
        }
        
        public string GetListOf(Kistl.API.ObjectType type, int ID, string property) {
            return base.Channel.GetListOf(type, ID, property);
        }
        
        public string GetObject(Kistl.API.ObjectType type, int ID) {
            return base.Channel.GetObject(type, ID);
        }
        
        public string SetObject(Kistl.API.ObjectType type, string obj) {
            return base.Channel.SetObject(type, obj);
        }
        
        public void Generate() {
            base.Channel.Generate();
        }
        
        public string HelloWorld(string name) {
            return base.Channel.HelloWorld(name);
        }
    }
}
