using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.App.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client
{
    public interface IGuiApplicationContext
    {
        /// <summary>
        /// A read-only <see cref="IKistlContext"/> to access meta data
        /// </summary>
        IKistlContext MetaContext { get; }

        /// <summary>
        /// The <see cref="ModelFactory"/> of this GUI.
        /// </summary>
        ModelFactory Factory { get; }

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

    public class GuiApplicationContext : ClientApiContext, IGuiApplicationContext
    {
        public static new GuiApplicationContext Current { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="tkName">Das muss leider ein String sein, 
        /// weil die Enum in Kistl.Objects definiert ist. 
        /// Zum Zeitpunkt des Aufrufs des Constructors könnte 
        /// aber der Assembly Resolver noch nicht initialisiert 
        /// sein (z.B. sie werden in der gleichen Methode ausgeführt).</param>
        public GuiApplicationContext(KistlConfig config, string tkName)
            : base(config)
        {
            GuiApplicationContext.Current = this;
            SetCustomActionsManager(new CustomActionsManagerClient());

            // this.Renderer = KistlGUIContext.CreateRenderer(tk);

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
                        Type.GetType("Kistl.Client.WPF.WpfModelFactory, Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", true),
                        new object[] { this }
                      );
                    break;
                case Toolkit.WinForms:
                    //UiThread = new Kistl.Client.Presentables.WPF.UiThreadManager();
                    //AsyncThread = new Kistl.Client.Presentables.WPF.AsyncThreadManager();
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();

                    Factory = (ModelFactory)Activator.CreateInstance(
                        Type.GetType("Kistl.Client.Forms.FormsModelFactory, Kistl.Client.Forms", true),
                        new object[] { this });
                    break;
                case Toolkit.ASPNET:
                    UiThread = new SynchronousThreadManager();
                    AsyncThread = new SynchronousThreadManager();

                    Factory = (ModelFactory)Activator.CreateInstance(
                        Type.GetType("Kistl.Client.ASPNET.Toolkit.AspnetModelFactory, Kistl.Client.ASPNET.Toolkit", true),
                        new object[] { this });
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public IRenderer Renderer { get; private set; }

        /// <summary>
        /// A read-only <see cref="IKistlContext"/> to access meta data
        /// </summary>
        public IKistlContext GuiDataContext { get { return Kistl.API.FrozenContext.Single; } }

        //private IKistlContext _guiDataContextCache;
        /// <summary>
        /// A <see cref="IKistlContext"/> for the GUI internals
        /// </summary>
        public IKistlContext MetaContext
        {
            get
            {
                return FrozenContext.Single;
                //if (_guiDataContextCache == null)
                //    _guiDataContextCache = KistlContext.GetContext();
                //return _guiDataContextCache;
            }
        }

        /// <summary>
        /// The <see cref="ModelFactory"/> of this GUI.
        /// </summary>
        public ModelFactory Factory { get; private set; }


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
