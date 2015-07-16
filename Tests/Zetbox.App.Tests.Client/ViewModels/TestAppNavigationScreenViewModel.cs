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

namespace Zetbox.Client.Presentables.TestModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;

    [ViewModelDescriptor]
    public class TestAppNavigationScreenViewModel : NavigationScreenViewModel
    {
        public new delegate TestAppNavigationScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        private readonly Func<IZetboxContext> _ctxFactory;

        public TestAppNavigationScreenViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
            _ctxFactory = ctxFactory;
        }

        protected override List<CommandViewModel> CreateAdditionalCommands()
        {
            var result = base.CreateAdditionalCommands();

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Test ProblemReporter",
                    "Throws an exception so that the Problem Reporter will be shown",
                    () => { throw new NotImplementedException("This is a test exception"); },
                    null,
                    null));

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Test ProblemReporter continuously",
                    "Throws every couple of seconds an exception so that the Problem Reporter will be shown",
                    () =>
                    {

                        var syncContext = System.Threading.SynchronizationContext.Current;
                        System.Threading.ThreadPool.QueueUserWorkItem((x) =>
                        {
                            while (true)
                            {
                                syncContext.Post((y) =>
                                {
                                    throw new NotImplementedException("This is a test exception");
                                }, null);
                                System.Threading.Thread.Sleep(2000);
                            }
                        });
                    },
                    null,
                    null));

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Test DialogCreator",
                    "Opens a complex dialog",
                    () =>
                    {
                        ViewModelFactory.CreateDialog(DataContext, "Test dialog")
                            .AddGroupBox("grp1", "Group 1",
                                c => c.AddTextBlock("txt1", "txt label", "Textblock 1")
                                      .AddTextBlock("txt2", "", "Textblock 2")
                                      .AddTextBlock("txt3", "txt label"))
                            .AddGroupBox("grp2", "Group 2",
                                c => c.AddString("txt4", "string", "", description: "enter something")
                                      .AddMultiLineString("txt5", "string", "", description: "enter something"))
                            .AddTabControl("tabCtrl", "",
                                c => c.AddTabItem("ti1", "Tab 1",
                                        i => i.AddString("txt6", "string 6")
                                              .AddString("txt7", "string 7"))
                                      .AddTabItem("ti2", "Tab 3",
                                        i => i.AddString("txt8", "string 8")
                                              .AddString("txt9", "string 9")))
                            .YesNo()
                            .DefaultButtons("Execute", "Cancel")
                            .AddButton("Execute and Continue", values =>
                            {
                                ViewModelFactory.ShowMessage(
                                    "Execute and Continue: \n\n" + string.Join("\n", values.Select(i => string.Format("{0}: \"{1}\"", i.Key, i.Value))),
                                    "Execute and Continue");
                            })
                            .OnAccept(values => { })
                            .OnCancel(() =>
                            {
                                ViewModelFactory.ShowMessage("Cancel!!", "Cancel");
                            })
                            .Show(values =>
                            {
                                ViewModelFactory.ShowMessage(
                                    string.Join("\n", values.Select(i => string.Format("{0}: \"{1}\"", i.Key, i.Value))),
                                    "received parameter");
                            });
                    },
                    null,
                    null));

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Open InstanceListTester",
                    "Open the tester in a real workspace",
                    () =>
                    {
                        var ctx = _ctxFactory();
                        var ws = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(ctx, null);
                        ViewModelFactory.ShowModel(ws, activate: true);

                        ws.ShowModel(ViewModelFactory.CreateViewModel<InstanceListTestViewModel.Factory>().Invoke(ctx, ws, this.Screen));
                    },
                    null,
                    null));

            return result;
        }
    }
}