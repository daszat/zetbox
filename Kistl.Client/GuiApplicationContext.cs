
namespace Kistl.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;

    public interface IGuiApplicationContext
    {
        /// <summary>
        /// A read-only <see cref="IReadOnlyKistlContext"/> to access meta data
        /// </summary>
        IReadOnlyKistlContext MetaContext { get; }

        /// <summary>
        /// The <see cref="ModelFactory"/> of this GUI.
        /// </summary>
        IModelFactory Factory { get; }

        ///// <summary>
        ///// This context's <see cref="Toolkit"/>
        ///// </summary>
        //Toolkit Toolkit { get; }

        /// <summary>
        /// A <see cref="IThreadManager"/> for the UI Thread
        /// </summary>
        IThreadManager UiThread { get; }
        /// <summary>
        /// A <see cref="IThreadManager"/> for asynchronous Tasks
        /// </summary>
        IThreadManager AsyncThread { get; }

        /// <summary>
        /// The configuration of this context
        /// </summary>
        [Obsolete]
        KistlConfig Configuration { get; }
    }

    public class GuiApplicationContext
        : ClientApiContext, IGuiApplicationContext
    {
        public static new GuiApplicationContext Current { get; private set; }

        public GuiApplicationContext(KistlConfig config, IModelFactory factory)
            : base(config)
        {
            GuiApplicationContext.Current = this;
            Factory = factory;

            // TODO: replace by fetching TypeRefs from the Store
            //       via the FrozenContext
            switch (factory.Toolkit)
            {
                case Toolkit.WPF:
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();
                    //UiThread = new Kistl.Client.Presentables.WPF.UiThreadManager();
                    //AsyncThread = new Kistl.Client.Presentables.WPF.AsyncThreadManager();
                    break;
                case Toolkit.WinForms:
                    //UiThread = new Kistl.Client.Presentables.WPF.UiThreadManager();
                    //AsyncThread = new Kistl.Client.Presentables.WPF.AsyncThreadManager();
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();
                    break;
                case Toolkit.ASPNET:
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            var fam = new FrozenActionsManagerClient();
            fam.Init(ctx);
        }

        public IRenderer Renderer { get; private set; }

        private IReadOnlyKistlContext _metaContext = null;
        /// <inheritdoc />
        public IReadOnlyKistlContext MetaContext
        {
            get
            {
                // TODO: Case# 817
                if (_metaContext == null)
                {
                    if (Configuration.Client.DevelopmentEnvironment == true)
                    {
                        _metaContext = KistlContext.GetContext();
                    }
                    else
                    {
                        _metaContext = FrozenContext.Single;
                    }
                }
                return _metaContext;
            }
        }

        /// <summary>
        /// The <see cref="ModelFactory"/> of this GUI.
        /// </summary>
        public IModelFactory Factory { get; private set; }

        /// <summary>
        /// A <see cref="IThreadManager"/> for the UI Thread
        /// </summary>
        public IThreadManager UiThread { get; private set; }

        /// <summary>
        /// A <see cref="IThreadManager"/> for asynchronous Tasks
        /// </summary>
        public IThreadManager AsyncThread { get; private set; }
    }
}
