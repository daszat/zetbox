
namespace Kistl.API.Server.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;

    public sealed class ServerImplementationTypeMock
        : ImplementationType
    {
        public ServerImplementationTypeMock(Type t, InterfaceType.Factory iftFactory)
            : base(t, iftFactory, new Kistl.API.Mocks.MockImplementationTypeChecker())
        {
        }

        public override InterfaceType ToInterfaceType()
        {
            var ifTypeName = Type.FullName.Replace(Helper.ImplementationSuffix, String.Empty);
            if (ifTypeName.Contains("Mock"))
            {
                return IftFactory(Type.GetType(ifTypeName, true));
            }
            else
            {
                return IftFactory(Type.GetType(ifTypeName + ", " + typeof(ObjectClass).Assembly.FullName, true));
            }
        }
    }
}
