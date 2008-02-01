using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public interface IKistlContext
    {
        void Attach(IDataObject obj);
        void Dettach(IDataObject obj);
    }
}
