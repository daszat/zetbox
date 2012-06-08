using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API.Client.Mocks.OneNLists
{
    public interface INSide : IDataObject
    {
        IOneSide OneSide { get; set; }
    }
}
