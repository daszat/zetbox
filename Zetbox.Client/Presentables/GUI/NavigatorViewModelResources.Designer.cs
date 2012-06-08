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
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zetbox.Client.Presentables.GUI {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NavigatorViewModelResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NavigatorViewModelResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Zetbox.Client.Presentables.GUI.NavigatorViewModelResources", typeof(NavigatorViewModelResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Back.
        /// </summary>
        internal static string BackCommand_Name {
            get {
                return ResourceManager.GetString("BackCommand_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Navigates back to the last screen.
        /// </summary>
        internal static string BackCommand_Tooltip {
            get {
                return ResourceManager.GetString("BackCommand_Tooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want to close this Application?.
        /// </summary>
        internal static string CanClose {
            get {
                return ResourceManager.GetString("CanClose", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Close Application.
        /// </summary>
        internal static string CanClose_Title {
            get {
                return ResourceManager.GetString("CanClose_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Home.
        /// </summary>
        internal static string HomeCommand_Name {
            get {
                return ResourceManager.GetString("HomeCommand_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Navigates back to the top-most screen.
        /// </summary>
        internal static string HomeCommand_Tooltip {
            get {
                return ResourceManager.GetString("HomeCommand_Tooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Go to ....
        /// </summary>
        internal static string NavigateToCommand_Name {
            get {
                return ResourceManager.GetString("NavigateToCommand_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Navigates to the selected screen.
        /// </summary>
        internal static string NavigateToCommand_Tooltip {
            get {
                return ResourceManager.GetString("NavigateToCommand_Tooltip", resourceCulture);
            }
        }
    }
}
