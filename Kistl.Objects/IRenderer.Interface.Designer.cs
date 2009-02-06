
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// 
    /// </summary>
    public interface IRenderer  
    {

        /// <summary>
        /// The Toolkit used by this Renderer
        /// </summary>

		Kistl.App.GUI.Toolkit Platform { get; set; }
        /// <summary>
        /// 
        /// </summary>

		 void ShowMessage(System.String message) ;
        /// <summary>
        /// 
        /// </summary>

		 void ShowObject(Kistl.API.IDataObject obj) ;
        /// <summary>
        /// 
        /// </summary>

		 Kistl.API.IDataObject ChooseObject(Kistl.API.IKistlContext ctx, System.Type objectType) ;
    }
}