using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client.Mocks.OneNLists
{
    public interface IOneSide : IDataObject
    {
        IList<INSide> NSide { get; }
    }
}
