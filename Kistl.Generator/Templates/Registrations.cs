
namespace Kistl.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class Registrations
    {
        protected Registrations(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {
            throw new InvalidOperationException("Do not call this constructor, use the parameterized one!");
        }


        internal static void Call(Arebis.CodeGeneration.IGenerationHost Host, API.IKistlContext ctx, string shortName)
        {
            Host.CallTemplate("Registrations", ctx, shortName);
        }
    }
}
