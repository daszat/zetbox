using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    public partial class Template
    {

        /// <returns>The interfaces this type implements</returns>
        protected virtual string[] GetInterfaces()
        {
            if (dataType is Kistl.App.Base.ObjectClass)
            {
                ObjectClass cls = (ObjectClass)dataType;
                string[] interfaces = cls.ImplementsInterfaces.Select(i => i.Module.Namespace + "." + i.ClassName).ToArray();
                var baseClass = (dataType as Kistl.App.Base.ObjectClass).BaseObjectClass;
                if (baseClass != null)
                {
                    return new string[] { baseClass.Module.Namespace + "." + baseClass.ClassName }.Concat(interfaces).ToArray();
                }
                else
                {
                    return new string[] { "IDataObject" }.Concat(interfaces).ToArray();
                }
            }
            else if (dataType is Struct)
            {
                return new string[] { "IStruct" };
            }
            else if (dataType is Kistl.App.Base.Interface)
            {
                // no type hierarchy here
                return new string[] { };
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
                isList = ((ValueTypeProperty)prop).IsList;
            }
            else if (prop is StructProperty)
            {
                isReadOnly = true;
                isList = ((StructProperty)prop).IsList;
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
                Interface.DataTypes.SimplePropertyListTemplate.Call(Host, ctx, prop);
            }
            else
            {
                Interface.DataTypes.SimplePropertyTemplate.Call(Host, ctx, prop, isReadOnly);
            }
        }

        /// <summary>
        /// Check if Property was defined on a "ImplementsInterface" Interface
        /// </summary>
        /// <param name="prop">Property to check</param>
        /// <returns>true if found in ImplementsIntercafe Collection</returns>
        protected bool IsDeclaredInImplementsInterface(Property prop)
        {
            if (prop.ObjectClass is ObjectClass)
            {
                ObjectClass cls = (ObjectClass)prop.ObjectClass;
                if (cls.ImplementsInterfaces.FirstOrDefault(c => c.Properties.FirstOrDefault(p => p.PropertyName == prop.PropertyName) != null) != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if Method was defined on a "ImplementsInterface" Interface
        /// </summary>
        /// <param name="method">Method to check</param>
        /// <returns>true if found in ImplementsIntercafe Collection</returns>
        protected bool IsDeclaredInImplementsInterface(Kistl.App.Base.Method method)
        {
            if (method.ObjectClass is ObjectClass)
            {
                ObjectClass cls = (ObjectClass)method.ObjectClass;
                List<Kistl.App.Base.Method> methods = new List<Kistl.App.Base.Method>();
                foreach (var c in cls.ImplementsInterfaces)
                {
                    methods.AddRange(c.Methods.Where(m => m.MethodName == method.MethodName));
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

                        if (paramSame) return true;
                    }
                }
            }
            return false;
        }

        protected virtual void ApplyMethodTemplate(Kistl.App.Base.Method m, int index)
        {
            Interface.DataTypes.Method.Call(Host, ctx, m, index);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return dataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
