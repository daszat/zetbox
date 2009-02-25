#define HACK_FOR_DAVIDS_GUI

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public class ReadOnlyContextException : NotSupportedException
    {
        public ReadOnlyContextException()
            : base("This context is readonly")
        {
        }
    }

    public class ReadOnlyObjectException : NotSupportedException
    {
        public ReadOnlyObjectException()
            : base("This object is readonly")
        {
        }
    }

    public static class FrozenContext
    {
        private static IKistlContext _Single = null;
        public static IKistlContext Single
        {
            get
            {
                if (_Single == null)
                {
                    Type t = Type.GetType("Kistl.App.FrozenContextImplementation, Kistl.Objects.Frozen", true);
                    _Single = (IKistlContext)Activator.CreateInstance(t);

                    if (_Single == null) throw new InvalidOperationException("Unable to create frozen context");
                }
                return _Single;
            }
        }
    }
}
