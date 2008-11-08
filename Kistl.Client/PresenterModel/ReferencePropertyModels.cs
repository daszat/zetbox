using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    public class ReferencePropertyModel : ValuePropertyModel<IDataObject>, IDataErrorInfo
    {
        public ReferencePropertyModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj, ObjectReferenceProperty bp)
            : base(uiManager, asyncManager, obj, bp)
        {
        }
    }
}
