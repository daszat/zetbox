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

namespace Zetbox.API.Common.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SavedListConfigurationList
    {
        public SavedListConfigurationList()
        {
            Configs = new List<SavedListConfig>();
        }

        public List<SavedListConfig> Configs { get; set; }
    }

    public class SavedListConfig
    {
        public SavedListConfig()
        {
            Filter = new List<FilterConfig>();
            Columns = new List<ColumnConfig>();
        }

        public string Name { get; set; }

        public List<FilterConfig> Filter { get; set; }
        public List<ColumnConfig> Columns { get; set; }

        public class FilterConfig
        {
            public Guid[] Properties { get; set; }
            public object[] Values { get; set; }
            public bool IsUserFilter { get; set; }
            public string Expression { get; set; }
        }

        public class ColumnConfig
        {
            public int Type { get; set; }
            public string Header { get; set; }

            public Guid[] Properties { get; set; }
            public Guid? Method { get; set; }
            public string Path { get; set; }

            public Guid? ControlKind { get; set; }
            public Guid? GridPreEditKind { get; set; }

            public int Width { get; set; }
        }
    }
}
