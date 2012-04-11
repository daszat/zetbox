
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    [Implementor]
    public static class SequenceActions
    {
        [Invocation]
        public static void GetName(Sequence obj, MethodReturnEventArgs<string> e)
        {
            // TODO: Add "Name" property
            //e.Result = "Base.Sequences." + obj.Name;
        }

        [Invocation]
        public static void get_CurrentNumber(Sequence obj, PropertyGetterEventArgs<int?> e)
        {
            // Initialise SequenceData if not available
            if (obj.Data == null)
            {
                obj.Data = obj.Context.Create<SequenceData>();
                obj.Data.CurrentNumber = 0;
            }

            e.Result = obj.Data.CurrentNumber;
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
