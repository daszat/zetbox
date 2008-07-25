using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using Kistl.App.Base;

namespace Kistl.Server.Generators
{
    public static class GeneratorHelper
    {
        public static CodeTypeReference ToCodeTypeReference(this BaseParameter param)
        {
            CodeTypeReference result;

            if (param == null)
            {
                result = new CodeTypeReference(typeof(void));
            }
            else
            {
                if (param.IsList)
                {
                    result = new CodeTypeReference(String.Format("IList<{0}>", param.GetParameterTypeString()));
                }
                else
                {
                    result = new CodeTypeReference(param.GetParameterTypeString());
                }
            }

            return result;
        }
    }
}
