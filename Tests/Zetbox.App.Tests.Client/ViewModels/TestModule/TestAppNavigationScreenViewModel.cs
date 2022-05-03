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
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.App.LicenseManagement;
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

                        return Task.CompletedTask;
                    },
                    null,
                    null));

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Test DialogCreator non-modal",
                    "Opens a complex non-modal dialog",
                    () =>
                    {
                        ViewModelFactory.CreateDialog(DataContext, "Test dialog")
                            .AddGroupBox("grp1", "Group 1",
                                c => c.AddTextBlock("txt1", "txt label", "Textblock 1")
                                      .AddTextBlock("txt2", "", "Textblock 2")
                                      .AddTextBlock("txt3", "txt label"))
                            .AddGroupBox("grp2", "Group 2",
                                c => c.AddString("txt4", "string (test)", "", description: "enter something")
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
                            .OnCanAccept(values => (string)values["txt4"] == "test", values => "\"string\" must be \"test\"")
                            .OnCancel(() =>
                            {
                                ViewModelFactory.ShowMessage("Cancel!!", "Cancel");
                            })
                            .ShowModal(false)
                            .Show(values =>
                            {
                                ViewModelFactory.ShowMessage(
                                    string.Join("\n", values.Select(i => string.Format("{0}: \"{1}\"", i.Key, i.Value))),
                                    "received parameter");
                            });

                        return Task.CompletedTask;
                    },
                    null,
                    null));

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Test Big DialogCreator",
                    "Opens a very complex dialog",
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
                                              .AddString("txt7", "string 7")
                                              .AddString("txt10", "string 10")
                                              .AddString("txt11", "string 11")
                                              .AddString("txt12", "string 12")
                                              .AddInt("int13", "int 13")
                                              .AddString("txt14", "string 14")
                                              .AddDateTime("dt15", "date 15")
                                              .AddString("txt16", "string 16")
                                              .AddString("txt17", "string 17")
                                              .AddBool("bool18", "bool 18")
                                              .AddString("txt19", "string 19")
                                              .AddTextBlock(null, "text 20")
                                              .AddString("txt21", "string 21")
                                              .AddEnumeration("enum22", "enum 22", FrozenContext.FindPersistenceObject<Zetbox.App.Base.Enumeration>(new Guid("1385e46d-3e5b-4d91-bf9a-94a740f08ba1")))
                                              .AddString("txt23", "string 23"))
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

                        return Task.CompletedTask;
                    },
                    null,
                    null));

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Open InstanceListTester",
                    "Open the tester in a real workspace",
                    async () =>
                    {
                        var newScope = ViewModelFactory.CreateNewScope();
                        var newCtx = newScope.ViewModelFactory.CreateNewContext();
                        var ws = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);

                        await newScope.ViewModelFactory.ShowModel(ws, activate: true);

                        ws.ShowModel(newScope.ViewModelFactory.CreateViewModel<InstanceListTestViewModel.Factory>().Invoke(newCtx, ws, this.Screen));
                    },
                    null,
                    null));

            result.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                .Invoke(
                    DataContext,
                    this,
                    "Check License",
                    "Check License",
                    () =>
                    {
                        var newScope = ViewModelFactory.CreateNewScope();
                        var newCtx = newScope.ViewModelFactory.CreateNewContext();

                        var selectClass = newScope.ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                                newCtx,
                                this,
                                (ObjectClass)NamedObjects.Base.Classes.Zetbox.App.LicenseManagement.License.Find(FrozenContext),
                                null,
                                (chosen) =>
                                {
                                    if (chosen != null)
                                    {
                                        var l = (License)chosen.First().Object;
                                        var valid = l.IsSignatureValid(App.Tests.Client.ViewModels.LicenseManagement.Resources.test_cer);
                                        newScope.ViewModelFactory.ShowMessage($"License valid = {valid}", "Result");
                                    }
                                    newScope.Dispose();
                                },
                                null);
                        newScope.ViewModelFactory.ShowDialog(selectClass);

                        return Task.CompletedTask;
                    },
                    null,
                    null));

            return result;
        }
    }
}
