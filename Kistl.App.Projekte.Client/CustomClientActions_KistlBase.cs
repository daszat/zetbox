using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Extensions;
using System.Diagnostics;
using Kistl.API.Utils;

namespace Kistl.App.Base
{
    public partial class CustomClientActions_KistlBase
    {
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
                ObjectClass objClass = obj.GetReferencedObjectClass();
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
                    Logging.Log.Warn("Should delete " + tr.FullName);
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
                        newProp.CategoryTags = prop.CategoryTags;
                        newProp.Description = prop.Description;
                        newProp.IsIndexed = prop.IsIndexed;
                        newProp.IsList = prop.IsList;
                        // put the new property into the module of the class
                        newProp.Module = objClass.Module;
                        newProp.ValueModelDescriptor = prop.ValueModelDescriptor;

                        // Copy Constrains
                        foreach (var c in prop.Constraints)
                        {
                            var newC = (Constraint)ctx.Create(c.GetInterfaceType());
                            newProp.Constraints.Add(newC);
                            newC.Reason = c.Reason;
                        }

                        // Copy Default Value
                        if (prop.DefaultValue != null)
                        {
                            var dv = prop.DefaultValue;
                            var newDV = (DefaultPropertyValue)ctx.Create(dv.GetInterfaceType());
                            newProp.DefaultValue = newDV;
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
                        // put the new method into the module of the class
                        newMeth.Module = objClass.Module;

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

        public void OnNotifyCreated_Relation(Relation obj)
        {
            obj.A = obj.Context.Create<RelationEnd>();
            obj.B = obj.Context.Create<RelationEnd>();
        }
    }
}