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
    using System.Threading.Tasks;
    using Zetbox.App.Base;

    public interface IServer
    {
        Task AnalyzeDatabase(string connectionName, TextWriter output);
        Task CheckSchema(bool withRepair);
        Task CheckSchema(string[] files, bool withRepair);
        Task CheckSchemaFromCurrentMetaData(bool withRepair);
        Task Deploy();
        Task Deploy(params string[] files);
        Task Export(string file, string[] schemaModules, string[] ownerModules);
        Task Import(params string[] files);
        Task Publish(string file, string[] ownerModules);
        Task DeleteModule(string module);
        Task RunBenchmarks();
        Task RunFixes();
        Task SyncIdentities(string source);
        Task UpdateSchema();
        Task UpdateSchema(params string[] files);
        /// <param name="properties">Pass null to recalculate all properties. Or, pass the list properties you want to have recalculated. An empty list, of course, will not process anything.</param>
        Task RecalculateProperties(Property[] properties);
        Task WipeDatabase();
        Task WaitForDatabase();
        Task RefreshRights();
    }
}
