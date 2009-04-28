using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.App.TimeRecords
{
    public class CustomClientActions_TimeRecords
    {

        public void OnToString_WorkEffortAccount(WorkEffortAccount obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

    }
}
