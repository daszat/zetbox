using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Client.Presentables.Calendar;


namespace Zetbox.Client.Reporting.Calendar
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst")]
    public partial class Events : Zetbox.API.Common.Reporting.ReportTemplate
    {
		protected IEnumerable<EventViewModel> events;
		protected DateTime start;
		protected DateTime end;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IEnumerable<EventViewModel> events, DateTime start, DateTime end)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Calendar.Events", events, start, end);
        }

        public Events(Arebis.CodeGeneration.IGenerationHost _host, IEnumerable<EventViewModel> events, DateTime start, DateTime end)
            : base(_host)
        {
			this.events = events;
			this.start = start;
			this.end = end;

        }

        public override void Generate()
        {
#line 12 "P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\\document  {\r\n");
this.WriteObjects("    \\section [\r\n");
this.WriteObjects("        PageSetup\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            Orientation = Portrait\r\n");
this.WriteObjects("            PageFormat = A4\r\n");
this.WriteObjects("            TopMargin = 40\r\n");
this.WriteObjects("            StartingNumber = 1\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    ] {\r\n");
this.WriteObjects("        \\primaryfooter {\r\n");
this.WriteObjects("            \\table {\r\n");
this.WriteObjects("                \\columns {\r\n");
this.WriteObjects("                    \\column [ Width = \"8cm\" ]\r\n");
this.WriteObjects("                    \\column [ Width = \"8cm\" Format { Alignment = Right }\r\n");
this.WriteObjects("                    ]\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                \\rows {\r\n");
this.WriteObjects("                    \\row {\r\n");
this.WriteObjects("                        \\cell { ",  FormatDate(DateTime.Today) , " }\r\n");
this.WriteObjects("                        \\cell { Seite \\field(Page) von \\field(SectionPages) }\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ Format { Font { Size = 30  Bold = True } } ] { ",  EventsResources.Heading , " }\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("        \\paragraph [ \r\n");
this.WriteObjects("            Style = \"Heading2\"\r\n");
this.WriteObjects("            Format { \r\n");
this.WriteObjects("                Borders { Bottom { Width = 1 } } \r\n");
this.WriteObjects("                SpaceAfter = \"1cm\"\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        ] { ",  string.Format(EventsResources.HeadingSublineFormat, FormatDate(start),FormatDate(end)) , " }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\table [ \r\n");
this.WriteObjects("            Borders { Width = 0.25 Color = 0xFFAAAAAA } \r\n");
this.WriteObjects("        ] {\r\n");
this.WriteObjects("            \\columns {\r\n");
this.WriteObjects("                \\column [ Width = \"5cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"2.5cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("                \\column [ Width = \"2.5cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("                \\column [ Width = \"6cm\" ]\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            \\rows {\r\n");
this.WriteObjects("                \\row [ \r\n");
this.WriteObjects("                    HeadingFormat = True \r\n");
this.WriteObjects("                    Format { Font { Bold = True } }\r\n");
this.WriteObjects("                ] {\r\n");
this.WriteObjects("                    \\cell { Calendar }\r\n");
this.WriteObjects("                    \\cell { Start }\r\n");
this.WriteObjects("                    \\cell { End }\r\n");
this.WriteObjects("                    \\cell { Summary }\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 70 "P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
foreach (var e in events) { 
#line 71 "P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("                \\row {\r\n");
this.WriteObjects("                    \\cell { ",  Format(e.Event.Calendar.Name) , " }\r\n");
this.WriteObjects("                    \\cell { ",  Format(e.Event.StartDate) , " }    \r\n");
this.WriteObjects("                    \\cell { ",  Format(e.Event.EndDate) , " }    \r\n");
this.WriteObjects("                    \\cell { ",  Format(e.Event.Summary) , " }    \r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                \\row {\r\n");
this.WriteObjects("                    \\cell [ Format { Font { Bold = True } } ] { Location }    \r\n");
this.WriteObjects("                    \\cell [  MergeRight = 2 ] { ",  Format(e.Event.Location) , " }    \r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                \\row {\r\n");
this.WriteObjects("                    \\cell [  MergeRight = 3 ] { ",  Format(e.Event.Body) , " }    \r\n");
this.WriteObjects("                }\r\n");
#line 84 "P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
} 
#line 85 "P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    } ");
#line 89 "P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
/* section */
#line 90 "P:\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("}\r\n");

        }

    }
}