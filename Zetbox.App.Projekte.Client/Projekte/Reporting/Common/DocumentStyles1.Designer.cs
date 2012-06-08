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
using System;
using System.Collections.Generic;
using System.Linq;


namespace Zetbox.App.Projekte.Client.Projekte.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\Common\DocumentStyles.cst")]
    public partial class DocumentStyles : Zetbox.App.Projekte.Client.Projekte.Reporting.ReportTemplate
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.DocumentStyles");
        }

        public DocumentStyles(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 8 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\Common\DocumentStyles.cst"
this.WriteObjects("  \\styles {\r\n");
this.WriteObjects("    Normal {\r\n");
this.WriteObjects("      Font { Name = \"Verdana\" Size = 10 }\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        SpaceBefore = 6\r\n");
this.WriteObjects("        SpaceAfter = 6\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Compact : Normal {\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        SpaceBefore = \"0.07cm\"\r\n");
this.WriteObjects("        SpaceAfter = \"0.07cm\"\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Title : Normal {\r\n");
this.WriteObjects("      Font { Size = 16 Bold = true }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("	\r\n");
this.WriteObjects("    Heading1 {\r\n");
this.WriteObjects("      Font { Size = 14 Bold = true }\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("		SpaceBefore = 6\r\n");
this.WriteObjects("        SpaceAfter = 6\r\n");
this.WriteObjects("        PageBreakBefore = false\r\n");
this.WriteObjects("        OutlineLevel = Level1\r\n");
this.WriteObjects("		KeepWithNext = True\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Heading2 {\r\n");
this.WriteObjects("      Font { Size = 12 Bold = true }\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        SpaceBefore = 6\r\n");
this.WriteObjects("		SpaceAfter = 3\r\n");
this.WriteObjects("        OutlineLevel = Level2\r\n");
this.WriteObjects("		KeepWithNext = True\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Heading3 {\r\n");
this.WriteObjects("      Font { Size = 11 Bold = true }\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        SpaceAfter = 3\r\n");
this.WriteObjects("        OutlineLevel = Level3\r\n");
this.WriteObjects("		KeepWithNext = True\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Heading4 {\r\n");
this.WriteObjects("      Font { Size = 10 Bold = true }\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        SpaceAfter = 2\r\n");
this.WriteObjects("        OutlineLevel = Level4\r\n");
this.WriteObjects("		KeepWithNext = True\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Heading5 { ParagraphFormat { OutlineLevel = Level5 } }\r\n");
this.WriteObjects("	Heading6 { ParagraphFormat { OutlineLevel = Level6 } }\r\n");
this.WriteObjects("	Heading7 { ParagraphFormat { OutlineLevel = Level7 } }\r\n");
this.WriteObjects("	Heading8 { ParagraphFormat { OutlineLevel = Level8 } }\r\n");
this.WriteObjects("	Heading9 { ParagraphFormat { OutlineLevel = Level9 } }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Header {\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        TabStops += {\r\n");
this.WriteObjects("          Position = \"16cm\"\r\n");
this.WriteObjects("          Alignment = Right\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    Footer {\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        TabStops += {\r\n");
this.WriteObjects("          Position = \"8cm\"\r\n");
this.WriteObjects("          Alignment = Center\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    InvalidStyleName {\r\n");
this.WriteObjects("      Font { Bold = true Underline = Dash Color = Red }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    TextBox : Normal {\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        Alignment = Justify\r\n");
this.WriteObjects("        Borders\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("          Width = 2.5\r\n");
this.WriteObjects("          DistanceFromTop = 3\r\n");
this.WriteObjects("          DistanceFromBottom = 3\r\n");
this.WriteObjects("          DistanceFromLeft = 3\r\n");
this.WriteObjects("          DistanceFromRight = 3\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        Shading { Color = SkyBlue }\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    TOC : Normal {\r\n");
this.WriteObjects("      Font { }\r\n");
this.WriteObjects("      ParagraphFormat {\r\n");
this.WriteObjects("        TabStops += {\r\n");
this.WriteObjects("          Position = \"16cm\"\r\n");
this.WriteObjects("          Alignment = Right\r\n");
this.WriteObjects("          Leader = Dots\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("      }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("  }");

        }

    }
}