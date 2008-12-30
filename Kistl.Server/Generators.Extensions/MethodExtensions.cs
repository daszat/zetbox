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
        /// returns true if the Method is one of the "default" methods, "ToString", "PreSave" or "PostSave".
        /// </summary>
        public static bool IsDefaultMethod(this Method method)
        {
            return (method.Module.ModuleName == "KistlBase")
                && (method.MethodName == "ToString"
                    || method.MethodName == "PreSave"
                    || method.MethodName == "PostSave");
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
    }
}
