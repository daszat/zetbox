using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Client.Presentables.Calendar;


namespace Zetbox.Client.Reporting.Calendar
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst")]
    public partial class SheetDays : Zetbox.API.Common.Reporting.ReportTemplate
    {
		protected IEnumerable<CalendarItemViewModel> events;
		protected DateTime start;
		protected DateTime end;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IEnumerable<CalendarItemViewModel> events, DateTime start, DateTime end)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Calendar.SheetDays", events, start, end);
        }

        public SheetDays(Arebis.CodeGeneration.IGenerationHost _host, IEnumerable<CalendarItemViewModel> events, DateTime start, DateTime end)
            : base(_host)
        {
			this.events = events;
			this.start = start;
			this.end = end;

        }

        public override void Generate()
        {
#line 12 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\\document  {\r\n");
#line 15 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
var current = start;
    while(current <= end) {
        var lst = events.Where(e => e.From.Date == current);

#line 19 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
this.WriteObjects("    \\section [\r\n");
this.WriteObjects("        PageSetup\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            Orientation = Landscape\r\n");
this.WriteObjects("            PageFormat = A4\r\n");
this.WriteObjects("            TopMargin = \"1cm\"\r\n");
this.WriteObjects("            LeftMargin = \"1cm\"\r\n");
this.WriteObjects("            BottomMargin = \"1cm\"\r\n");
this.WriteObjects("            RightMargin = \"1cm\"\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    ] {\r\n");
this.WriteObjects("        \\table [ \r\n");
this.WriteObjects("            Borders { Width = 0.25 Color = 0xFFAAAAAA } \r\n");
this.WriteObjects("        ] {\r\n");
this.WriteObjects("            \\columns {\r\n");
this.WriteObjects("                \\column [ Width = \"13cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"1.5cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"11.5cm\" ]\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            \\rows {\r\n");
this.WriteObjects("                \\row [ Height = \"1.2cm\" ] {\r\n");
this.WriteObjects("                    \\cell [ MergeDown = 25 ] { Notizen }\r\n");
this.WriteObjects("                    \\cell [ \r\n");
this.WriteObjects("                        MergeRight = 1 \r\n");
this.WriteObjects("                        Format { Font { Bold = True Size = 20 } }                         \r\n");
this.WriteObjects("                    ] { ",  current.ToString("dddd, dd MMMM yyyy") , " }\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                \\row [ Height = \"3cm\" ] {\r\n");
this.WriteObjects("                    \\cell { }\r\n");
this.WriteObjects("                    \\cell [ Borders { Right { Width = 0 } } ] { }\r\n");
this.WriteObjects("                    \\cell [ Borders { Left  { Width = 0 } } ] { ",  string.Join(" \\linebreak ", lst.Where(e => e.IsAllDay).Select(e => Format(e.Summary))) , " }\r\n");
this.WriteObjects("                }\r\n");
#line 51 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
for(int hour = 0; hour < 24; hour++) { 
#line 52 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
this.WriteObjects("                \\row [ \r\n");
this.WriteObjects("                    Height = \"0.6cm\" \r\n");
this.WriteObjects("                    VerticalAlignment = Center\r\n");
this.WriteObjects("                ] {\r\n");
this.WriteObjects("                    \\cell { }\r\n");
this.WriteObjects("                    \\cell [ Borders { Right { Width = 0 } } ] { ",  string.Format("{0:00}:00", hour) , " }\r\n");
this.WriteObjects("                    \\cell [ Borders { Left  { Width = 0 } } ] { ",  string.Join(", ", lst.Where(e => !e.IsAllDay && e.From.Hour == hour).Select(e => Format(e.Summary))) , " }\r\n");
this.WriteObjects("                }\r\n");
#line 60 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
} 
#line 61 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
#line 65 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
current = current.AddDays(1);
    } 
#line 67 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\SheetDays.cst"
this.WriteObjects("}");

        }

    }
}