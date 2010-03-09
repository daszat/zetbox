using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server;
using Kistl.App.Extensions;
using Kistl.API.Utils;
using Kistl.API;

namespace Kistl.App.Base
{
    public static class CustomServerActions_KistlBase
    {

        #region Save
        public static void OnPreSave_BaseParameter(Kistl.App.Base.BaseParameter obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.ParameterName))
            {
                throw new ArgumentException(string.Format("ParameterName {0} has some illegal chars", obj.ParameterName));
            }

            // TODO: replace with constraint
            if (obj.Method != null && obj.Method.Parameter.Count(p => p.IsReturnParameter) > 1)
            {
                throw new ArgumentException(string.Format("Method {0}.{1}.{2} has more then one Return Parameter",
                    obj.Method.ObjectClass.Module.Namespace,
                    obj.Method.ObjectClass.ClassName,
                    obj.Method.MethodName));
            }
        }

        public static void OnPreSave_Method(Kistl.App.Base.Method obj)
        {
            // TODO: replace with constraint
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.MethodName))
            {
                throw new ArgumentException(string.Format("MethodName {0} has some illegal chars", obj.MethodName));
            }
        }
        #endregion

        #region GetTypes
        public static void OnGetDataType_DataType(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetDataTypeString() + ", Kistl.Objects", true);
        }

        public static void OnGetDataTypeString_DataType(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Module.Namespace + "." + obj.ClassName;
        }

        public static void OnGetPropertyType_Property(Kistl.App.Base.Property obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
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

        public static void OnGetPropertyTypeString_Property(Kistl.App.Base.Property obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement Property.GetPropertyTypeString()>";
        }

        public static void OnGetPropertyTypeString_StringProperty(Kistl.App.Base.StringProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public static void OnGetPropertyTypeString_DoubleProperty(Kistl.App.Base.DoubleProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public static void OnGetPropertyTypeString_BoolProperty(Kistl.App.Base.BoolProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public static void OnGetPropertyTypeString_IntProperty(Kistl.App.Base.IntProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public static void OnGetPropertyTypeString_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public static void OnGetPropertyTypeString_GuidProperty(Kistl.App.Base.GuidProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Guid";
        }

        public static void OnGetPropertyTypeString_EnumerationProperty(Kistl.App.Base.EnumerationProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.ClassName;
            // e.Result = "System.Int32";
        }

        public static void OnGetPropertyTypeString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.GetReferencedObjectClass().Module.Namespace + "." + obj.GetReferencedObjectClass().ClassName;
        }

        public static void OnGetPropertyTypeString_CalculatedObjectReferenceProperty(Kistl.App.Base.CalculatedObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ReferencedClass.Module.Namespace + "." + obj.ReferencedClass.ClassName;
        }

        public static void OnGetPropertyTypeString_CompoundObjectProperty(Kistl.App.Base.CompoundObjectProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            DataType objClass = obj.CompoundObjectDefinition;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }

        // Parameter
        public static void OnGetParameterType_BaseParameter(Kistl.App.Base.BaseParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString(), true);
        }
        public static void OnGetParameterType_ObjectParameter(Kistl.App.Base.ObjectParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString() + ", Kistl.Objects", true);
        }

        public static void OnGetParameterTypeString_StringParameter(Kistl.App.Base.StringParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public static void OnGetParameterTypeString_DoubleParameter(Kistl.App.Base.DoubleParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public static void OnGetParameterTypeString_BoolParameter(Kistl.App.Base.BoolParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public static void OnGetParameterTypeString_IntParameter(Kistl.App.Base.IntParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public static void OnGetParameterTypeString_DateTimeParameter(Kistl.App.Base.DateTimeParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public static void OnGetParameterTypeString_ObjectParameter(Kistl.App.Base.ObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.DataType.Module.Namespace + "." + obj.DataType.ClassName;
        }

        public static void OnGetParameterTypeString_CLRObjectParameter(Kistl.App.Base.CLRObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Type.FullName;
        }
        #endregion

        #region Document Management
        public static void OnGetStream_Document(Kistl.App.Base.Blob obj, MethodReturnEventArgs<System.IO.Stream> e)
        {
            e.Result = obj.Context.GetStream(obj.ID);
        }
        #endregion

        //public static void OnIsValid_Constraint(Kistl.App.Base.Constraint obj, Kistl.API.MethodReturnEventArgs<bool> e, object value)
        //{
        //    // the base constraint accepts all values
        //    e.Result = true;
        //}
    }
}
