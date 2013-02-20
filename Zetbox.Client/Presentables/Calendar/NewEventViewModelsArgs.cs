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
    using System.Linq;
    using System.Text;
    using cal = Zetbox.App.Calendar;
using Zetbox.API;

    public class NewEventViewModelsArgs
    {
        public NewEventViewModelsArgs(IZetboxContext ctx, IViewModelFactory viewModelFactory, ViewModel parent, cal.Calendar calendar, DateTime selectedStartDate)
        {
            this.DataContext = ctx;
            this.ViewModelFactory = viewModelFactory;
            this.Parent = parent;
            this.Calendar = calendar;
            this.SelectedStartDate = selectedStartDate;

            this.ViewModels = new List<IEventInputViewModel>();
        }

        public IZetboxContext DataContext {get; private set;}
        public IViewModelFactory ViewModelFactory { get; private set; }
        public ViewModel Parent { get; private set; }
        public DateTime SelectedStartDate { get; private set; }
        public cal.Calendar Calendar { get; private set; }

        public List<IEventInputViewModel> ViewModels { get; private set; }

    }
}
