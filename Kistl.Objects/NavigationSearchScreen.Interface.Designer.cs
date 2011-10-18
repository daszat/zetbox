// <autogenerated/>

namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Navigation screen for searching objects
    /// </summary>
    public interface NavigationSearchScreen : Kistl.App.GUI.NavigationScreen 
    {

        /// <summary>
        /// 
        /// </summary>
        bool? AllowAddNew {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? AllowDelete {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? AllowSelectColumns {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? AllowUserFilter {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? EnableAutoFilter {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        string InitialSort {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Kistl.App.GUI.ListSortDirection? InitialSortDirection {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? IsEditable {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? IsMultiselect {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Kistl.App.GUI.ControlKind RequestedEditorKind {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Kistl.App.GUI.ControlKind RequestedWorkspaceKind {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? RespectRequiredFilter {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? ShowFilter {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? ShowMasterDetail {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? ShowOpenCommand {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool? ShowRefreshCommand {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Kistl.App.Base.ObjectClass Type {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Kistl.App.GUI.InstanceListViewMethod? ViewMethod {
            get;
            set;
        }
    }
}
