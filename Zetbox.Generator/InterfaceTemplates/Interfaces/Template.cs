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

namespace Zetbox.Generator.InterfaceTemplates.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class Template
    {
        /// <returns>The interfaces this type implements</returns>
        protected virtual string[] GetInterfaces()
        {
            string[] interfaces = dataType.ImplementsInterfaces.Select(i => i.Module.Namespace + "." + i.Name).ToArray();

            if (dataType is Zetbox.App.Base.ObjectClass)
            {
                var baseClass = (dataType as Zetbox.App.Base.ObjectClass).BaseObjectClass;
                if (baseClass != null)
                {
                    return new string[] { baseClass.Module.Namespace + "." + baseClass.Name }.Concat(interfaces).ToArray();
                }
                else
                {
                    return new string[] { "IDataObject" }.Concat(interfaces).ToArray();
                }
            }
            else if (dataType is CompoundObject)
            {
                return new string[] { "ICompoundObject" };
            }
            else if (dataType is Zetbox.App.Base.Interface)
            {
                return interfaces;
            }
            else
            {
                throw new ApplicationException(String.Format("Do not know how to handle {0}", dataType));
            }
        }

        /// <returns>a string defining the inheritance relations of this class</returns>
        protected virtual string GetInheritance()
        {
            string[] interfaces = GetInterfaces().OrderBy(s => s).ToArray();
            if (interfaces.Length > 0)
            {
                return ": " + String.Join(", ", interfaces);
            }
            else
            {
                return String.Empty;
            }
        }

        protected virtual void ApplyPropertyTemplate(Property prop)
        {
            bool isReadOnly = false;
            bool isList = false;

            if (prop is ValueTypeProperty)
            {
                var vtp = (ValueTypeProperty)prop;
                isList = vtp.IsList;
                isReadOnly = vtp.IsCalculated;
            }
            else if (prop is CompoundObjectProperty)
            {
                isList = ((CompoundObjectProperty)prop).IsList;
            }
            else if (prop is ObjectReferenceProperty)
            {
                isList = ((ObjectReferenceProperty)prop).IsList();
            }
            else if (prop is CalculatedObjectReferenceProperty)
            {
                isReadOnly = true;
            }

            if (isList)
            {
                Properties.SimplePropertyListTemplate.Call(Host, ctx, prop);
            }
            else
            {
                Properties.SimplePropertyTemplate.Call(Host, ctx, prop, isReadOnly);
            }
        }

        /// <summary>
        /// Check if Property was defined on a "ImplementsInterface" Interface
        /// </summary>
        /// <param name="prop">Property to check</param>
        /// <returns>true if found in ImplementsIntercafe Collection</returns>
        protected bool IsDeclaredInImplementsInterface(Property prop)
        {
            return prop.ObjectClass.ImplementsInterfaces
                .FirstOrDefault(c => c.Properties.FirstOrDefault(p => p.Name == prop.Name) != null) != null;
        }

        /// <summary>
        /// Check if Method was defined on a "ImplementsInterface" Interface
        /// </summary>
        /// <param name="method">Method to check</param>
        /// <returns>true if found in ImplementsInterface Collection</returns>
        protected bool IsDeclaredInImplementsInterface(Zetbox.App.Base.Method method)
        {
            List<Zetbox.App.Base.Method> methods = new List<Zetbox.App.Base.Method>();
            foreach (var c in method.ObjectClass.ImplementsInterfaces)
            {
                methods.AddRange(c.Methods.Where(m => m.Name == method.Name));
            }
            foreach (var m in methods)
            {
                if (m.Parameter.Count == method.Parameter.Count)
                {
                    bool paramSame = true;

                    // TODO: Uncomment this when IList Poshandling works correctly
                    //for (int i = 0; i < m.Parameter.Count; i++)
                    //{
                    //    if (   m.Parameter[i].GetParameterType() != meth.Parameter[i].GetParameterType()
                    //        || m.Parameter[i].IsReturnParameter != meth.Parameter[i].IsReturnParameter)
                    //    {
                    //        paramSame = false;
                    //        break;
                    //    }
                    //}

                    if (paramSame)
                        return true;
                }
            }
            return false;
        }

        protected virtual void ApplyMethodTemplate(Zetbox.App.Base.Method m, int index)
        {
            var returnParam = m.Parameter.SingleOrDefault(p => p.IsReturnParameter);
            var returnString = returnParam == null ? "void" : returnParam.GetParameterTypeString();
            var name = m.Name;
            var args = String.Join(", ", m.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => p.GetParameterTypeString() + " " + p.Name)
                .ToArray());

            this.WriteLine("        {0} {1}({2});", returnString, name, args);
        }

        protected IEnumerable<Zetbox.App.Base.Method> MethodsToGenerate()
        {
            return dataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
