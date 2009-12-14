
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
            if (param is BoolParameter && param.IsList)
                return typeof(IList<bool>);
            else if (param is BoolParameter && !param.IsList)
                return typeof(bool);

            else if (param is CLRObjectParameter)
            {
                var p = param as CLRObjectParameter;
                Type t = Type.GetType(p.Type.FullName + (p.Type.Assembly != null ? ", " + p.Type.Assembly.AssemblyName : String.Empty), true);
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

            else if (param is ObjectParameter)
            {
                var p = param as ObjectParameter;
                Type t = Type.GetType(p.DataType.Module.Namespace + "." + p.DataType.ClassName + ", Kistl.Objects", true);
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
