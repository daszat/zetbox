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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    
    /// <summary>
    /// Models a <see cref="System.Type"/>. Contains references to the Assembly and the respective TypeRef.
    /// </summary>
    public class SystemTypeViewModel
        : ViewModel
    {
        public new delegate SystemTypeViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Type type);

        private Type _type;

        public SystemTypeViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Type type)
            : base(appCtx, dataCtx, parent)
        {
            _type = type;
        }

        #region Public Interface

        /// <summary>
        /// Returns a direct reference to the wrapped <see cref="Type"/>.
        /// </summary>
        public Type Value { get { return _type; } }

        /// <summary>
        /// Whether or not this Type's Assembly is already in the Database.
        /// If HasAssembly is true, the Assembly property contains the stored 
        /// assembly. if HasAssembly is false, the CreateAssembly command can
        /// construct a new Assembly.
        /// </summary>
        public bool HasAssembly { get { return this.Assembly != null; } }

        /// <summary>
        /// The Assembly containing this Type. MAY be null, see HasAssembly.
        /// </summary>
        public AssemblyViewModel Assembly { get; private set; }

        /// <summary>
        /// If the Assembly containing this Type is not yet stored in the data 
        /// store, this command can construct it.
        /// </summary>
        public ICommandViewModel CreateAssembly { get; private set; }

        /// <inheritdoc />
        public override string Name
        {
            get { return Value.FullName; }
        }

        #endregion
    }
}
