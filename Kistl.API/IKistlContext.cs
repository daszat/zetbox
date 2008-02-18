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
        void Delete(IDataObject obj);

        void Attach(ICollectionEntry e);
        void Dettach(ICollectionEntry e);
        void Delete(ICollectionEntry e);
    }
}
