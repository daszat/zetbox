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
        public DiagramViewModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, Module module)
            : base(appCtx, dataCtx)
        {
            this.module = module;
        }

        protected Module module;

        public override string Name
        {
            get { return "Class Diagram"; }
        }

        private IBidirectionalGraph<object, IEdge<object>> _graph;
        public object Graph
        {
            get
            {
                if (_graph == null)
                {
                    CreateGraphToVisualize();
                }
                return _graph;
            }
        }

        private void CreateGraphToVisualize()
        {
            var g = new BidirectionalGraph<object, IEdge<object>>();

            foreach (var dt in DataContext.GetQuery<DataType>().Where(i => i.Module == module))
            {
                g.AddVertex(dt);
            }

            foreach (var rel in DataContext.GetQuery<Relation>().Where(i => i.Module == module))
            {
                if (rel.A.Type.Module.ID == module.ID && rel.B.Type.Module.ID == module.ID)
                {
                    g.AddEdge(new TaggedEdge<object, Relation>(rel.A.Type, rel.B.Type, rel));
                }
            }
            _graph = g;
        }
    }
}
