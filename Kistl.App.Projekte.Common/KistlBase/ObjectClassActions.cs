
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;

    [Implementor]
    public static class ObjectClassActions
    {
        #region CreateDefaultMethods
        private static void CheckDefaultMethod(ObjectClass obj, string methodName)
        {
            var m = obj.Methods.SingleOrDefault(i => i.Name == methodName);
            if (m == null && obj.BaseObjectClass == null)
            {
                // Only for BaseClasses
                m = obj.Context.Create<Method>();
                m.Name = methodName;
                m.Module = obj.Module;
                obj.Methods.Add(m);
            }
            // Do not delete if BaseClass is declared
            // deleting should be a user action
            // TODO: Add Object Level Constraint
        }

        [Invocation]
        public static void CreateDefaultMethods(Kistl.App.Base.ObjectClass obj)
        {
            CheckDefaultMethod(obj, "ToString");
            CheckDefaultMethod(obj, "NotifyPreSave");
            CheckDefaultMethod(obj, "NotifyPostSave");
            CheckDefaultMethod(obj, "NotifyCreated");
            CheckDefaultMethod(obj, "NotifyDeleting");

            var toStr = obj.Methods.SingleOrDefault(i => i.Name == "ToString");
            if (toStr != null && toStr.Parameter.Count == 0)
            {
                var p = obj.Context.Create<StringParameter>();
                p.IsReturnParameter = true;
                p.Name = "retVal";
                toStr.Parameter.Add(p);
            }
        }
        #endregion

        [Invocation]
        public static void NotifyPreSave(Kistl.App.Base.ObjectClass obj)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        [Invocation]
        public static void NotifyCreated(Kistl.App.Base.ObjectClass obj)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        [Invocation]
        public static void postSet_BaseObjectClass(Kistl.App.Base.ObjectClass obj, PropertyPostSetterEventArgs<Kistl.App.Base.ObjectClass> e)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        [Invocation]
        public static void CreateRelation(ObjectClass obj, MethodReturnEventArgs<Relation> e)
        {
            e.Result = obj.Context.Create<Relation>();
            e.Result.Module = obj.Module;

            if (e.Result.A == null)
            {
                e.Result.A = obj.Context.Create<RelationEnd>();
            }
            e.Result.A.Type = obj;

            if (e.Result.B == null)
            {
                e.Result.B = obj.Context.Create<RelationEnd>();
            }
        }

        [Invocation]
        public static void CreateMethod(ObjectClass obj, MethodReturnEventArgs<Method> e)
        {
            e.Result = obj.Context.Create<Method>();
            e.Result.Module = obj.Module;
            e.Result.ObjectClass = obj;
        }

        [Invocation]
        public static void GetInheritedMethods(ObjectClass obj, MethodReturnEventArgs<IList<Method>> e)
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

        [Invocation]
        public static void ImplementInterfaces(ObjectClass objClass)
        {
            IKistlContext ctx = objClass.Context;
            if (objClass.Module == null)
            {
                throw new InvalidOperationException("ObjectClass has no Module yet");
            }

            foreach (var iface in objClass.ImplementsInterfaces)
            {
                // TODO: implement CompoundObject too
                #region Properties
                foreach (var prop in iface.Properties)
                {
                    Property newProp = objClass.Properties.SingleOrDefault(p => p.Name == prop.Name);
                    if (newProp == null)
                    {
                        // Add Property
                        if (prop is ValueTypeProperty || prop is CompoundObjectProperty)
                        {
                            newProp = (Property)ctx.Create(ctx.GetInterfaceType(prop));
                        }
                        else if (prop is ObjectReferencePlaceholderProperty)
                        {
                            newProp = ctx.Create<ObjectReferenceProperty>();
                        }
                        else
                        {
                            Logging.Client.WarnFormat("Cannot implement interface for property [{0}] of Type [{1}]", prop.ToString(), prop.GetType().FullName);
                            continue;
                        }
                        objClass.Properties.Add(newProp);

                        // Default Values
                        newProp.Name = prop.Name;
                        newProp.Label = prop.Label;
                        newProp.CategoryTags = prop.CategoryTags;
                        newProp.Description = prop.Description;

                        // put the new property into the module of the class
                        newProp.Module = objClass.Module;

                        newProp.ValueModelDescriptor = prop.ValueModelDescriptor;
                        newProp.RequestedKind = prop.RequestedKind;

                        if (prop is ValueTypeProperty)
                        {
                            ((ValueTypeProperty)newProp).HasPersistentOrder = ((ValueTypeProperty)prop).HasPersistentOrder;
                            ((ValueTypeProperty)newProp).IsList = ((ValueTypeProperty)prop).IsList;
                        }
                        else if (prop is ObjectReferencePlaceholderProperty)
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
                        else if (prop is CompoundObjectProperty)
                        {
                            var cop = prop as CompoundObjectProperty;
                            var newCop = newProp as CompoundObjectProperty;

                            newCop.CompoundObjectDefinition = cop.CompoundObjectDefinition;
                            newCop.HasPersistentOrder = cop.HasPersistentOrder;
                            newCop.IsList = cop.IsList;
                        }
                    } // if (newProp == null)

                    // Copy Constrains
                    foreach (var c in prop.Constraints)
                    {
                        if (!newProp.Constraints.Select(i => i.GetObjectClass(ctx)).Contains(c.GetObjectClass(ctx)))
                        {
                            var newC = (Constraint)ctx.Create(ctx.GetInterfaceType(c));
                            newProp.Constraints.Add(newC);
                            newC.Reason = c.Reason;
                        }
                    }

                    // Copy Default Value
                    if (newProp.DefaultValue == null && prop.DefaultValue != null)
                    {
                        var dv = prop.DefaultValue;
                        var newDV = (DefaultPropertyValue)ctx.Create(ctx.GetInterfaceType(dv));
                        newProp.DefaultValue = newDV;
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

                #region Constraints
                foreach (InstanceConstraint constr in iface.Constraints)
                {
                    if (!objClass.Constraints.Select(c => c.GetObjectClass(ctx)).Contains(constr.GetObjectClass(ctx)))
                    {
                        InstanceConstraint newConstr = (InstanceConstraint)ctx.Create(ctx.GetInterfaceType(constr));
                        objClass.Constraints.Add(newConstr);
                        newConstr.Reason = constr.Reason;
                        if (constr is IndexConstraint)
                        {
                            var uConstr = (IndexConstraint)constr;
                            var newUConstr = (IndexConstraint)newConstr;
                            newUConstr.IsUnique = uConstr.IsUnique;
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
    }
}
