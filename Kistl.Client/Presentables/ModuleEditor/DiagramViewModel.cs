using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using QuickGraph;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using ObjectEditorWorkspace = Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel;

namespace Kistl.Client.Presentables.ModuleEditor
{
    [CLSCompliant(false)]
    public class DataTypeGraph : BidirectionalGraph<DataTypeGraphModel, IEdge<DataTypeGraphModel>>
    {
        public DataTypeGraph() { }

        public DataTypeGraph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public DataTypeGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }
    }

    public class DataTypeGraphModel : Presentables.DataTypeModel
    {
        public new delegate DataTypeGraphModel Factory(IKistlContext dataCtx, DataType obj, DiagramViewModel parent);

        private DiagramViewModel _diagMdl;
        protected readonly Func<IKistlContext> ctxFactory;

        public DataTypeGraphModel(IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            DataType obj, DiagramViewModel parent, Func<IKistlContext> ctxFactory)
            : base(appCtx, config, dataCtx, obj)
        {
            this._diagMdl = parent;
            this.ctxFactory = ctxFactory;
            this.DataType = obj;
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
                if (recreateGraph) _diagMdl.RecreateGraph();
                OnPropertyChanged("IsChecked");
            }
        }

        private ICommand _open = null;
        public ICommand Open
        {
            get
            {
                if (_open == null)
                {
                    _open = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(
                        DataContext, "Open", "Opens the current DataType", () =>
                        {
                            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(ctxFactory());
                            newWorkspace.ShowForeignModel(this);
                            ModelFactory.ShowModel(newWorkspace, true);
                        }, null);
                }

                return _open;
            }
        }
    }


    public class DiagramViewModel : ViewModel
    {
        public new delegate DiagramViewModel Factory(IKistlContext dataCtx, Module module);

        public DiagramViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, Module module)
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

        private List<DataTypeGraphModel> _dataTypes = null;
        public IEnumerable<DataTypeGraphModel> DataTypes
        {
            get
            {
                if (_dataTypes == null)
                {
                    _dataTypes = DataContext.GetQuery<DataType>().Where(i => i.Module == module).ToList()
                        .Select(i => ModelFactory.CreateViewModel<DataTypeGraphModel.Factory>().Invoke(DataContext, i, this)).OrderBy(i => i.Name).ToList();
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

        private IEnumerable<DataTypeGraphModel> SelectedDataTypeModels
        {
            get
            {
                return _dataTypes.Where(i => i.IsChecked);
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
                var add = new List<DataTypeGraphModel>();
                if (GraphType == GraphTypeEnum.Inheritance)
                {
                    // Add BaseClass
                    if (dtm.DataType is ObjectClass && ((ObjectClass)dtm.DataType).BaseObjectClass != null)
                    {
                        var item = DataTypes.FirstOrDefault(i => i.DataType == ((ObjectClass)dtm.DataType).BaseObjectClass);
                        if (item != null) add.Add(item);
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
            var g = new DataTypeGraph(true);

            if (SelectedDataTypeModels.Count() == 0)
            {
                _graph = null;
                return;
            }

            Dictionary<DataType, DataTypeGraphModel> typeMdlDict = new Dictionary<DataType, DataTypeGraphModel>();
            foreach (var dt in SelectedDataTypeModels)
            {
                g.AddVertex(dt);
                typeMdlDict[dt.DataType] = dt;
            }

            if (GraphType == GraphTypeEnum.Relation)
            {
                foreach (var rel in Relations)
                {
                    if (typeMdlDict.ContainsKey(rel.A.Type) && typeMdlDict.ContainsKey(rel.B.Type))
                    {
                        g.AddEdge(new TaggedEdge<DataTypeGraphModel, Relation>(typeMdlDict[rel.A.Type], typeMdlDict[rel.B.Type], rel));
                    }
                }
            }
            else if (GraphType == GraphTypeEnum.Inheritance)
            {
                foreach (var cls in SelectedDataTypeModels.Select(i => i.DataType).OfType<ObjectClass>())
                {
                    if (cls.BaseObjectClass != null && typeMdlDict.ContainsKey(cls) && typeMdlDict.ContainsKey(cls.BaseObjectClass))
                    {
                        g.AddEdge(new TaggedEdge<DataTypeGraphModel, ObjectClass>(typeMdlDict[cls], typeMdlDict[cls.BaseObjectClass], cls));
                    }
                }
            }

            _graph = g;
        }

        internal void RecreateGraph()
        {
            _graph = null;
            OnPropertyChanged("Graph");
        }

        private DataTypeGraph _graph;
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
                    _selectAllCommand = ModelFactory.CreateViewModel<SelectAllCommandModel.Factory>().Invoke(DataContext, this);
                }
                return _selectAllCommand;
            }
        }
        internal class SelectAllCommandModel : CommandModel
        {
            public new delegate SelectAllCommandModel Factory(IKistlContext dataCtx, DiagramViewModel parent);

            private DiagramViewModel _parent;

            public SelectAllCommandModel(IViewModelDependencies appCtx, IKistlContext dataCtx, DiagramViewModel parent)
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
                    _selectNoneCommand = ModelFactory.CreateViewModel<SelectNoneCommandModel.Factory>().Invoke(DataContext, this);
                }
                return _selectNoneCommand;
            }
        }
        internal class SelectNoneCommandModel : CommandModel
        {
            public new delegate SelectNoneCommandModel Factory(IKistlContext dataCtx, DiagramViewModel parent);

            private DiagramViewModel _parent;

            public SelectNoneCommandModel(IViewModelDependencies appCtx, IKistlContext dataCtx, DiagramViewModel parent)
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
                    _addRelatedCommand = ModelFactory.CreateViewModel<AddRelatedCommandModel.Factory>().Invoke(DataContext, this);
                }
                return _addRelatedCommand;
            }
        }
        internal class AddRelatedCommandModel : CommandModel
        {
            public new delegate AddRelatedCommandModel Factory(IKistlContext dataCtx, DiagramViewModel parent);

            private DiagramViewModel _parent;

            public AddRelatedCommandModel(IViewModelDependencies appCtx, IKistlContext dataCtx, DiagramViewModel parent)
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
