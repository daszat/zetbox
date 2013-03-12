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

namespace Zetbox.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Base;

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
            var isNullable = param.IsNullable;
            var isList = param.IsList;

            if (param is BoolParameter && param.IsList)
                return typeof(IEnumerable<bool>);
            else if (param is BoolParameter && !isList && !isNullable)
                return typeof(bool);
            else if (param is BoolParameter && !isList && isNullable)
                return typeof(bool?);

            else if (param is CLRObjectParameter)
            {
                var p = param as CLRObjectParameter;
                Type t = Type.GetType(p.Type.FullName + (p.Type.Assembly != null ? ", " + p.Type.Assembly.Name : String.Empty), true);
                if (isList)
                    t = typeof(IEnumerable<>).MakeGenericType(t);

                return t;
            }

            else if (param is DateTimeParameter && isList)
                return typeof(IEnumerable<DateTime>);
            else if (param is DateTimeParameter && !isList && !isNullable)
                return typeof(DateTime);
            else if (param is DateTimeParameter && !isList && isNullable)
                return typeof(DateTime?);

            else if (param is DoubleParameter && isList)
                return typeof(IEnumerable<double>);
            else if (param is DoubleParameter && !isList && !isNullable)
                return typeof(double);
            else if (param is DoubleParameter && !isList && isNullable)
                return typeof(double?);

            else if (param is IntParameter && isList)
                return typeof(IEnumerable<int>);
            else if (param is IntParameter && !isList && !isNullable)
                return typeof(int);
            else if (param is IntParameter && !isList && isNullable)
                return typeof(int?);

            else if (param is DecimalParameter && isList)
                return typeof(IEnumerable<decimal>);
            else if (param is DecimalParameter && !isList && !isNullable)
                return typeof(decimal);
            else if (param is DecimalParameter && !isList && isNullable)
                return typeof(decimal?);

            else if (param is ObjectReferenceParameter)
            {
                var p = param as ObjectReferenceParameter;
                // TEMP
                if (p.ObjectClass == null) return null;
                Type t = Type.GetType(p.ObjectClass.Module.Namespace + "." + p.ObjectClass.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
                if (isList)
                    t = typeof(IEnumerable<>).MakeGenericType(t);

                return t;
            }

            else if (param is EnumParameter)
            {
                var p = param as EnumParameter;
                Type t = Type.GetType(p.Enumeration.Module.Namespace + "." + p.Enumeration.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
                if (isList)
                    t = typeof(IEnumerable<>).MakeGenericType(t);
                else if (isNullable)
                    t = typeof(Nullable<>).MakeGenericType(t);

                return t;
            }

            else if (param is CompoundObjectParameter)
            {
                var p = param as CompoundObjectParameter;
                Type t = Type.GetType(p.CompoundObject.Module.Namespace + "." + p.CompoundObject.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
                if (isList)
                    t = typeof(IEnumerable<>).MakeGenericType(t);

                return t;
            }

            else if (param is StringParameter && isList)
                return typeof(IEnumerable<string>);
            else if (param is StringParameter && !isList)
                return typeof(string);
            else
                return null;
        }
    }
}
