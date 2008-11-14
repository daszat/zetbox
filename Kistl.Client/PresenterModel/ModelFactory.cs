using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.PresenterModel
{
    public class ModelFactory
    {
        public ModelFactory(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx)
        {
            UI = uiManager;
            UI.Verify();

            Async = asyncManager;

            GuiContext = guiCtx;
            DataContext = dataCtx;
        }

        /// <summary>
        /// A <see cref="IThreadManager"/> for the UI Thread
        /// </summary>
        public IThreadManager UI { get; private set; }
        /// <summary>
        /// A <see cref="IThreadManager"/> for asynchronous Tasks
        /// </summary>
        public IThreadManager Async { get; private set; }

        /// <summary>
        /// A read-only <see cref="IKistlContext"/> to access meta data
        /// </summary>
        public IKistlContext GuiContext { get; private set; }
        /// <summary>
        /// A <see cref="IKistlContext"/> to access the current user's data
        /// </summary>
        public IKistlContext DataContext { get; private set; }

        public TModel CreateModel<TModel>(params object[] data)
            where TModel : PresentableModel
        {
            return (TModel)Activator.CreateInstance(typeof(TModel),
                new object[] { UI, Async, GuiContext, DataContext, this }.Concat(data).ToArray());
        }
    }
}
