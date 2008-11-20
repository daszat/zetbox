using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Client;
using System.Xml;
using System.IO;
using Kistl.API;

namespace Kistl.App.Base
{
    public partial class CustomClientActions_KistlBase
    {
        #region ToString
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_DataType(DataType obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}", obj.GetType());
            }
            else
            {
                e.Result = obj.Module.Namespace + "." + obj.ClassName;
            }
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_Enumeration(Enumeration obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: if (!IsValid)
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}", obj.GetType());
            }
            else
            {
                e.Result = obj.ClassName;
            }
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_EnumerationEntry(EnumerationEntry obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: if (!IsValid)
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}", obj.GetType());
            }
            else
            {
                e.Result = obj.Enumeration + "." + obj.Name;
            }
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_MethodInvokation(MethodInvocation obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = (obj.Assembly.IsClientAssembly ? "[Client] " : "[Server] ")
                    + obj.InvokeOnObjectClass.ClassName + "." + obj.Method.MethodName;
            }
            else
            {
                e.Result = String.Format("MethodInvocation {0}", obj.ID);
            }
        }

        public void OnToString_BaseProperty(Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = string.Format("{0} {1}.{2}",
                   obj.GetPropertyTypeString(),
                   (obj.ObjectClass == null) ? "unattached" : obj.ObjectClass.ClassName,
                   obj.PropertyName);
            }
            else
            {
                e.Result = String.Format("BaseProperty {0}", obj.ID);
            }
        }

        public void OnToString_Property(Base.Property obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (obj.IsList) e.Result += " [0..n]";
        }

        public void OnToString_Method(Kistl.App.Base.Method obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = obj.ObjectClass.Module.Namespace + "." +
                                obj.ObjectClass.ClassName + "." + obj.MethodName;
            }
            else
            {
                e.Result = String.Format("Method {0}: {1}", obj.ID, obj.MethodName);
            }
        }

        public void OnToString_Assembly(Kistl.App.Base.Assembly obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.AssemblyName;
        }

        public void OnToString_Module(Kistl.App.Base.Module obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ModuleName;
        }

        //public void OnToString_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        //{
        //    e.Result = "* " + e.Result;
        //}

        public void OnToString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;
        }

        public void OnToString_BaseParameter(Kistl.App.Base.BaseParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0}{1} {2}",
                obj.IsReturnParameter ? "[Return] " : "",
                obj.GetParameterTypeString(),
                obj.ParameterName);
        }
        #endregion

        #region GetTypes
        public void OnGetDataType_DataType(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            // TODO: remove this bad test-hack
            string fullname = obj.GetDataTypeString();
            string assembly = fullname == "Kistl.Client.Mocks.TestObject" ? "Kistl.Client.Tests" : "Kistl.Objects";
            e.Result = Type.GetType(fullname + ", " + assembly, true);
        }

        public void OnGetDataTypeString_DataType(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = "";
            }
            else
            {
                e.Result = obj.Module.Namespace + "." + obj.ClassName;
            }
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
                // TODO: ??
                string assembly = fullname == "Kistl.Client.Mocks.TestObject" ? "Kistl.Client.Tests" : "Kistl.Objects";
                e.Result = Type.GetType(fullname + ", " + assembly, true);
            }
        }

        public void OnGetPropertyTypeString_BaseProperty(Kistl.App.Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement BaseProperty.GetPropertyTypeString()>";
        }

        public void OnGetPropertyTypeString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                ObjectClass objClass = obj.ReferenceObjectClass;
                e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
            }
            else
            {
                e.Result = String.Format("ObjectReferenceProperty {0}: {1}", obj.ID, obj.PropertyName);
            }

        }

        public void OnGetPropertyTypeString_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                DataType objClass = obj.ReferenceProperty.ObjectClass;
                e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
            }
            else
            {
                e.Result = String.Format("BackReferenceProperty {0}: {1}", obj.ID, obj.PropertyName);
            }

        }

        public void OnGetPropertyTypeString_StructProperty(Kistl.App.Base.StructProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                DataType objClass = obj.StructDefinition;
                e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
            }
            else
            {
                e.Result = String.Format("StructProperty {0}: {1}", obj.ID, obj.PropertyName);
            }

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
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.ClassName;
            }
            else
            {
                e.Result = String.Format("EnumerationProperty {0}: {1}", obj.ID, obj.PropertyName);
            }
        }

        #region OnGetParameterTypeString_*

        public void OnGetParameterType_BaseParameter(Kistl.App.Base.BaseParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            // TODO: e.Result = Type.GetType(obj.GetParameterTypeString(), true);
            e.Result = Type.GetType(obj.GetParameterTypeString());
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
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = obj.DataType.GetDataTypeString();
            }
            else
            {
                e.Result = String.Format("ObjectParameter {0}: {1}", obj.ID, obj.ParameterName);
            }
        }

        public void OnGetParameterTypeString_CLRObjectParameter(Kistl.App.Base.CLRObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}, {1}", obj.FullTypeName, obj.Assembly);
        }

        #endregion

        #endregion

        public void OnGetReturnParameter_Method(Kistl.App.Base.Method obj, Kistl.API.MethodReturnEventArgs<BaseParameter> e)
        {
            e.Result = obj.Parameter.SingleOrDefault(param => param.IsReturnParameter);
        }

        public void OnGetInheritedMethods_ObjectClass(Kistl.App.Base.ObjectClass obj, Kistl.API.MethodReturnEventArgs<IList<Method>> e)
        {
            ObjectClass baseObjectClass = obj.BaseObjectClass;
            if (baseObjectClass != null)
            {
                e.Result = baseObjectClass.GetInheritedMethods();
                baseObjectClass.Methods.ForEach<Method>(m => e.Result.Add(m));
            }
            else
            {
                e.Result = new List<Method>();
            }
        }

    }
}