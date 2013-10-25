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
namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Calendar;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;

    [ViewModelDescriptor]
    public class WorkScheduleRuleInstanceViewModel : ViewModel
    {
        public new delegate WorkScheduleRuleInstanceViewModel Factory(IZetboxContext dataCtx, ViewModel parent, WorkScheduleRule rule, DateTime dt);

        public WorkScheduleRuleInstanceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, WorkScheduleRule rule, DateTime dt)
            : base(appCtx, dataCtx, parent)
        {
            this.Rule = rule;
            this.Date = dt.Date;
        }

        public WorkScheduleRule Rule { get; private set; }
        public DateTime Date { get; private set; }

        public override string Name
        {
            get { return Rule.Name; }
        }
    }
}
