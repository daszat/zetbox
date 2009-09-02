
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.GUI;

    public class ColumnDisplayModel
    {
        public string Header { get; set; }
        public string PropertyName { get; set; }
        public ControlKind ControlKind { get; set; }
    }

    public class GridDisplayConfiguration
    {
        public bool ShowId { get; set; }
        public bool ShowIcon { get; set; }
        public bool ShowName { get; set; }
        public IList<ColumnDisplayModel> Columns { get; set; }
    }
}
