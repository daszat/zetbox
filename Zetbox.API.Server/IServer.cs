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
namespace Zetbox.API.Server
{
    using System;
    using System.IO;
    using Zetbox.App.Base;

    public interface IServer
    {
        void AnalyzeDatabase(string connectionName, TextWriter output);
        void CheckSchema(bool withRepair);
        void CheckSchema(string[] files, bool withRepair);
        void CheckSchemaFromCurrentMetaData(bool withRepair);
        void Deploy();
        void Deploy(params string[] files);
        void Export(string file, string[] schemaModules, string[] ownerModules);
        void Import(params string[] files);
        void Publish(string file, string[] ownerModules);
        void RunBenchmarks();
        void RunFixes();
        void SyncIdentities();
        void UpdateSchema();
        void UpdateSchema(params string[] files);
        /// <param name="properties">Pass null to recalculate all properties. Or, pass the list properties you want to have recalculated. An empty list, of course, will not process anything.</param>
        void RecalculateProperties(Property[] properties);
        void WipeDatabase();
    }
}
