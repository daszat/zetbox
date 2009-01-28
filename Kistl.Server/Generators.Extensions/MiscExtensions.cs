using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.GeneratorsOld;

namespace Kistl.Server.Generators.Extensions
{
    public static class MiscExtensions
    {

        public static string ToNameSpace(this TaskEnum task)
        {
            if (task == TaskEnum.Interface)
            {
                return "Kistl.Objects";
            }
            else
            {
                return string.Format(@"Kistl.Objects.{0}", task);
            }
        }

        public static string ToCSharpTypeRef(this Type t)
        {
            if (t.IsGenericType)
            {
                return String.Format("{0}<{1}>",
                    t.FullName.Split('`')[0], // TODO: hack to get to class name
                    String.Join(", ", t.GetGenericArguments().Select(arg => arg.ToCSharpTypeRef()).ToArray())
                    );
            }
            else
            {
                return t.FullName;
            }
        }

    }
}
