using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.API.Client;

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
                e.Result = String.Format("{0}.{1}",
                    obj.Module == null ? "[no module]" : obj.Module.Namespace,
                    obj.ClassName);
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
                e.Result = String.Format("{0} {1}.{2}",
                    obj.Implementor.Assembly.IsClientAssembly ? "[Client]" : "[Server]",
                    obj.InvokeOnObjectClass == null ? "unattached" : obj.InvokeOnObjectClass.ClassName,
                    obj.Method == null ? "unattached" : obj.Method.MethodName);
            }
            else
            {
                e.Result = String.Format("MethodInvocation {0}", obj.ID);
            }
        }

        public void OnToString_Property(Base.Property obj, Kistl.API.MethodReturnEventArgs<string> e)
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
                e.Result = String.Format("Property {0}", obj.ID);
            }

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

        public void OnToString_Relation(Relation obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            try
            {
                e.Result = String.Format("Relation: {0}({1}) : {2}({3})",
                    obj.A.RoleName,
                    obj.A.Type.ClassName,
                    obj.B.RoleName,
                    obj.B.Type.ClassName);
            }
            catch (NullReferenceException)
            {
                e.Result = "invalid Relation";
            }
        }

        public void OnToString_RelationEnd(RelationEnd obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            try
            {
                e.Result = String.Format("RelationEnd {0}({1})",
                    obj.RoleName,
                    obj.Type.ClassName);
            }
            catch (NullReferenceException)
            {
                e.Result = "invalid RelationEnd";
            }
        }

        public void OnToString_TypeRef(Kistl.App.Base.TypeRef obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}{1}, {2}",
                obj.FullName,
                "<???>" /* Currently this goes to the Database once per TypeRef
                       * TODO: re-enable that, when GenericArguments is loaded eagerly. 
                       * (obj.GenericArguments.Count > 0
                    ? "<" + String.Join(", ", obj.GenericArguments.Select(tr => tr.ToString()).ToArray()) + ">"
                    : "") */
                            ,
                obj.Assembly);
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

        public void OnGetPropertyType_Property(Kistl.App.Base.Property obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
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

        public void OnGetPropertyTypeString_Property(Kistl.App.Base.Property obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement Property.GetPropertyTypeString()>";
        }

        public void OnGetPropertyTypeString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                ObjectClass objClass = obj.ReferenceObjectClass;
                if (objClass == null)
                {
                    e.Result = "(null)";
                }
                else
                {
                    e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
                }
            }
            else
            {
                e.Result = String.Format("ObjectReferenceProperty {0}: {1}", obj.ID, obj.PropertyName);
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

        public void OnAsType_TypeRef(TypeRef obj, Kistl.API.MethodReturnEventArgs<Type> e, bool throwOnError)
        {
            e.Result = Type.GetType(String.Format("{0}, {1}", obj.FullName, obj.Assembly.AssemblyName), throwOnError);
            if (obj.GenericArguments.Count > 0)
            {
                e.Result = e.Result.MakeGenericType(obj.GenericArguments.Select(tRef => tRef.AsType(throwOnError)).ToArray());
            }
        }

        public void OnRegenerateTypeRefs_Assembly(Assembly assembly)
        {
            var ctx = assembly.Context;

            // the clr assembly descriptor from System.Reflection
            var runtimeAssembly = System.Reflection.Assembly.Load(assembly.AssemblyName);

            // a lookup of all TypeRefs currently in the data store
            var refs = ctx.GetQuery<TypeRef>().Where(tr => tr.Assembly.ID == assembly.ID).ToLookup(tr => tr.FullName);

            foreach (var t in runtimeAssembly.GetExportedTypes())
            {
                // lookup the ref to the generic tpye definition
                // TODO: there should only be one ref without generic args
                TypeRef current = refs[t.FullName].FirstOrDefault(tr => tr.GenericArguments.Count == 0);

                if (current == null)
                {
                    // create a new one
                    current = ctx.Create<TypeRef>();
                    current.FullName = t.FullName;
                    current.Assembly = assembly;
                }
            }

        }

    }
}