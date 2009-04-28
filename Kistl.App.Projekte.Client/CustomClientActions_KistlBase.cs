using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Extensions;

namespace Kistl.App.Base
{
    public partial class CustomClientActions_KistlBase
    {
        #region ToString

        public void OnToString_Assembly(Assembly obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.AssemblyName;
        }

        public void OnToString_BaseParameter(BaseParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0}{1} {2}",
                obj.IsReturnParameter ? "[Return] " : "",
                obj.GetParameterTypeString(),
                obj.ParameterName);
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_DataType(DataType obj, MethodReturnEventArgs<string> e)
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
        public void OnToString_Enumeration(Enumeration obj, MethodReturnEventArgs<string> e)
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
        public void OnToString_EnumerationEntry(EnumerationEntry obj, MethodReturnEventArgs<string> e)
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
        public void OnToString_MethodInvokation(MethodInvocation obj, MethodReturnEventArgs<string> e)
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

        public void OnToString_Method(Method obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (obj.ObjectClass != null && obj.Module != null)
            {
                e.Result = obj.ObjectClass.Module.Namespace + "." +
                                obj.ObjectClass.ClassName + "." + obj.MethodName;
            }
            else
            {
                e.Result = String.Format("Method {0}: {1}", obj.ID, obj.MethodName);
            }
        }

        public void OnToString_Module(Module obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ModuleName;
        }

        public void OnToString_ObjectReferenceProperty(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;
        }

        public void OnToString_Property(Property obj, MethodReturnEventArgs<string> e)
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

        public void OnToString_Relation(Relation obj, MethodReturnEventArgs<string> e)
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

        public void OnToString_RelationEnd(RelationEnd obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("RelationEnd {0}({1})",
                obj.RoleName,
                obj.Type == null ? "no type" : obj.Type.ClassName);
        }

        public void OnToString_TypeRef(TypeRef obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}{1}, {2}",
                obj.FullName,
                //"<???>"
                /* 
                 * Currently this goes to the Database once per TypeRef
                 * TODO: re-enable that, when GenericArguments is loaded eagerly. 
                 */
                (obj.GenericArguments.Count > 0
                    ? "<" + String.Join(", ", obj.GenericArguments.Select(tr => tr.ToString()).ToArray()) + ">"
                    : "")
                            ,
                obj.Assembly);
        }

        #endregion

        public void OnGetDataType_DataType(DataType obj, MethodReturnEventArgs<System.Type> e)
        {
            // TODO: remove this bad test-hack
            string fullname = obj.GetDataTypeString();
            string assembly = fullname == "Kistl.Client.Mocks.TestObject" ? "Kistl.Client.Tests" : "Kistl.Objects";
            e.Result = Type.GetType(fullname + ", " + assembly, true);
        }

        public void OnGetDataTypeString_DataType(DataType obj, MethodReturnEventArgs<string> e)
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

        #region OnGetPropertyType*

        public void OnGetPropertyType_Property(Property obj, MethodReturnEventArgs<System.Type> e)
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

        public void OnGetPropertyTypeString_Property(Property obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement Property.GetPropertyTypeString()>";
        }

        public void OnGetPropertyTypeString_ObjectReferenceProperty(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
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

        public void OnGetPropertyTypeString_StructProperty(StructProperty obj, MethodReturnEventArgs<string> e)
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

        public void OnGetPropertyTypeString_StringProperty(StringProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public void OnGetPropertyTypeString_DoubleProperty(DoubleProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public void OnGetPropertyTypeString_BoolProperty(BoolProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public void OnGetPropertyTypeString_IntProperty(IntProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public void OnGetPropertyTypeString_DateTimeProperty(DateTimeProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public void OnGetPropertyTypeString_GuidProperty(Kistl.App.Base.DateTimeProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Guid";
        }

        public void OnGetPropertyTypeString_EnumerationProperty(EnumerationProperty obj, MethodReturnEventArgs<string> e)
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

        #endregion

        #region OnGetParameterType*

        public void OnGetParameterType_BaseParameter(BaseParameter obj, MethodReturnEventArgs<System.Type> e)
        {
            // TODO: e.Result = Type.GetType(obj.GetParameterTypeString(), true);
            e.Result = Type.GetType(obj.GetParameterTypeString());
        }

        public void OnGetParameterType_ObjectParameter(ObjectParameter obj, MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString() + ", Kistl.Objects", true);
        }

        public void OnGetParameterTypeString_StringParameter(StringParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public void OnGetParameterTypeString_DoubleParameter(DoubleParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public void OnGetParameterTypeString_BoolParameter(BoolParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public void OnGetParameterTypeString_IntParameter(IntParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public void OnGetParameterTypeString_DateTimeParameter(DateTimeParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public void OnGetParameterTypeString_ObjectParameter(ObjectParameter obj, MethodReturnEventArgs<string> e)
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

        public void OnGetParameterTypeString_CLRObjectParameter(CLRObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}, {1}", obj.FullTypeName, obj.Assembly);
        }

        #endregion

        public void OnGetReturnParameter_Method(Method obj, MethodReturnEventArgs<BaseParameter> e)
        {
            e.Result = obj.Parameter.SingleOrDefault(param => param.IsReturnParameter);
        }

        public void OnGetInheritedMethods_ObjectClass(ObjectClass obj, MethodReturnEventArgs<IList<Method>> e)
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

        public void OnAsType_TypeRef(TypeRef obj, MethodReturnEventArgs<Type> e, bool throwOnError)
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

            // pre-load context
            var oldTypes = ctx.GetQuery<TypeRef>().Where(tr => tr.Assembly.ID == assembly.ID);

            // load all current references into the context
            var newTypes = System.Reflection.Assembly.Load(assembly.AssemblyName).GetExportedTypes().Select(t => t.ToRef(ctx)).ToDictionary(tr => tr.ID);

            foreach (var tr in oldTypes)
            {
                if (!newTypes.ContainsKey(tr.ID))
                {
                    ctx.Delete(tr);
                }
            }

        }

    }
}