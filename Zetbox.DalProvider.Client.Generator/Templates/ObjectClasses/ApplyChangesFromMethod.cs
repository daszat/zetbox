
namespace Zetbox.DalProvider.Client.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;

    public partial class ApplyChangesFromMethod
    {
        public static void Call(IGenerationHost host, IZetboxContext ctx, string otherInterface, DataType dataType, string implName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (dataType == null) { throw new ArgumentNullException("dataType"); }

            var clsName = dataType.Name;

            Call(host, ctx, otherInterface, dataType, clsName, implName);
        }

        public static void Call(IGenerationHost host, IZetboxContext ctx, DataType dataType, string implName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (dataType == null) { throw new ArgumentNullException("dataType"); }

            var clsName = dataType.Name;
            Call(host, ctx, "IPersistenceObject", dataType, clsName, implName);
        }
    }
}
