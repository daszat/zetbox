using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.Models;

namespace Kistl.Client.Presentables.SchemaMigration
{
    [ViewModelDescriptor("SchemaMigration", DefaultKind = "Kistl.App.GUI.ObjectCollectionKind", Description = "ViewModel for SourceTable.SourceColumns object collection")]
    public class SourceColumnsObjectListModel : ObjectCollectionViewModel
    {
        public new delegate SourceColumnsObjectListModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public SourceColumnsObjectListModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {

        }

        protected override GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = base.CreateDisplayedColumns();
            // Add create property button
            result.Columns.Add(new ColumnDisplayModel()
                {
                    Type = ColumnDisplayModel.ColumnType.MethodModel,
                    Header = "Create",
                    Name = "CreateProperty",
                    ControlKind = FrozenContext.FindPersistenceObject<ControlKind>(new Guid("364aaaf3-0f61-4642-a0e3-bd812935cf44")) // Kistl.App.GUI.ActionKind
                });
            return result;
        }
    }
}
