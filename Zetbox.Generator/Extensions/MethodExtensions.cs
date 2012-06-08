
namespace Zetbox.Generator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Base;

    public static class MethodExtensions
    {
        public static string GetParameterDefinitions(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            return String.Join(", ",
                method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetParameterDefinition(p))
                .ToArray());
        }

        public static string GetParameterDefinition(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            return String.Format("{0} {1}", param.GetParameterTypeString(), param.Name);
        }

        public static string GetArguments(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            return String.Join(", ",
                method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetArgument(p))
                .ToArray());
        }

        public static string GetArgument(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            return param.Name;
        }

        public static string GetArgumentTypes(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            return String.Join(", ",
                method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetArgumentType(p))
                .ToArray());
        }

        public static string GetArgumentType(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            return "typeof(" + param.GetParameterTypeString() + ")";
        }


        public static IOrderedEnumerable<Method> OrderByDefault(this IEnumerable<Method> methods)
        {
            if (methods == null) { throw new ArgumentNullException("methods"); }

            return methods
                .OrderBy(m => m.Name)
                .ThenBy(m => m.Parameter.Count)
                .ThenBy(m => String.Join("|", m.Parameter.Select(p => p.GetParameterTypeString()).ToArray()));
        }
    }
}
