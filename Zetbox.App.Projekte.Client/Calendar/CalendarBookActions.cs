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
namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.Client.Presentables.Calendar;
    using Zetbox.API.Common;
    using Zetbox.App.Base;
    using System.Threading.Tasks;

    [Implementor]
    public class CalendarBookActions
    {
        private static IPrincipalResolver _principalResolver = null;
        public CalendarBookActions(IPrincipalResolver principalResolver)
        {
            _principalResolver = principalResolver;
        }

        [Invocation]
        public static async Task NotifyCreated(CalendarBook obj)
        {
            var ctx = obj.Context;
            // sets the current principal as the default owner
            var principal = await _principalResolver.GetCurrent();
            obj.Owner = principal != null ? ctx.Find<Identity>(principal.ID) : null;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetNewEventViewModels(CalendarBook obj, object /* I'm so sorry, Zetbox.Objects.dll cannot use custom classes */ args)
        {
            var eventArgs = (NewEventViewModelsArgs)args;
            eventArgs.ViewModels.Insert(0, eventArgs.ViewModelFactory.CreateViewModel<EventInputViewModel.Factory>()
                .Invoke(eventArgs.DataContext, eventArgs.Parent, obj, eventArgs.SelectedStartDate, eventArgs.IsAllDay));

            // For testing only!
            //eventArgs.ViewModels.Add(eventArgs.ViewModelFactory.CreateViewModel<Zetbox.App.Projekte.Client.ViewModel.TestModule.EventTestInputViewModel.Factory>()
            //    .Invoke(eventArgs.DataContext, eventArgs.Parent, obj, eventArgs.SelectedStartDate, eventArgs.IsAllDay));

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
