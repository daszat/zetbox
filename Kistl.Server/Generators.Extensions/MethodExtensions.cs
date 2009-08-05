using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.Extensions
{
    public static class MethodExtensions
    {
        /// <summary>
        /// returns true if the Method is one of the "default" methods, "ToString", "PreSave", "PostSave", "Created" or "Deleting".
        /// </summary>
        public static bool IsDefaultMethod(this Method method)
        {
            switch (method.MethodName)
            {
                case "ToString":
                case "PreSave":
                case "PostSave":
                case "Created":
                case "Deleting":
                    return true;
                default:
                    return false;
            }
        }

        public static string GetParameterDefinitions(this Method method)
        {
            return String.Join(", ",
                method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetParameterDefinition(p))
                .ToArray());
        }

        public static string GetParameterDefinition(this BaseParameter param)
        {
            return String.Format("{0} {1}", param.GetParameterTypeString(), param.ParameterName);
        }

        public static string GetArguments(this Method method)
        {
            return String.Join(", ",
                method.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetArgument(p))
                .ToArray());
        }

        public static string GetArgument(this BaseParameter param)
        {
            return String.Format("{0}", param.ParameterName);
        }

        public static IOrderedEnumerable<Method> OrderByDefault(this IEnumerable<Method> methods)
        {
            return methods.OrderBy(m => m.MethodName).ThenBy(m => m.Parameter.Count).ThenBy(m => String.Join("|", m.Parameter.Select(p => p.GetParameterTypeString()).ToArray()));
        }
    }
}
