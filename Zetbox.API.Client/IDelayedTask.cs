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
using System.Text;

namespace Zetbox.API.Client
{
    public interface IDelayedTask
    {
        /// <summary>
        /// Trigger one execution of this Task. The execution may be delayed by the implementation by queueing it onto the UI's message pump or similar.
        /// </summary>
        void Trigger();
    }

    public class ImmediateTask : IDelayedTask
    {
        private readonly Action _task;

        public ImmediateTask(Action task)
        {
            if (task == null) throw new ArgumentNullException("task");

            _task = task;
        }

        public void Trigger()
        {
            _task();
        }
    }
}
