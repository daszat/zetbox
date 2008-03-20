using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.App.Base
{
    public class CustomServerActions_KistlBase
    {
        public void OnPreSave_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.ClassName))
            {
                throw new ApplicationException(string.Format("ClassName {0} has some illegal chars", obj.ClassName));
            }

            // Only for BaseClasses
            if (obj.BaseObjectClass == null)
            {
                Kistl.App.Base.Module kistlModule = KistlDataContext.Current.GetTable<Kistl.App.Base.Module>().First(md => md.ModuleName == "KistlBase");
                Kistl.App.Base.Method m;
                m = obj.Methods.SingleOrDefault(i => i.MethodName == "ToString" && i.Module == kistlModule);
                if (m == null)
                {
                    m = new Kistl.App.Base.Method();
                    m.MethodName = "ToString";
                    m.Module = kistlModule;
                    obj.Methods.Add(m);
                }

                m = obj.Methods.SingleOrDefault(i => i.MethodName == "PreSave" && i.Module == kistlModule);
                if (m == null)
                {
                    m = new Kistl.App.Base.Method();
                    m.MethodName = "PreSave";
                    m.Module = kistlModule;
                    obj.Methods.Add(m);
                }

                m = obj.Methods.SingleOrDefault(i => i.MethodName == "PostSave" && i.Module == kistlModule);
                if (m == null)
                {
                    m = new Kistl.App.Base.Method();
                    m.MethodName = "PostSave";
                    m.Module = kistlModule;
                    obj.Methods.Add(m);
                }
            }
        }

        public void OnPreSave_BaseParameter(Kistl.App.Base.BaseParameter obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.ParameterName))
            {
                throw new ApplicationException(string.Format("ParameterName {0} has some illegal chars", obj.ParameterName));
            }

            if (obj.Method.Parameter.Count(p => p.IsReturnParameter) > 1)
            {
                throw new ApplicationException(string.Format("Method {0}.{1}.{2} has more then one Return Parameter", 
                    obj.Method.ObjectClass.Module.Namespace,
                    obj.Method.ObjectClass.ClassName,
                    obj.Method.MethodName));
            }
        }

        public void OnPreSave_Method(Kistl.App.Base.Method obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.MethodName))
            {
                throw new ApplicationException(string.Format("MethodName {0} has some illegal chars", obj.MethodName));
            }
        }

        #region GetDataType
        public void OnGetDataType_BaseProperty(Kistl.App.Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement BaseProperty.GetDataType()>";
        }

        public void OnGetDataType_StringProperty(Kistl.App.Base.StringProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public void OnGetDataType_DoubleProperty(Kistl.App.Base.DoubleProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public void OnGetDataType_BoolProperty(Kistl.App.Base.BoolProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public void OnGetDataType_IntProperty(Kistl.App.Base.IntProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public void OnGetDataType_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public void OnGetDataType_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ReferenceObjectClass.Module.Namespace + "." + obj.ReferenceObjectClass.ClassName;
        }

        public void OnGetDataType_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ReferenceProperty.ObjectClass.Module.Namespace + "." + obj.ReferenceProperty.ObjectClass.ClassName;
        }

        // Parameter
        public void OnGetDataType_StringParameter(Kistl.App.Base.StringParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public void OnGetDataType_DoubleParameter(Kistl.App.Base.DoubleParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public void OnGetDataType_BoolParameter(Kistl.App.Base.BoolParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public void OnGetDataType_IntParameter(Kistl.App.Base.IntParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public void OnGetDataType_DateTimeParameter(Kistl.App.Base.DateTimeParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public void OnGetDataType_ObjectParameter(Kistl.App.Base.ObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.DataType.Module.Namespace + "." + obj.DataType.ClassName;
        }

        public void OnGetDataType_CLRObjectParameter(Kistl.App.Base.CLRObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.FullTypeName;
        }
        #endregion
    }
}
