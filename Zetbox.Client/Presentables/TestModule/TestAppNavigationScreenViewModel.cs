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
                    () => { throw new Exception("This is a test exception"); },
                    null, 
                    null));


            return result;
        }
    }
}
