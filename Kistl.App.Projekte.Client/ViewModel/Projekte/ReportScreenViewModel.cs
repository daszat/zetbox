
namespace Kistl.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.App.GUI;
    using Kistl.API.Dtos;

    [ViewModelDescriptor]
    public class ReportScreenViewModel : NavigationReportScreenViewModel
    {
        public new delegate ReportScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public ReportScreenViewModel(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener)
            : base(appCtx, ctxFactory, dataCtx, parent, screen, fileOpener)
        {
        }

        [Serializable]
        [GuiPrintableRoot]
        [GuiTitle("Projektbericht")]
        public class Report
        {
            public Report()
            {
                Months = new List<MonthEntry>();
            }

            public DateTime From { get; set; }
            public DateTime Until { get; set; }

            public decimal Total { get; set; }
            public decimal TotalCorrected { get; set; }

            [GuiTitle("Monate")]
            public List<MonthEntry> Months { get; set; }

            [Serializable]
            public class MonthEntry
            {
                public string Year { get; set; }
                public string Month { get; set; }

                public decimal Total { get; set; }
                public decimal TotalCorrected { get; set; }
            }
        }

        protected override object LoadStatistic(DateTime from, DateTime until)
        {
            var rpt = new Report() { Total = 1, TotalCorrected = 2, From = from, Until = until };
            rpt.Months.Add(new Report.MonthEntry() { Month = "01", Year = "2012", Total = 1, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "02", Year = "2012", Total = 2, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "03", Year = "2012", Total = 3, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "04", Year = "2012", Total = 4, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "05", Year = "2012", Total = 5, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "06", Year = "2012", Total = 6, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "07", Year = "2012", Total = 7, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "08", Year = "2012", Total = 8, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "09", Year = "2012", Total = 9, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "00", Year = "2012", Total = 10, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "11", Year = "2012", Total = 11, TotalCorrected = 10 });
            rpt.Months.Add(new Report.MonthEntry() { Month = "12", Year = "2012", Total = 12, TotalCorrected = 10 });
            return rpt;
        }
    }
}
