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
using Kistl.App.GUI;
using Kistl.Client;
using Kistl.Client.Presentables;

namespace Kistl.App.Base
{
    public partial class CustomClientActions_KistlBase
    {
        private static IViewModelFactory _mdlFactory = null;

        public CustomClientActions_KistlBase(IViewModelFactory mdlFactory)
        {
            _mdlFactory = mdlFactory;
        }

        public static void OnCreated_ObjectClass(ObjectClass obj)
        {
            obj.DefaultViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_DataObjectViewModel);
        }

        public static void OnGetDataType_DataType(DataType obj, MethodReturnEventArgs<System.Type> e)
        {
            // TODO: remove this bad test-hack
            string fullname = obj.GetDataTypeString();
            string assembly = fullname == "Kistl.Client.Mocks.TestObject" ? "Kistl.Client.Tests" : Kistl.API.Helper.InterfaceAssembly;
            e.Result = Type.GetType(fullname + ", " + assembly, true);
        }

        public static void OnGetDataTypeString_DataType(DataType obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Module == null)
            {
                e.Result = obj.Name;
            }
            else
            {
                e.Result = obj.Module.Namespace + "." + obj.Name;
            }
        }

        #region OnGetPropertyType*

        public static void OnGetPropertyType_Property(Property obj, MethodReturnEventArgs<System.Type> e)
        {
            string fullname = obj.GetPropertyTypeString();

            if (obj is EnumerationProperty)
            {
                e.Result = Type.GetType(fullname + ", " + Kistl.API.Helper.InterfaceAssembly);
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
                string assembly = fullname == "Kistl.Client.Mocks.TestObject" ? "Kistl.Client.Tests" : Kistl.API.Helper.InterfaceAssembly;
                e.Result = Type.GetType(fullname + ", " + assembly, true);
            }
        }

        public static void OnGetPropertyTypeString_Property(Property obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement Property.GetPropertyTypeString()>";
        }

        public static void OnGetPropertyTypeString_ObjectReferenceProperty(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.GetReferencedObjectClass();
            if (objClass == null)
            {
                e.Result = "a class";
            }
            else
            {
                e.Result = objClass.Module.Namespace + "." + objClass.Name;
            }
        }

        public static void OnGetPropertyTypeString_CompoundObjectProperty(CompoundObjectProperty obj, MethodReturnEventArgs<string> e)
        {
            DataType objClass = obj.CompoundObjectDefinition;
            if (objClass != null && objClass.Module != null)
            {
                e.Result = objClass.Module.Namespace + "." + objClass.Name;
            }
            else
            {
                e.Result = "a CompoundObject";
            }
        }

        public static void OnGetPropertyTypeString_StringProperty(StringProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public static void OnGetPropertyTypeString_DoubleProperty(DoubleProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public static void OnGetPropertyTypeString_BoolProperty(BoolProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public static void OnGetPropertyTypeString_IntProperty(IntProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public static void OnGetPropertyTypeString_DecimalProperty(DecimalProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Decimal";
        }

        public static void OnGetPropertyTypeString_DateTimeProperty(DateTimeProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public static void OnGetPropertyTypeString_GuidProperty(Kistl.App.Base.GuidProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Guid";
        }

        public static void OnGetPropertyTypeString_EnumerationProperty(EnumerationProperty obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.Name;
            }
            else
            {
                e.Result = String.Format("{0}: {1}", obj.ID, obj.Name);
            }
        }

        public static void OnGetPropertyTypeString_CalculatedObjectReferenceProperty(CalculatedObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferencedClass;
            if (objClass == null)
            {
                e.Result = "a class";
            }
            else
            {
                e.Result = objClass.Module.Namespace + "." + objClass.Name;
            }
        }

        #endregion

        #region OnGetParameterType*

        public static void OnGetParameterType_BaseParameter(BaseParameter obj, MethodReturnEventArgs<System.Type> e)
        {
            // TODO: e.Result = Type.GetType(obj.GetParameterTypeString(), true);
            e.Result = Type.GetType(obj.GetParameterTypeString());
        }

        public static void OnGetParameterType_ObjectParameter(ObjectParameter obj, MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString() + ", " + Kistl.API.Helper.InterfaceAssembly, true);
        }

        public static void OnGetParameterTypeString_StringParameter(StringParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public static void OnGetParameterTypeString_DoubleParameter(DoubleParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public static void OnGetParameterTypeString_BoolParameter(BoolParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public static void OnGetParameterTypeString_IntParameter(IntParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public static void OnGetParameterTypeString_DateTimeParameter(DateTimeParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        public static void OnGetParameterTypeString_ObjectParameter(ObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = obj.DataType.GetDataTypeString();
            }
            else
            {
                e.Result = String.Format("ObjectParameter {0}: {1}", obj.ID, obj.Name);
            }
        }

        public static void OnGetParameterTypeString_CLRObjectParameter(CLRObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Type == null)
            {
                e.Result = string.Empty;
            }
            else if (obj.Type.Assembly == null)
            {
                e.Result = String.Format("{0}", obj.Type.FullName);
            }
            else
            {
                e.Result = String.Format("{0}, {1}", obj.Type.FullName, obj.Type.Assembly.Name);
            }
        }

        public static void OnGetParameterTypeString_EnumParameter(Kistl.App.Base.EnumParameter obj, MethodReturnEventArgs<System.String> e)
        {
            if (obj.Enumeration == null)
            {
                e.Result = string.Empty;
            }
            else if (obj.Enumeration.Module == null)
            {
                e.Result = "?." + obj.Enumeration.Name;
            }
            else
            {
                e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.Name;
            }
        }
        #endregion

        public static void OnGetReturnParameter_Method(Method obj, MethodReturnEventArgs<BaseParameter> e)
        {
            e.Result = obj.Parameter.SingleOrDefault(param => param.IsReturnParameter);
        }

        public static void OnGetInheritedMethods_ObjectClass(ObjectClass obj, MethodReturnEventArgs<IList<Method>> e)
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



        public static void OnImplementInterfaces_ObjectClass(ObjectClass objClass)
        {
            IKistlContext ctx = objClass.Context;

            foreach (var iface in objClass.ImplementsInterfaces)
            {
                // TODO: implement CompoundObject too
                #region Properties
                foreach (var prop in iface.Properties)
                {
                    if (!objClass.Properties.Select(p => p.Name).Contains(prop.Name))
                    {
                        // Add Property
                        Property newProp;
                        if (prop is ValueTypeProperty)
                        {
                            newProp = (Property)ctx.Create(ctx.GetInterfaceType(prop));
                        }
                        else if (prop is ObjectReferencePlaceholderProperty)
                        {
                            newProp = ctx.Create<ObjectReferenceProperty>();
                        }
                        else
                        {
                            // TODO: Add CompoundObject
                            continue;
                        }
                        objClass.Properties.Add(newProp);

                        // Default Values
                        newProp.Name = prop.Name;
                        newProp.CategoryTags = prop.CategoryTags;
                        newProp.Description = prop.Description;
                        if (prop is ValueTypeProperty)
                        {
                            ((ValueTypeProperty)newProp).HasPersistentOrder = ((ValueTypeProperty)prop).HasPersistentOrder;
                            ((ValueTypeProperty)newProp).IsList = ((ValueTypeProperty)prop).IsList;
                        }
                        // put the new property into the module of the class
                        newProp.Module = objClass.Module;
                        newProp.ValueModelDescriptor = prop.ValueModelDescriptor;

                        // Copy Constrains
                        foreach (var c in prop.Constraints)
                        {
                            var newC = (Constraint)ctx.Create(ctx.GetInterfaceType(c));
                            newProp.Constraints.Add(newC);
                            newC.Reason = c.Reason;
                        }

                        // Copy Default Value
                        if (prop.DefaultValue != null)
                        {
                            var dv = prop.DefaultValue;
                            var newDV = (DefaultPropertyValue)ctx.Create(ctx.GetInterfaceType(dv));
                            newProp.DefaultValue = newDV;
                        }

                        if (prop is ObjectReferencePlaceholderProperty)
                        {
                            var ph = (ObjectReferencePlaceholderProperty)prop;
                            var objRef = (ObjectReferenceProperty)newProp;

                            // Create Relation
                            var rel = ctx.Create<Relation>();
                            rel.Verb = string.IsNullOrEmpty(ph.Verb) ? "has" : ph.Verb;
                            rel.Module = objClass.Module;
                            rel.Storage = ph.IsList ? StorageType.Separate : StorageType.MergeIntoA;

                            rel.A.Navigator = objRef;
                            rel.A.Type = objClass;
                            rel.A.Multiplicity = Multiplicity.ZeroOrMore;
                            rel.A.HasPersistentOrder = ph.HasPersistentOrder;
                            rel.A.RoleName = string.IsNullOrEmpty(ph.ImplementorRoleName) ? objClass.Name : ph.ImplementorRoleName;

                            rel.B.Type = ph.ReferencedObjectClass;
                            rel.B.Multiplicity = ph.IsList ? Multiplicity.ZeroOrMore : Multiplicity.ZeroOrOne;
                            rel.B.RoleName = string.IsNullOrEmpty(ph.ItemRoleName) ? ph.ReferencedObjectClass.Name : ph.ItemRoleName;
                        }
                    }
                }
                #endregion

                #region Methods
                foreach (Method meth in iface.Methods)
                {
                    // TODO: Wenn das sortieren von Parametern funktioniert müssen auch die Parameter
                    // der Methode geprüft werden
                    // TODO: evtl. IsDeclaredInImplementsInterface aus dem Generator verallgemeinern & benutzen
                    if (!objClass.Methods.Select(m => m.Name).Contains(meth.Name))
                    {
                        Method newMeth = (Method)ctx.Create(ctx.GetInterfaceType(meth));
                        objClass.Methods.Add(newMeth);
                        newMeth.Name = meth.Name;
                        newMeth.IsDisplayable = meth.IsDisplayable;
                        // put the new method into the module of the class
                        newMeth.Module = objClass.Module;

                        foreach (var param in meth.Parameter)
                        {
                            var newParam = (BaseParameter)ctx.Create(ctx.GetInterfaceType(param));
                            newMeth.Parameter.Add(newParam);

                            newParam.Name = param.Name;
                            newParam.Description = param.Description;
                            newParam.IsList = param.IsList;
                            newParam.IsReturnParameter = param.IsReturnParameter;
                        }
                    }
                }
                #endregion

                #region Contraints
                foreach (InstanceConstraint constr in iface.Constraints)
                {
                    if (!objClass.Constraints.Select(c => c.GetObjectClass(ctx)).Contains(constr.GetObjectClass(ctx)))
                    {
                        InstanceConstraint newConstr = (InstanceConstraint)ctx.Create(ctx.GetInterfaceType(constr));
                        objClass.Constraints.Add(newConstr);
                        newConstr.Reason = constr.Reason;
                        if (constr is UniqueConstraint)
                        {
                            var uConstr = (UniqueConstraint)constr;
                            var newUConstr = (UniqueConstraint)newConstr;
                            foreach (var propname in uConstr.Properties.Select(p => p.Name))
                            {
                                var np = objClass.Properties.FirstOrDefault(p => p.Name == propname);
                                if (np != null) newUConstr.Properties.Add(np);
                            }
                        }
                    }
                }
                #endregion
            }
        }

        public static void OnNotifyCreated_Relation(Relation obj)
        {
            obj.A = obj.Context.Create<RelationEnd>();
            obj.B = obj.Context.Create<RelationEnd>();
        }

        #region Document Management
        public static void OnGetStream_Document(Kistl.App.Base.Blob obj, MethodReturnEventArgs<System.IO.Stream> e)
        {
            e.Result = obj.Context.GetStream(obj.ID);
        }

        public static void OnOpen_Document(Kistl.App.Base.Blob obj)
        {
            obj.Context.GetFileInfo(obj.ID).ShellExecute();
        }
        #endregion

    }
}