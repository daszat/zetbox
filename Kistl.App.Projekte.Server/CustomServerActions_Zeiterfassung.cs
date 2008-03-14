using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Zeiterfassung
{
    public class CustomServerActions_Zeiterfassung
    {
        public void OnPreSave_Zeitkonto(Kistl.App.Zeiterfassung.Zeitkonto obj)
        {
            UpdateZeitkontoAggregations(obj);
        }

        public void OnPreSave_Taetigkeit(Kistl.App.Zeiterfassung.Taetigkeit obj)
        {
            Zeitkonto z = obj.Zeitkonto;

            if (z.Mitarbeiter.FirstOrDefault(m => m.Value.ID == obj.Mitarbeiter.ID) == null)
            {
                throw new ApplicationException("Sie sind nicht berechtigt auf dieses Zeitkonto zu buchen.");
            }

            UpdateZeitkontoAggregations(z);

            if (z.MaxStunden.HasValue && z.AktuelleStunden > z.MaxStunden)
            {
                throw new ApplicationException("Die aktuellen Stunden auf dem Zeitkonto überschreiten die max zulässige Anzahl an Stunden.");
            }
        }

        private void UpdateZeitkontoAggregations(Kistl.App.Zeiterfassung.Zeitkonto obj)
        {
            obj.AktuelleStunden = obj.Taetigkeiten.Sum(t => t.Dauer);
        }
    }
}
