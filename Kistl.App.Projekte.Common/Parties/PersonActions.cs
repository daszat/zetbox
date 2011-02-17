using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Extensions;
using Kistl.App.GUI;
using Kistl.API.Utils;

namespace ZBox.Basic.Parties
{
    [Implementor]
    public static class PersonActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Parties.Person obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} {1}", obj.FirstName, obj.LastName);
        }

        [Invocation]
        public static void get_LastName(ZBox.Basic.Parties.Person obj, PropertyGetterEventArgs<System.String> e)
        {
        }

        [Invocation]
        public static void NotifyPreSave(ZBox.Basic.Parties.Person obj)
        {
        }

        [Invocation]
        public static void postSet_FirstName(ZBox.Basic.Parties.Person obj, PropertyPostSetterEventArgs<System.String> e)
        {
        }

        [Invocation]
        public static void preSet_FirstName(ZBox.Basic.Parties.Person obj, PropertyPreSetterEventArgs<System.String> e)
        {
        }
    }
}
