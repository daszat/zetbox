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

namespace Zetbox.API.Common.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Text;
    using System.Text.RegularExpressions;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.App.Base;

    public class ReportTemplate : CodeTemplate
    {
        public ReportTemplate(IGenerationHost host)
            : base(host)
        {
        }

        [CLSCompliant(false)]
        protected AbstractReportingHost ReportingHost
        {
            get
            {
                return (AbstractReportingHost)Host;
            }
        }

        public override void Generate()
        {
        }

        #region Images
        protected virtual string ResolveTemplateResourceUrl(string template)
        {
            return String.Format("res://{0}/{1}.{2}", Settings["baseReportTemplateAssembly"], Settings["baseReportTemplateNamespace"], template);
        }

        protected virtual string GetResourceImageFile(System.Reflection.Assembly assembly, string image)
        {
            var img = System.Drawing.Bitmap.FromStream(assembly.GetManifestResourceStream(image));
            var result = ReportingHost.TempService.CreateWithExtension(".png");
            img.Save(result, ImageFormat.Png); // Always convert to a PNG
            return result.Replace('\\', '/');
        }

        protected virtual string GetBlobImageFile(Blob image)
        {
            var imageStream = image.GetStream();
            var ext = ".bmp";
            switch (image.MimeType)
            {
                case "image/png":
                    ext = ".png";
                    break;
                case "image/bmp":
                    ext = ".bmp";
                    break;
                case "image/jpg":
                    ext = ".jpg";
                    break;
            }

            using (var tmpFile = File.OpenWrite(ReportingHost.TempService.CreateWithExtension(ext)))
            {
                imageStream.WriteAllTo(tmpFile);
                return tmpFile.Name.Replace('\\', '/');
            }
        }
        #endregion

        #region Formathelper
        protected virtual string Format(object text)
        {
            return text != null ? text.ToString().Replace("\\", "\\\\") : string.Empty;
        }

        protected virtual string FormatTextfield(string text)
        {
            if (text == null) return "";
            else
            {
                string[] lines = Regex.Split(text, "\r\n");
                string formattedText = "";
                foreach (string line in lines)
                {
                    formattedText += " \\linebreak " + line.Replace("\\", "\\\\");
                }

                return formattedText;
            }
        }

        protected virtual string FormatDate(DateTime? dt)
        {
            return dt != null ? FormatDate(dt.Value) : string.Empty;
        }

        protected virtual string FormatDate(DateTime dt)
        {
            return dt.ToShortDateString();
        }

        protected virtual string Today()
        {
            return FormatDate(DateTime.Today);
        }

        protected virtual string FormatTime(DateTime? dt)
        {
            return dt != null ? FormatTime(dt.Value) : string.Empty;
        }

        protected virtual string FormatTime(DateTime dt)
        {
            return dt.ToShortTimeString();
        }

        protected virtual string FormatWeekday(DateTime? dt)
        {
            return dt != null ? FormatWeekday(dt.Value) : string.Empty;
        }

        protected virtual string FormatWeekday(DateTime dt)
        {
            return dt.ToString("dddd");
        }

        protected virtual string FormatDateRange(string von, DateTime? v, string bis, DateTime? b)
        {
            if (v == null)
            {
                if (b == null) return "";
                else return bis + " " + FormatDate(b);
            }
            else
            {
                if (b == null) return von + " " + FormatDate(v);
                else return von + " " + FormatDate(v) + " " + bis + " " + FormatDate(b);
            }
        }

        protected virtual string FormatPercent(float? betrag)
        {
            return betrag.HasValue ? String.Format("{0:n1}%", betrag.Value) : String.Format("{0:n1}%", "0");
        }

        protected virtual string FormatPercent(double? betrag)
        {
            return betrag.HasValue ? String.Format("{0:n1}%", betrag.Value) : String.Format("{0:n1}%", "0");
        }

        protected virtual string FormatEuro(decimal? betrag)
        {
            return betrag.HasValue ? String.Format("{0:n2} €", betrag.Value) : String.Format("{0:n2} €", "0");
        }

        protected virtual string FormatErrorMessage(string message)
        {
            return @"\bold{\fontcolor(red){" + message + "}}";
        }

        protected virtual string FormatNonBreaking(string s)
        {
            return s.Replace(" ", "\u00A0");
        }
        #endregion

        #region ToDo
        protected virtual string ToDo(string message)
        {
            return @"\bold{\fontcolor(red){TODO: " + message + "!}}";
        }
        #endregion
    }
}
