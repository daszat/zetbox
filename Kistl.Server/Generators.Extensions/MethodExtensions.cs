
namespace Kistl.Server.Generators.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;

    public static class MethodExtensions
    {
        /// <summary>
        /// returns true if the Method is one of the "default" methods, "ToString", "NotifyPreSave", "NotifyPostSave", "NotifyCreated" or "NotifyDeleting".
        /// </summary>
        public static bool IsDefaultMethod(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            switch (method.Name)
            {
                case "ToString":
                case "NotifyPreSave":
                case "NotifyPostSave":
                case "NotifyCreated":
                case "NotifyDeleting":
                    return true;
                default:
                    return false;
            }
        }

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
