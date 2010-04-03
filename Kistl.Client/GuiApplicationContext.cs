
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
        /// A non-persisted <see cref="IKistlContext"/> for transient objects
        /// </summary>
        IKistlContext TransientContext { get; }

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
        KistlConfig Configuration { get; }
    }

    public class GuiApplicationContext
        : ClientApiContext, IGuiApplicationContext
    {
        public static new GuiApplicationContext Current { get; private set; }

        public ICustomActionsManager CustomActionsManager { get; private set; }

        private readonly Func<MemoryContext> MemoryContextFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="tkName">This has to be astring, since the 
        /// <see cref="Toolkit"/> enum is defined in the Kistl.Objects 
        /// assembly, which cannot be loaded before initialisation 
        /// of the <see cref="AssemblyLoader"/>, which is loaded in 
        /// the same calling method (which is too late).</param>
        /// <param name="memCtxFactory">The transient context will be created from this factory.</param>
        public GuiApplicationContext(KistlConfig config, string tkName, Func<MemoryContext> memCtxFactory)
            : base(config)
        {
            MemoryContextFactory = memCtxFactory;

            GuiApplicationContext.Current = this;

            CustomActionsManager = new CustomActionsManagerClient();
            CustomActionsManager.Init(FrozenContext.Single);

            Toolkit tk = (Toolkit)Enum.Parse(typeof(Toolkit), tkName, true);
            // TODO: replace by fetching TypeRefs from the Store
            //       via the FrozenContext
            switch (tk)
            {
                case Toolkit.WPF:
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();
                    //UiThread = new Kistl.Client.Presentables.WPF.UiThreadManager();
                    //AsyncThread = new Kistl.Client.Presentables.WPF.AsyncThreadManager();

                    Factory = (ModelFactory)Activator.CreateInstance(
                        Type.GetType("Kistl.Client.WPF.WpfModelFactory, Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ccdf16e4dd7b6d78", true),
                        new object[] { this }
                      );
                    break;
                case Toolkit.WinForms:
                    //UiThread = new Kistl.Client.Presentables.WPF.UiThreadManager();
                    //AsyncThread = new Kistl.Client.Presentables.WPF.AsyncThreadManager();
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();

                    Factory = (ModelFactory)Activator.CreateInstance(
                        Type.GetType("Kistl.Client.Forms.FormsModelFactory, Kistl.Client.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ccdf16e4dd7b6d78", true),
                        new object[] { this });
                    break;
                case Toolkit.ASPNET:
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();

                    Factory = (ModelFactory)Activator.CreateInstance(
                        Type.GetType("Kistl.Client.ASPNET.Toolkit.AspnetModelFactory, Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ccdf16e4dd7b6d78", true),
                        new object[] { this });
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
        /// private backing store for the <see cref="TransientContext"/> property.
        /// </summary>
        private MemoryContext _transientContextCache;

        /// <inheritdoc/>
        public IKistlContext TransientContext
        {
            get
            {
                if (_transientContextCache == null)
                    _transientContextCache = MemoryContextFactory.Invoke();
                return _transientContextCache;
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

        public void SetCustomActionsManager(ICustomActionsManager manager)
        {
            this.CustomActionsManager = manager;
        }
    }
}
