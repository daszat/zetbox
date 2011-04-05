
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
    using Kistl.Client.Presentables;
    using Kistl.App.GUI;
    using Kistl.Client;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class ObjectClassActions
    {
        private static IViewModelFactory _mdlFactory = null;

        public ObjectClassActions(IViewModelFactory mdlFactory)
        {
            _mdlFactory = mdlFactory;
        }

        [Invocation]
        public static void NotifyCreated(ObjectClass obj)
        {
            obj.DefaultViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_DataObjectViewModel);
        }

        [Invocation]
        public static void ImplementInterfaces(ObjectClass objClass)
        {
            IKistlContext ctx = objClass.Context;
            if (objClass.Module == null)
            {
                _mdlFactory.ShowMessage("ObjectClass has no Module yet", "Error");
                return;
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
                    }

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

    }
}
