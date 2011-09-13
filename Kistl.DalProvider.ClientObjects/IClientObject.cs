
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    internal interface IClientObject
    {
        void SetUnmodified();
        void SetDeleted();
        void SetNew();

        BasePersistenceObject UnderlyingObject { get; }

        void MakeAccessDeniedProxy();
    }
}
