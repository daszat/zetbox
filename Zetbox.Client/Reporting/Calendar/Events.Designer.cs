using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Client.Presentables.Calendar;


namespace Zetbox.Client.Reporting.Calendar
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst")]
    public partial class Events : Zetbox.API.Common.Reporting.ReportTemplate
    {
		protected IEnumerable<CalendarItemViewModel> events;
		protected DateTime start;
		protected DateTime end;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IEnumerable<CalendarItemViewModel> events, DateTime start, DateTime end)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Calendar.Events", events, start, end);
        }

        public Events(Arebis.CodeGeneration.IGenerationHost _host, IEnumerable<CalendarItemViewModel> events, DateTime start, DateTime end)
            : base(_host)
        {
			this.events = events;
			this.start = start;
			this.end = end;

        }

        public override void Generate()
        {
#line 12 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
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
this.WriteObjects("                        \\cell { ",  EventsResources.Page , " \\field(Page)/\\field(SectionPages) }\r\n");
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
this.WriteObjects("            }\r\n");
this.WriteObjects("        ] { ",  string.Format(EventsResources.HeadingSublineFormat, FormatDate(start),FormatDate(end)) , " }\r\n");
this.WriteObjects("\r\n");
#line 49 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
foreach (var grp in events.GroupBy(k => k.From.Date).OrderBy(g => g.Key)) { 
#line 50 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("        \\paragraph { }\r\n");
this.WriteObjects("        \\table [ \r\n");
this.WriteObjects("            Borders { Width = 0.25 Color = 0xFFAAAAAA } \r\n");
this.WriteObjects("        ] {\r\n");
this.WriteObjects("            \\columns {\r\n");
this.WriteObjects("                \\column [ Width = \"3cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"3cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"10cm\" ]\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            \\rows {\r\n");
this.WriteObjects("                \\row [ \r\n");
this.WriteObjects("                    HeadingFormat = True\r\n");
this.WriteObjects("                    Style = \"Heading2\"\r\n");
this.WriteObjects("                    Format { Font { Bold = True Size = 14 } } \r\n");
this.WriteObjects("                    Shading { Color = RGB(235,235,235) }\r\n");
this.WriteObjects("                ] {\r\n");
this.WriteObjects("                    \\cell [ MergeRight = 2 ] { ",  FormatLongDate(grp.Key) , " }\r\n");
this.WriteObjects("                }\r\n");
#line 68 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
foreach (var e in grp.OrderByDescending(g => g.IsAllDay).ThenBy(g => g.From)) { 
#line 69 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("                \\row {\r\n");
this.WriteObjects("                    \\cell { \r\n");
this.WriteObjects("                        ");
#line 71 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
if(!e.IsAllDay) { 
#line 72 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("                            \\bold{ ",  FormatTime(e.From) , " - ",  FormatTime(e.Until) , " }\r\n");
this.WriteObjects("                        ");
#line 73 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
} 
#line 73 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects(" \r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                    \\cell { ",  Format(e.Event.Calendar.Name) , " }\r\n");
this.WriteObjects("                    \\cell { \r\n");
this.WriteObjects("                        \\bold{ ",  Format(e.Event.Summary) , " }\r\n");
this.WriteObjects("                        ");
#line 78 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
if(!string.IsNullOrWhiteSpace(e.Event.Location)) { 
#line 78 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects(" \\linebreak (",  Format(e.Event.Location) , ") ");
#line 78 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
} 
#line 79 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("                        ");
#line 79 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
if(!string.IsNullOrWhiteSpace(e.Event.Body)) { 
#line 79 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects(" \\linebreak ",  FormatTextfield(e.Event.Body) , " ");
#line 79 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
} 
#line 80 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("                    }    \r\n");
this.WriteObjects("                }\r\n");
#line 82 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
} 
#line 83 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 85 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
} 
#line 86 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    } ");
#line 87 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
/* section */
#line 88 "C:\Projects\zetbox\Zetbox.Client\Reporting\Calendar\Events.cst"
this.WriteObjects("}\r\n");

        }

    }
}