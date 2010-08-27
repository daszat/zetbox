using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;

namespace Kistl.Client.Presentables.SchemaMigration
{
    [ViewModelDescriptor("SchemaMigration", DefaultKind = "Kistl.App.GUI.ObjectCollectionKind", Description = "ViewModel for SourceTable.SourceColumns object collection")]
    public class SourceColumnsObjectListModel : ObjectCollectionModel
    {
        public new delegate SourceColumnsObjectListModel Factory(IKistlContext dataCtx, IDataObject obj, Property prop);

        public SourceColumnsObjectListModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject obj, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {

        }

        protected override GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = base.DisplayedColumns;
            // Add create property button
            result.Columns.Add(new ColumnDisplayModel()
                {
                    IsMethod = true,
                    Header = "Create",
                    Name = "CreateProperty",
                    ControlKind = FrozenContext.FindPersistenceObject<ControlKind>(new Guid("364aaaf3-0f61-4642-a0e3-bd812935cf44")) // Kistl.App.GUI.ActionKind
                });
            return result;
        }
    }
}
