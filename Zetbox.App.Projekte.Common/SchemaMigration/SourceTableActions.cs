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
namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    [Implementor]
    public static class SourceTableActions
    {
        [Invocation]
        public static void ToString(Zetbox.App.SchemaMigration.SourceTable obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = "[" + ((obj.StagingDatabase != null ? obj.StagingDatabase.Description : null) ?? string.Empty) + "]";
            e.Result += "." + (!string.IsNullOrEmpty(obj.Name) ? obj.Name : "new Source Table");
        }

        [Invocation]
        public static void CreateObjectClass(Zetbox.App.SchemaMigration.SourceTable obj)
        {
            if (obj.StagingDatabase == null) throw new InvalidOperationException("Not attached to a staging database");
            if (obj.StagingDatabase.MigrationProject == null) throw new InvalidOperationException("Not attached to a migration project");
            if (obj.StagingDatabase.MigrationProject.DestinationModule == null) throw new InvalidOperationException("No destination module provided");
            if (obj.DestinationObjectClass != null) throw new InvalidOperationException("there is already a destination object class");

            obj.DestinationObjectClass = obj.Context.Create<ObjectClass>();
            obj.DestinationObjectClass.Name = obj.Name;
            obj.DestinationObjectClass.Module = obj.StagingDatabase.MigrationProject.DestinationModule;
            obj.DestinationObjectClass.Description = obj.Description;
        }
    }
}
