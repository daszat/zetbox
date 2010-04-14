using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using QuickGraph;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public class DiagramViewModel : ViewModel
    {
        public new delegate DiagramViewModel Factory(IKistlContext dataCtx, Module module);

        public DiagramViewModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, Module module)
            : base(appCtx, dataCtx)
        {
            this.module = module;
        }

        #region GraphSettings
        public enum GraphTypeEnum
        {
            Relation,
            Inheritance,
        }

        private GraphTypeEnum _graphType = GraphTypeEnum.Inheritance;
        public GraphTypeEnum GraphType
        {
            get
            {
                return _graphType;
            }
            set
            {
                if (_graphType != value)
                {
                    _graphType = value;
                    OnPropertyChanged("GrapType");
                    RecreateGraph();
                }
            }
        }

        public IEnumerable<GraphTypeEnum> GraphTypes
        {
            get
            {
                return new GraphTypeEnum[] { GraphTypeEnum.Relation, GraphTypeEnum.Inheritance };
            }
        }
        #endregion

        #region ViewModel
        protected Module module;

        public override string Name
        {
            get { return "Class Diagram"; }
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region DataTypes
        public class DataTypeModel : Presentables.DataObjectModel
        {
            private DiagramViewModel _parent;

            public DataTypeModel(IGuiApplicationContext appCtx, IKistlContext dataCtx,
                DataType obj, DiagramViewModel parent)
                : base(appCtx, dataCtx, obj)
            {
                _parent = parent;
                DataType = obj as DataType;
            }

            public DataType DataType { get; private set; }

            private bool _isChecked = false;
            public bool IsChecked
            {
                get
                {
                    return _isChecked;
                }
                set
                {
                    SetChecked(value, true);
                }
            }

            internal void SetChecked(bool chk, bool recreateGraph)
            {
                if (_isChecked != chk)
                {
                    _isChecked = chk;
                    if (recreateGraph) _parent.RecreateGraph();
                    OnPropertyChanged("IsChecked");
                }
            }
        }

        private List<DataTypeModel> _dataTypes = null;
        public IEnumerable<DataTypeModel> DataTypes
        {
            get
            {
                if (_dataTypes == null)
                {
                    _dataTypes = DataContext.GetQuery<DataType>().Where(i => i.Module == module).ToList()
                        .Select(i => new DataTypeModel(AppContext, DataContext, i, this)).OrderBy(i => i.Name).ToList();
                }
                return _dataTypes;
            }
        }

        private List<Relation> _relations = null;
        public IEnumerable<Relation> Relations
        {
            get
            {
                if (_relations == null)
                {
                    _relations = DataContext.GetQuery<Relation>().Where(i => i.Module == module).ToList();
                }
                return _relations;
            }
        }

        private IEnumerable<DataType> SelectedDataTypes
        {
            get
            {
                return _dataTypes.Where(i => i.IsChecked).Select(i => i.DataType);
            }
        }

        private void SelectAllDataTypes()
        {
            DataTypes.ForEach(i => i.SetChecked(true, false));
            RecreateGraph();
        }

        private void SelectNoDataTypes()
        {
            DataTypes.ForEach(i => i.SetChecked(false, false));
            RecreateGraph();
        }

        private void AddRelatedDataTypes()
        {
            foreach (var dtm in DataTypes.Where(i => i.IsChecked).ToList())
            {
                var add = new List<DataTypeModel>();
                if (GraphType == GraphTypeEnum.Inheritance)
                {
                    // Add BaseClass
                    if(dtm.DataType is ObjectClass && ((ObjectClass)dtm.DataType).BaseObjectClass != null)
                    {
                        var item = DataTypes.FirstOrDefault(i => i.DataType == ((ObjectClass)dtm.DataType).BaseObjectClass);
                        if(item != null) add.Add(item);
                    }

                    // Add Inheritance
                    add.AddRange(DataTypes.Where(i => i.DataType is ObjectClass && ((ObjectClass)i.DataType).BaseObjectClass == dtm.DataType));
                    
                }
                else if (GraphType == GraphTypeEnum.Relation)
                {
                    foreach (var rel in Relations.Where(i => i.A.Type == dtm.DataType || i.B.Type == dtm.DataType))
                    {
                        var a = DataTypes.FirstOrDefault(i => i.DataType == rel.A.Type);
                        if (a != null) add.Add(a);
                        var b = DataTypes.FirstOrDefault(i => i.DataType == rel.B.Type);
                        if (b != null) add.Add(b);
                    }
                }

                // checked = true
                add.ForEach(i => i.SetChecked(true, false));
            }

            RecreateGraph();
        }
        #endregion

        #region Graph
        private void CreateGraph()
        {
            var g = new BidirectionalGraph<object, IEdge<object>>();

            if (SelectedDataTypes.Count() == 0)
            {
                g.AddVertex("Nothing selected");
            }
            else
            {
                foreach (var dt in SelectedDataTypes)
                {
                    g.AddVertex(dt);
                }

                if (GraphType == GraphTypeEnum.Relation)
                {
                    foreach (var rel in Relations)
                    {
                        if (g.ContainsVertex(rel.A.Type) && g.ContainsVertex(rel.B.Type))
                        {
                            g.AddEdge(new TaggedEdge<object, Relation>(rel.A.Type, rel.B.Type, rel));
                        }
                    }
                }
                else if (GraphType == GraphTypeEnum.Inheritance)
                {
                    foreach (var cls in SelectedDataTypes.OfType<ObjectClass>())
                    {
                        if (cls.BaseObjectClass != null && g.ContainsVertex(cls) && g.ContainsVertex(cls.BaseObjectClass))
                        {
                            g.AddEdge(new TaggedEdge<object, ObjectClass>(cls, cls.BaseObjectClass, cls));
                        }
                    }
                }
            }
            _graph = g;
        }

        protected void RecreateGraph()
        {
            _graph = null;
            OnPropertyChanged("Graph");
        }

        private IBidirectionalGraph<object, IEdge<object>> _graph;
        public object Graph
        {
            get
            {
                if (_graph == null)
                {
                    CreateGraph();
                }
                return _graph;
            }
        }

        #endregion

        #region Commands
        private SelectAllCommandModel _selectAllCommand = null;
        public ICommand SelectAllCommand
        {
            get
            {
                if (_selectAllCommand == null)
                {
                    _selectAllCommand = new SelectAllCommandModel(AppContext, DataContext, this);
                }
                return _selectAllCommand;
            }
        }
        internal class SelectAllCommandModel : CommandModel
        {
            private DiagramViewModel _parent;

            public SelectAllCommandModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, DiagramViewModel parent)
                : base(appCtx, dataCtx, "Select All", "Selects all DataTypes")
            {
                _parent = parent;
            }

            public override bool CanExecute(object data)
            {
                return true;
            }

            protected override void DoExecute(object data)
            {
                _parent.SelectAllDataTypes();
            }
        }

        private SelectNoneCommandModel _selectNoneCommand = null;
        public ICommand SelectNoneCommand
        {
            get
            {
                if (_selectNoneCommand == null)
                {
                    _selectNoneCommand = new SelectNoneCommandModel(AppContext, DataContext, this);
                }
                return _selectNoneCommand;
            }
        }
        internal class SelectNoneCommandModel : CommandModel
        {
            private DiagramViewModel _parent;

            public SelectNoneCommandModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, DiagramViewModel parent)
                : base(appCtx, dataCtx, "Select None", "Selects no DataTypes")
            {
                _parent = parent;
            }

            public override bool CanExecute(object data)
            {
                return true;
            }

            protected override void DoExecute(object data)
            {
                _parent.SelectNoDataTypes();
            }
        }

        private AddRelatedCommandModel _addRelatedCommand = null;
        public ICommand AddRelatedCommand
        {
            get
            {
                if (_addRelatedCommand == null)
                {
                    _addRelatedCommand = new AddRelatedCommandModel(AppContext, DataContext, this);
                }
                return _addRelatedCommand;
            }
        }
        internal class AddRelatedCommandModel : CommandModel
        {
            private DiagramViewModel _parent;

            public AddRelatedCommandModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, DiagramViewModel parent)
                : base(appCtx, dataCtx, "Select None", "Selects no DataTypes")
            {
                _parent = parent;
            }

            public override bool CanExecute(object data)
            {
                return true;
            }

            protected override void DoExecute(object data)
            {
                _parent.AddRelatedDataTypes();
            }
        }
        #endregion
    }
}
