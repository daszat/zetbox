
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using System.Text.RegularExpressions;

    [Implementor]
    public static class SequenceActions
    {
        [Invocation]
        public static void ToString(Sequence obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void GetName(Sequence obj, MethodReturnEventArgs<string> e)
        {
            if (!string.IsNullOrEmpty(obj.Name) && obj.Module != null && !string.IsNullOrEmpty(obj.Module.Name))
            {
                e.Result = "Base.Sequences." + obj.Module.Name + "." + Regex.Replace(obj.Name, "\\W", "_");
            }
        }

        [Invocation]
        public static void preSet_Data(Sequence obj, PropertyPreSetterEventArgs<SequenceData> e)
        {
            // TODO: Workaroud: No! Changing Sequence Data is not allowed
            if (e.OldValue != null)
            {
                e.Result = e.OldValue;
            }
        }
    }
}
