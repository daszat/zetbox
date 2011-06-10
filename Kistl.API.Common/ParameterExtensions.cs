
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;

    public static class ParameterExtensions
    {
        /// <summary>
        /// Guesses the System.Type of a parameter. Use only while bootstrapping.
        /// </summary>
        /// <param name="param">the parameter to guess about</param>
        /// <returns></returns>
        public static Type GuessParameterType(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            if (param is BoolParameter && param.IsList)
                return typeof(IList<bool>);
            else if (param is BoolParameter && !param.IsList)
                return typeof(bool);

            else if (param is CLRObjectParameter)
            {
                var p = param as CLRObjectParameter;
                Type t = Type.GetType(p.Type.FullName + (p.Type.Assembly != null ? ", " + p.Type.Assembly.Name : String.Empty), true);
                if (param.IsList)
                    t = typeof(IList<>).MakeGenericType(t);

                return t;
            }

            else if (param is DateTimeParameter && param.IsList)
                return typeof(IList<DateTime>);
            else if (param is DateTimeParameter && !param.IsList)
                return typeof(DateTime);

            else if (param is DoubleParameter && param.IsList)
                return typeof(IList<Double>);
            else if (param is DoubleParameter && !param.IsList)
                return typeof(Double);

            else if (param is IntParameter && param.IsList)
                return typeof(IList<int>);
            else if (param is IntParameter && !param.IsList)
                return typeof(int);

            else if (param is DecimalParameter && param.IsList)
                return typeof(IList<decimal>);
            else if (param is DecimalParameter && !param.IsList)
                return typeof(decimal);

            else if (param is ObjectReferenceParameter)
            {
                var p = param as ObjectReferenceParameter;
                // TEMP
                if (p.ObjectClass == null) return null;
                Type t = Type.GetType(p.ObjectClass.Module.Namespace + "." + p.ObjectClass.Name + ", " + Kistl.API.Helper.InterfaceAssembly, true);
                if (param.IsList)
                    t = typeof(IList<>).MakeGenericType(t);

                return t;
            }

            else if (param is EnumParameter)
            {
                var p = param as EnumParameter;
                Type t = Type.GetType(p.Enumeration.Module.Namespace + "." + p.Enumeration.Name + ", " + Kistl.API.Helper.InterfaceAssembly, true);
                if (param.IsList)
                    t = typeof(IList<>).MakeGenericType(t);

                return t;
            }

            else if (param is CompoundObjectParameter)
            {
                var p = param as CompoundObjectParameter;
                Type t = Type.GetType(p.CompoundObject.Module.Namespace + "." + p.CompoundObject.Name + ", " + Kistl.API.Helper.InterfaceAssembly, true);
                if (param.IsList)
                    t = typeof(IList<>).MakeGenericType(t);

                return t;
            }

            else if (param is StringParameter && param.IsList)
                return typeof(IList<string>);
            else if (param is StringParameter && !param.IsList)
                return typeof(string);
            else
                return null;
        }
    }
}
