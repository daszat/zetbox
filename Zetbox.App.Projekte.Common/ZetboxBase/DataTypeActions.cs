// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;

    [Implementor]
    public static class DataTypeActions
    {
        [Invocation]
        public static void ToString(DataType obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}.{1}",
                obj.Module == null
                    ? "[no module]"
                    : obj.Module.Namespace,
                obj.Name);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetDataType(DataType obj, Zetbox.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetDataTypeString() + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
        }

        [Invocation]
        public static void GetDataTypeString(DataType obj, Zetbox.API.MethodReturnEventArgs<string> e)
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

        [Invocation]
        public static void ImplementInterfaces(DataType obj)
        {
            IZetboxContext ctx = obj.Context;
            if (obj.Module == null)
            {
                throw new InvalidOperationException("ObjectClass has no Module yet");
            }

            foreach (var iface in obj.ImplementsInterfaces)
            {
                // TODO: implement CompoundObject too
                #region Properties
                foreach (var prop in iface.Properties)
                {
                    Property newProp = obj.Properties.SingleOrDefault(p => p.Name == prop.Name);
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
                        obj.Properties.Add(newProp);

                        // Default Values
                        newProp.Name = prop.Name;
                        newProp.Label = prop.Label;
                        newProp.CategoryTags = prop.CategoryTags;
                        newProp.Description = prop.Description;

                        // put the new property into the module of the class
                        newProp.Module = obj.Module;

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
                            rel.Module = obj.Module;
                            rel.Storage = ph.IsList ? StorageType.Separate : StorageType.MergeIntoA;

                            rel.A.Navigator = objRef;
                            rel.A.Type = (ObjectClass)obj;
                            rel.A.Multiplicity = Multiplicity.ZeroOrMore;
                            rel.A.HasPersistentOrder = ph.HasPersistentOrder;
                            rel.A.RoleName = string.IsNullOrEmpty(ph.ImplementorRoleName) ? obj.Name : ph.ImplementorRoleName;

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
                    if (!obj.Methods.Select(m => m.Name).Contains(meth.Name))
                    {
                        Method newMeth = (Method)ctx.Create(ctx.GetInterfaceType(meth));
                        obj.Methods.Add(newMeth);
                        newMeth.Name = meth.Name;
                        newMeth.IsDisplayable = meth.IsDisplayable;
                        // put the new method into the module of the class
                        newMeth.Module = obj.Module;

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
                    if (!obj.Constraints.Select(c => c.GetObjectClass(ctx)).Contains(constr.GetObjectClass(ctx)))
                    {
                        InstanceConstraint newConstr = (InstanceConstraint)ctx.Create(ctx.GetInterfaceType(constr));
                        obj.Constraints.Add(newConstr);
                        newConstr.Reason = constr.Reason;
                        if (constr is IndexConstraint)
                        {
                            var uConstr = (IndexConstraint)constr;
                            var newUConstr = (IndexConstraint)newConstr;
                            newUConstr.IsUnique = uConstr.IsUnique;
                            foreach (var propname in uConstr.Properties.Select(p => p.Name))
                            {
                                var np = obj.Properties.FirstOrDefault(p => p.Name == propname);
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
