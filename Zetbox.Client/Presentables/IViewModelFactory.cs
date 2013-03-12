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

namespace Zetbox.Client.Presentables
{
    using System;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;

    public interface IViewModelFactory : IToolkit
    {
        void ShowModel(ViewModel mdl, bool activate);
        void ShowModel(ViewModel mdl, Zetbox.App.GUI.ControlKind kind, bool activate);

        void ShowDialog(ViewModel mdl);
        void ShowDialog(ViewModel mdl, Zetbox.App.GUI.ControlKind kind);

        bool CanShowModel(ViewModel mdl);

        ViewModel GetWorkspace(IZetboxContext ctx);

        /// <summary>
        /// Initializes the requested culture from config on the current thread
        /// </summary>
        void InitCulture();

        // Create Models
        TModelFactory CreateViewModel<TModelFactory>() where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Zetbox.API.IDataObject obj) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Zetbox.API.ICompoundObject obj) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Zetbox.App.Base.Property p) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Zetbox.App.Base.BaseParameter p) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Zetbox.App.Base.Method m) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(ViewModelDescriptor desc) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(System.Type t) where TModelFactory : class;

        IDelayedTask CreateDelayedTask(ViewModel displayer, Action loadAction);
        void TriggerDelayedTask(ViewModel displayer, Action loadAction);

        // IMultipleInstancesManager
        void OnIMultipleInstancesManagerCreated(Zetbox.API.IZetboxContext ctx, IMultipleInstancesManager workspace);
        void OnIMultipleInstancesManagerDisposed(Zetbox.API.IZetboxContext ctx, IMultipleInstancesManager workspace);

        IPerfCounter PerfCounter { get; }

        DialogCreator CreateDialog(IZetboxContext ctx, string title);
    }
}
