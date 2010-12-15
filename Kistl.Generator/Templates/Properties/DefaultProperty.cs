
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    
    public partial class DefaultProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string propName, string presentedType)
        {
            _host.CallTemplate("Properties.DefaultProperty", ctx, propName, presentedType);
        }
    }
}
