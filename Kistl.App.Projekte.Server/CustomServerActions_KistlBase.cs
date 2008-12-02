using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.App.Base
{
    public class CustomServerActions_KistlBase
    {
        #region Save
        public void OnPreSave_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.ClassName))
            {
                throw new ArgumentException(string.Format("ClassName {0} has some illegal chars", obj.ClassName));
            }

            // Only for BaseClasses
            if (obj.BaseObjectClass == null)
            {
                Kistl.App.Base.Module kistlModule = KistlContext.Current.GetQuery<Kistl.App.Base.Module>().First(md => md.ModuleName == "KistlBase");
                Kistl.App.Base.Method m;
                m = obj.Methods.SingleOrDefault(i => i.MethodName == "ToString" && i.Module == kistlModule);
                if (m == null)
                {
                    m = KistlContext.Current.Create<Kistl.App.Base.Method>();
                    m.MethodName = "ToString";
                    m.Module = kistlModule;
                    obj.Methods.Add(m);
                }

                m = obj.Methods.SingleOrDefault(i => i.MethodName == "PreSave" && i.Module == kistlModule);
                if (m == null)
                {
                    m = KistlContext.Current.Create<Kistl.App.Base.Method>();
                    m.MethodName = "PreSave";
                    m.Module = kistlModule;
                    obj.Methods.Add(m);
                }

                m = obj.Methods.SingleOrDefault(i => i.MethodName == "PostSave" && i.Module == kistlModule);
                if (m == null)
                {
                    m = KistlContext.Current.Create<Kistl.App.Base.Method>();
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
                throw new ArgumentException(string.Format("ParameterName {0} has some illegal chars", obj.ParameterName));
            }

            // TODO: => ValidationFramework
            if (obj.Method != null && obj.Method.Parameter.Count(p => p.IsReturnParameter) > 1)
            {
                throw new ArgumentException(string.Format("Method {0}.{1}.{2} has more then one Return Parameter", 
                    obj.Method.ObjectClass.Module.Namespace,
                    obj.Method.ObjectClass.ClassName,
                    obj.Method.MethodName));
            }
        }

        public void OnPreSave_Method(Kistl.App.Base.Method obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.MethodName))
            {
                throw new ArgumentException(string.Format("MethodName {0} has some illegal chars", obj.MethodName));
            }
        }
        #endregion

        #region GetTypes
        public void OnGetDataType_DataType(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetDataTypeString() + ", Kistl.Objects", true);
        }

        public void OnGetDataTypeString_DataType(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Module.Namespace + "." + obj.ClassName;
        }

        public void OnGetPropertyType_BaseProperty(Kistl.App.Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            string fullname = obj.GetPropertyTypeString();

            if (obj is EnumerationProperty)
            {
                e.Result = Type.GetType(fullname + ", Kistl.Objects");
            }
            // ValueTypes all use mscorlib types,
            else if (obj is ValueTypeProperty)
            {
                e.Result = Type.GetType(fullname);
            }
            else
            {
                // other properties not
                string assembly = "Kistl.Objects";
                e.Result = Type.GetType(fullname + ", " + assembly, true);
            }
        }

        public void OnGetPropertyTypeString_BaseProperty(Kistl.App.Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement BaseProperty.GetPropertyTypeString()>";
        }

        public void OnGetPropertyTypeString_StringProperty(Kistl.App.Base.StringProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public void OnGetPropertyTypeString_DoubleProperty(Kistl.App.Base.DoubleProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public void OnGetPropertyTypeString_BoolProperty(Kistl.App.Base.BoolProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public void OnGetPropertyTypeString_IntProperty(Kistl.App.Base.IntProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public void OnGetPropertyTypeString_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public void OnGetPropertyTypeString_EnumerationProperty(Kistl.App.Base.EnumerationProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.ClassName;
            // e.Result = "System.Int32";
        }

        public void OnGetPropertyTypeString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ReferenceObjectClass.Module.Namespace + "." + obj.ReferenceObjectClass.ClassName;
        }

        //public void OnGetPropertyTypeString_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        //{
        //    e.Result = obj.ReferenceProperty.ObjectClass.Module.Namespace + "." + obj.ReferenceProperty.ObjectClass.ClassName;
        //}

        public void OnGetPropertyTypeString_StructProperty(Kistl.App.Base.StructProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            DataType objClass = obj.StructDefinition;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }

        // Parameter
        public void OnGetParameterType_BaseParameter(Kistl.App.Base.BaseParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString(), true);
        }
        public void OnGetParameterType_ObjectParameter(Kistl.App.Base.ObjectParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString() + ", Kistl.Objects", true);
        }

        public void OnGetParameterTypeString_StringParameter(Kistl.App.Base.StringParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public void OnGetParameterTypeString_DoubleParameter(Kistl.App.Base.DoubleParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public void OnGetParameterTypeString_BoolParameter(Kistl.App.Base.BoolParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public void OnGetParameterTypeString_IntParameter(Kistl.App.Base.IntParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public void OnGetParameterTypeString_DateTimeParameter(Kistl.App.Base.DateTimeParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public void OnGetParameterTypeString_ObjectParameter(Kistl.App.Base.ObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.DataType.Module.Namespace + "." + obj.DataType.ClassName;
        }

        public void OnGetParameterTypeString_CLRObjectParameter(Kistl.App.Base.CLRObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.FullTypeName;
        }
        #endregion

        //public void OnIsValid_Constraint(Kistl.App.Base.Constraint obj, Kistl.API.MethodReturnEventArgs<bool> e, object value)
        //{
        //    // the base constraint accepts all values
        //    e.Result = true;
        //}
    }
}
