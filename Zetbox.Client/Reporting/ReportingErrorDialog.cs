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

namespace Zetbox.Client.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using MigraDoc.DocumentObjectModel.IO;
    using Zetbox.API;
    using Zetbox.API.Common.Reporting;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using ControlKinds = Zetbox.NamedObjects.Gui.ControlKinds;

    [CLSCompliant(false)]
    public class ReportingErrorDialog : IReportingErrorReporter
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IFrozenContext _frozenCtx;
        private readonly Lazy<IZetboxContext> _lazyCtx;

        public ReportingErrorDialog(IViewModelFactory vmFactory, IFrozenContext frozenCtx, Lazy<IZetboxContext> lazyCtx)
        {
            if (vmFactory == null) throw new ArgumentNullException("vmFactory");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (lazyCtx == null) throw new ArgumentNullException("lazyCtx");

            _viewModelFactory = vmFactory;
            _frozenCtx = frozenCtx;
            _lazyCtx = lazyCtx;
        }

        public void ReportErrors(DdlReaderErrors errors, Exception ex, Stream mddl)
        {
            var dlg = _viewModelFactory.CreateDialog(_lazyCtx.Value, "Fehler beim Erstellen des Reports");

            var ctx = _lazyCtx.Value;
            var valueModels = new List<BaseValueViewModel>();

            if (ex != null)
            {
                dlg.AddString("Exception", ex.ToString(), ControlKinds.Zetbox_App_GUI_MultiLineTextboxKind.Find(_frozenCtx));
            }

            if (errors != null && errors.ErrorCount > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (DdlReaderError e in errors)
                {
                    switch (e.ErrorLevel)
                    {
                        case DdlErrorLevel.Error:
                            sb.Append("E: ");
                            break;
                        case DdlErrorLevel.Warning:
                            sb.Append("W: ");
                            break;
                        default:
                            sb.Append("?: ");
                            break;
                    }
                    sb.AppendLine(e.ToString());
                }

                dlg.AddString("Errors", sb.ToString(), ControlKinds.Zetbox_App_GUI_MultiLineTextboxKind.Find(_frozenCtx));
            }

            if (mddl != null)
            {
                mddl.Position = 0;
                StringBuilder sb = new StringBuilder();
                var sr = new StreamReader(mddl);
                int counter = 0;
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    sb.AppendLine(string.Format("{0:000}: {1}", ++counter, line));
                }

                dlg.AddString("MDDL", sb.ToString(), ControlKinds.Zetbox_App_GUI_MultiLineTextboxKind.Find(_frozenCtx));
            }

            dlg.Show();
        }
    }
}
