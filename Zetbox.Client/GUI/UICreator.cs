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

namespace Zetbox.Client.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;

    public class UICreator
    {
        public IZetboxContext DataContext { get; private set; }
        public IViewModelFactory ViewModelFactory { get; private set; }
        public ViewModel Parent { get; private set; }

        public UICreator(IViewModelFactory vmf, IZetboxContext ctx, ViewModel parent)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (ctx == null) throw new ArgumentNullException("ctx");

            ViewModelFactory = vmf;
            DataContext = ctx;
            Parent = parent;
        }
    }
    public static class UICreatorExtensions
    {
        /// <summary>
        /// Only used by DataObjectViewModel
        /// </summary>
        public static CustomPropertyGroupViewModel CustomPropertyGroup(this UICreator uiCreator, string key, string title, IEnumerable<ViewModel> children)
        {
            if (uiCreator == null) throw new ArgumentNullException("uiCreator");

            return uiCreator.ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>()
                       .Invoke(uiCreator.DataContext, uiCreator.Parent, key, title, children);
        }

        public static StackPanelViewModel StackPanel(this UICreator uiCreator, IEnumerable<ViewModel> children)
        {
            if (uiCreator == null) throw new ArgumentNullException("uiCreator");

            return uiCreator.ViewModelFactory.CreateViewModel<StackPanelViewModel.Factory>()
                       .Invoke(uiCreator.DataContext, uiCreator.Parent, "__stack", children);
        }

        public static GroupBoxViewModel GroupBox(this UICreator uiCreator, string title, IEnumerable<ViewModel> children)
        {
            if (uiCreator == null) throw new ArgumentNullException("uiCreator");

            return uiCreator.ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>()
                       .Invoke(uiCreator.DataContext, uiCreator.Parent, title, children);
        }

        public static GridPanelViewModel Grid(this UICreator uiCreator, IEnumerable<GridPanelViewModel.Cell> children)
        {
            if (uiCreator == null) throw new ArgumentNullException("uiCreator");

            return uiCreator.ViewModelFactory.CreateViewModel<GridPanelViewModel.Factory>()
                       .Invoke(uiCreator.DataContext, uiCreator.Parent, "__grid", children);
        }

        public static DockPanelViewModel DockPanel(this UICreator uiCreator, IEnumerable<DockPanelViewModel.Cell> children)
        {
            if (uiCreator == null) throw new ArgumentNullException("uiCreator");

            return uiCreator.ViewModelFactory.CreateViewModel<DockPanelViewModel.Factory>()
                       .Invoke(uiCreator.DataContext, uiCreator.Parent, "__dock", children);
        }
        public static PresenterViewModel Presenter(this UICreator uiCreator, ViewModel view, App.GUI.ControlKind requestedKind)
        {
            if (uiCreator == null) throw new ArgumentNullException("uiCreator");

            return uiCreator.ViewModelFactory.CreateViewModel<PresenterViewModel.Factory>()
                       .Invoke(uiCreator.DataContext, uiCreator.Parent, view, requestedKind);
        }
    }
}
