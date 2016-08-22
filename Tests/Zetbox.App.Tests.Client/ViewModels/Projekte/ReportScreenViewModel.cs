// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.API.Dtos;

    [ViewModelDescriptor]
    public class ReportScreenViewModel : NavigationReportScreenViewModel
    {
        public new delegate ReportScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        public ReportScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener, ITempFileService tmpService)
            : base(appCtx, dataCtx, parent, screen, fileOpener, tmpService)
        {
        }

        [Serializable]
        public class MonthEntry
        {
            public string Year { get; set; }
            public string Month { get; set; }

            public decimal Total { get; set; }
            public decimal TotalCorrected { get; set; }

            public decimal TotalEURO { get; set; }
            public decimal TotalEUROCorrected { get; set; }

            public decimal TotalYEN { get; set; }
            public decimal TotalYENCorrected { get; set; }

            public decimal TotalUSD { get; set; }
            public decimal TotalUSDCorrected { get; set; }
        }


        [Serializable]
        [GuiTitle("Aufgabenübersicht")]
        public class TaskReport
        {
            public TaskReport()
            {
                Months = new List<MonthEntry>();
            }

            public DateTime From { get; set; }
            public DateTime Until { get; set; }

            public decimal Total { get; set; }
            public decimal TotalCorrected { get; set; }

            [GuiTitle("Monate")]
            public List<MonthEntry> Months { get; set; }

        }

        [Serializable]
        [GuiTitle("Projektbericht")]
        [GuiGrid]
        public class ProjectReport
        {
            public ProjectReport()
            {
                Months = new List<MonthEntry>();
                Summary = new ProjectSummary();
                Costs = new ProjectCosts();
            }

            [GuiGridLocation(0, 0)]
            public ProjectSummary Summary { get; set; }
            [GuiGridLocation(0, 1)]
            public ProjectCosts Costs { get; set; }
            
            [GuiTitle("Monate")]
            [GuiGridLocation(1, 0, 1, 2)]
            public List<MonthEntry> Months { get; set; }
        }

        [Serializable]
        [GuiTitle("Übersicht")]
        public class ProjectSummary
        {
            public DateTime From { get; set; }
            public DateTime Until { get; set; }

            public decimal Total { get; set; }
            public decimal TotalCorrected { get; set; }
        }

        [Serializable]
        [GuiTitle("Kosten")]
        public class ProjectCosts
        {
            public DateTime From { get; set; }
            public DateTime Until { get; set; }

            public decimal TotalEURO { get; set; }
            public decimal TotalEUROCorrected { get; set; }
        }

        [Serializable]
        [GuiPrintableRoot]
        [GuiTitle("Bericht")]
        [GuiTabbed]
        public class Report
        {
            public Report(DateTime from, DateTime until)
            {
                Project = new ProjectReport()
                {
                    Summary = new ProjectSummary()
                    {
                        Total = 1,
                        TotalCorrected = 2,
                        From = from,
                        Until = until
                    },
                    Costs = new ProjectCosts()
                    {
                        TotalEURO = 1000,
                        TotalEUROCorrected = 2000,
                        From = from,
                        Until = until
                    },
                    Months = { 
                        new MonthEntry() { Month = "01", Year = "2012", Total = 1, TotalCorrected = 10 },
                        new MonthEntry() { Month = "02", Year = "2012", Total = 2, TotalCorrected = 10 },
                        new MonthEntry() { Month = "03", Year = "2012", Total = 3, TotalCorrected = 10 },
                        new MonthEntry() { Month = "04", Year = "2012", Total = 4, TotalCorrected = 10 },
                        new MonthEntry() { Month = "05", Year = "2012", Total = 5, TotalCorrected = 10 },
                        new MonthEntry() { Month = "06", Year = "2012", Total = 6, TotalCorrected = 10 },
                        new MonthEntry() { Month = "07", Year = "2012", Total = 7, TotalCorrected = 10 },
                        new MonthEntry() { Month = "08", Year = "2012", Total = 8, TotalCorrected = 10 },
                        new MonthEntry() { Month = "09", Year = "2012", Total = 9, TotalCorrected = 10 },
                        new MonthEntry() { Month = "00", Year = "2012", Total = 10, TotalCorrected = 10 },
                        new MonthEntry() { Month = "11", Year = "2012", Total = 11, TotalCorrected = 10 },
                        new MonthEntry() { Month = "12", Year = "2012", Total = 12, TotalCorrected = 10 },
                    }
                };
                Task = new TaskReport()
                {
                    Total = 11,
                    TotalCorrected = 12,
                    From = from,
                    Until = until,
                    Months = { 
                        new MonthEntry() { Month = "01", Year = "2012", Total = 11, TotalCorrected = 110 },
                        new MonthEntry() { Month = "02", Year = "2012", Total = 12, TotalCorrected = 110 },
                        new MonthEntry() { Month = "03", Year = "2012", Total = 13, TotalCorrected = 110 },
                        new MonthEntry() { Month = "04", Year = "2012", Total = 14, TotalCorrected = 110 },
                        new MonthEntry() { Month = "05", Year = "2012", Total = 15, TotalCorrected = 110 },
                        new MonthEntry() { Month = "06", Year = "2012", Total = 16, TotalCorrected = 110 },
                        new MonthEntry() { Month = "07", Year = "2012", Total = 17, TotalCorrected = 110 },
                        new MonthEntry() { Month = "08", Year = "2012", Total = 18, TotalCorrected = 110 },
                        new MonthEntry() { Month = "09", Year = "2012", Total = 19, TotalCorrected = 110 },
                        new MonthEntry() { Month = "00", Year = "2012", Total = 110, TotalCorrected = 110 },
                        new MonthEntry() { Month = "11", Year = "2012", Total = 111, TotalCorrected = 110 },
                        new MonthEntry() { Month = "12", Year = "2012", Total = 112, TotalCorrected = 110 },
                    }
                };
    
            }

            public ProjectReport Project { get; set; }
            public TaskReport Task { get; set; }
        }

        protected override object LoadStatistic(DateTime from, DateTime until)
        {
            var rpt = new Report(from, until);
            return rpt;
        }
    }
}
