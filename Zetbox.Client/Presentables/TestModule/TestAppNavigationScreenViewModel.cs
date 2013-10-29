namespace Zetbox.Client.Presentables.TestModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class TestAppNavigationScreenViewModel : NavigationScreenViewModel
    {
        public new delegate TestAppNavigationScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        public TestAppNavigationScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
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

            return result;
        }
    }
}
