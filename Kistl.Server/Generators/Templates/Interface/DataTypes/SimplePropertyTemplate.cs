using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Arebis.CodeGeneration;


namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    public partial class SimplePropertyTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Property p, bool isReadonly)
        {
            host.CallTemplate("Interface.DataTypes.SimplePropertyTemplate", ctx, p, isReadonly);
        }
    }
}
