using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Extensions;
using System.Diagnostics;

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
            if (obj.A == null ||
                obj.B == null ||
                obj.A.Type == null ||
                obj.B.Type == null)
            {
                e.Result = "invalid Relation";
            }
            else
            {
                e.Result = String.Format("Relation: {0}({1}) : {2}({3})",
                    obj.A.RoleName,
                    obj.A.Type.ClassName,
                    obj.B.RoleName,
                    obj.B.Type.ClassName);
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

        public void OnGetPropertyTypeString_GuidProperty(Kistl.App.Base.GuidProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
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
            if (e.Result == null)
            {
                return;
            }
            if (obj.GenericArguments.Count > 0)
            {
                var args = obj.GenericArguments.Select(tRef => tRef.AsType(throwOnError)).ToArray();
                if (args.Contains(null))
                {
                    if (throwOnError)
                    {
                        throw new InvalidOperationException("Cannot create Type: missing generic argument");
                    }
                    else
                    {
                        return;
                    }
                }
                e.Result = e.Result.MakeGenericType(args);
            }
        }

        public void OnRegenerateTypeRefs_Assembly(Assembly assembly)
        {
            var ctx = assembly.Context;

            // pre-load context
            var oldTypes = ctx.GetQuery<TypeRef>()
                .Where(tr => tr.Assembly.ID == assembly.ID)
                .ToList();

            // load all current references into the context
            var newTypes = System.Reflection.Assembly
                .Load(assembly.AssemblyName)
                .GetExportedTypes()
                .Where(t => !t.IsGenericTypeDefinition)
                .Select(t => t.ToRef(ctx))
                .ToDictionary(tr => tr.ID);

            foreach (var tr in oldTypes)
            {
                var type = tr.AsType(false);
                if (type == null)
                {
                    // TODO: delete+cascade here
                    Trace.TraceWarning("Should delete " + tr.FullName);
                    ////ctx.Delete(tr);
                }
                else if (!type.IsGenericType)
                {
                    if (!newTypes.ContainsKey(tr.ID))
                    {
                        ctx.Delete(tr);
                    }
                }
            }

        }

        public void OnUpdateParent_TypeRef(TypeRef obj)
        {
            obj.Parent = obj.AsType(true).BaseType.ToRef(obj.Context);
        }

        public void OnImplementInterfaces_ObjectClass(ObjectClass objClass)
        {
            IKistlContext ctx = objClass.Context;

            foreach (var iface in objClass.ImplementsInterfaces)
            {
                foreach (var prop in iface.Properties)
                {
                    if (!objClass.Properties.Select(p => p.PropertyName).Contains(prop.PropertyName))
                    {
                        // Add Property
                        var newProp = (Property)ctx.Create(prop.GetInterfaceType());
                        objClass.Properties.Add(newProp);
                        newProp.PropertyName = prop.PropertyName;
                        newProp.AltText = prop.AltText;
                        newProp.CategoryTags = prop.CategoryTags;
                        newProp.Description = prop.Description;
                        newProp.IsIndexed = prop.IsIndexed;
                        newProp.IsList = prop.IsList;
                        newProp.IsNullable = prop.IsNullable;
                        newProp.Module = prop.Module;
                        newProp.ValueModelDescriptor = prop.ValueModelDescriptor;

                        if (prop is StringProperty)
                        {
                            ((StringProperty)newProp).Length = ((StringProperty)prop).Length;
                        }

                        // Copy Constrains
                        foreach (var c in prop.Constraints)
                        {
                            var newC = (Constraint)ctx.Create(c.GetInterfaceType());
                            newProp.Constraints.Add(newC);
                            newC.Reason = c.Reason;
                        }
                    }
                }

                foreach (Method meth in iface.Methods)
                {
                    // TODO: Wenn das sortieren von Parametern funktioniert müssen auch die Parameter
                    // der Methode geprüft werden
                    // TODO: evtl. IsDeclaredInImplementsInterface aus dem Generator verallgemeinern & benutzen
                    if (!objClass.Methods.Select(m => m.MethodName).Contains(meth.MethodName))
                    {
                        Method newMeth = (Method)ctx.Create(meth.GetInterfaceType());
                        objClass.Methods.Add(newMeth);
                        newMeth.MethodName = meth.MethodName;
                        newMeth.IsDisplayable = meth.IsDisplayable;
                        newMeth.Module = meth.Module;

                        foreach (var param in meth.Parameter)
                        {
                            var newParam = (BaseParameter)ctx.Create(param.GetInterfaceType());
                            newMeth.Parameter.Add(newParam);

                            newParam.ParameterName = param.ParameterName;
                            newParam.Description = param.Description;
                            newParam.IsList = param.IsList;
                            newParam.IsReturnParameter = param.IsReturnParameter;
                        }
                    }
                }
            }
        }

        public void OnGetCodeTemplate_MethodInvocation(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public void {0}", mi.GetMemberName());

            if (mi.InvokeOnObjectClass != null)
            {
                sb.AppendFormat("({0}.{1} obj", mi.InvokeOnObjectClass.Module != null ? mi.InvokeOnObjectClass.Module.Namespace : "", mi.InvokeOnObjectClass.ClassName);
            }

            if (mi.Method != null)
            {
                var returnParam = mi.Method.GetReturnParameter();
                if (returnParam != null)
                {
                    sb.AppendFormat(", MethodReturnEventArgs<{0}> e", returnParam.GetParameterTypeString());
                }

                foreach (var param in mi.Method.Parameter.Where(p => !p.IsReturnParameter))
                {
                    sb.AppendFormat(", {0} {1}", param.GetParameterTypeString(), param.ParameterName);
                }
            }

            sb.AppendLine(")");
            sb.AppendLine("{");
            sb.AppendLine("}");

            e.Result = sb.ToString();
        }

        public void OnGetMemberName_MethodInvocation(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("On");
            sb.Append(mi.Method != null ? mi.Method.MethodName : "");
            sb.Append("_");
            sb.Append(mi.InvokeOnObjectClass != null ? mi.InvokeOnObjectClass.ClassName : "");

            e.Result = sb.ToString();
        }
    }
}