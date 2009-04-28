using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace Kistl.App.TimeRecords
{
    public class CustomServerActions_TimeRecords
    {
        //public void OnPreSave_WorkEffortAccount(Kistl.App.TimeRecords.WorkEffortAccount obj)
        //{
        //    UpdateWorkEffortAccountAggregations(obj);
        //}

        //public void OnPreSave_Taetigkeit(Kistl.App.TimeRecords.Taetigkeit obj)
        //{
        //    WorkEffortAccount z = obj.WorkEffortAccount;

        //    if (z.Mitarbeiter.FirstOrDefault(m => m.ID == obj.Mitarbeiter.ID) == null)
        //    {
        //        throw new SecurityException("Sie sind nicht berechtigt auf dieses WorkEffortAccount zu buchen.");
        //    }

        //    UpdateWorkEffortAccountAggregations(z);

        //    if (z.BudgetHours.HasValue && z.SpentHours > z.BudgetHours)
        //    {
        //        throw new ArgumentException("Die aktuellen Stunden auf dem WorkEffortAccount überschreiten die max zulässige Anzahl an Stunden.");
        //    }
        //}

        //private void UpdateWorkEffortAccountAggregations(Kistl.App.TimeRecords.WorkEffortAccount obj)
        //{
        //    obj.SpentHours = obj.Taetigkeiten.Sum(t => t.Dauer);
        //}
    }
}
