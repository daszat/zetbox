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
    public partial class Method
    {
#if INTELLISENSE
        protected Arebis.CodeGeneration.IGenerationHost Host;
        protected string ResolveResourceUrl(string template) { return "mock"; }

        protected Kistl.App.Base.Method m;

        protected Method(Arebis.CodeGeneration.IGenerationHost h, Kistl.App.Base.Method m) { }
#endif

        protected virtual string GetReturnType()
        {
            var ret = m.GetReturnParameter();
            if (ret == null)
            {
                return "void";
            }
            else
            {
                return ret.GetParameterTypeString();
            }
        }

        protected virtual string GetParameterDefinition(BaseParameter param)
        {
            return String.Format("{0} {1}", param.GetParameterTypeString(), param.ParameterName);
        }

        protected virtual string GetParameterDefinitions()
        {
            return String.Join(", ",
                m.Parameter
                .Where(p => !p.IsReturnParameter)
                .Select(p => GetParameterDefinition(p))
                .ToArray());
        }

    }
}
