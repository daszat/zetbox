using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
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
            string[] interfaces = GetInterfaces();
            if (interfaces.Length > 0)
            {
                return ": " + String.Join(", ", interfaces);
            }
            else
            {
                return "";
            }
        }

        protected virtual void ApplyPropertyTemplate(Property prop)
        {
            if (!prop.IsListProperty())
            {
                bool isReadonly = prop is StructProperty;
                Interface.DataTypes.SimplePropertyTemplate.Call(Host, ctx, prop, isReadonly);
            }
            else
            {
                Interface.DataTypes.SimplePropertyListTemplate.Call(Host, ctx, prop);
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
        /// <param name="prop">Method to check</param>
        /// <returns>true if found in ImplementsIntercafe Collection</returns>
        protected bool IsDeclaredInImplementsInterface(Kistl.App.Base.Method meth)
        {
            if (meth.ObjectClass is ObjectClass)
            {
                ObjectClass cls = (ObjectClass)meth.ObjectClass;
                List<Kistl.App.Base.Method> methods = new List<Kistl.App.Base.Method>();
                foreach(var c in cls.ImplementsInterfaces)
                {
                    methods.AddRange(c.Methods.Where(m => m.MethodName == meth.MethodName));
                }
                foreach(var m in methods)
                {
                    if (m.Parameter.Count == meth.Parameter.Count)
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

        protected virtual void ApplyMethodTemplate(Kistl.App.Base.Method m)
        {
            Interface.DataTypes.Method.Call(Host, ctx, m);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return dataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
