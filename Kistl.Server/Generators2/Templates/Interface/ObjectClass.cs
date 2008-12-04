using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators2.Templates.Interface
{
    public partial class ObjectClass
    {
        string GetBaseClass()
        {
            if (objClass.BaseObjectClass != null)
            {
                return " : " + objClass.BaseObjectClass.Module.Namespace + "." + objClass.BaseObjectClass.ClassName;
            }
            return "";
        }
    }
}
