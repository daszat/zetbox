
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.GUI;

    public class ColumnDisplayModel
    {
        public ColumnDisplayModel()
        {
            IsMethod = false;
        }

        public bool IsMethod { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }
        public ControlKind ControlKind { get; set; }

        public override string ToString()
        {
            return Header;
        }
    }

    public class GridDisplayConfiguration
    {
        public bool ShowId { get; set; }
        public bool ShowIcon { get; set; }
        public bool ShowName { get; set; }
        public IList<ColumnDisplayModel> Columns { get; set; }
    }
}
