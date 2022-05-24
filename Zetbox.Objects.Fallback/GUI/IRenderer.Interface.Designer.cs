// <autogenerated/>

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("2e93e071-875a-41ee-a768-5d55c2683546")]
    public interface IRenderer  
    {

        /// <summary>
        /// The Toolkit used by this Renderer
        /// </summary>
        [Zetbox.API.DefinitionGuid("83ab9087-52a5-400d-9e41-bd46fb5e7957")]
        Zetbox.App.GUI.Toolkit Platform {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task<Zetbox.API.IDataObject> ChooseObject(Zetbox.API.IZetboxContext ctx, System.Type objectType);

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task ShowMessage(string message);

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task ShowObject(Zetbox.API.IDataObject obj);
    }
}
