﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zetbox.Client.Presentables.ObjectEditor {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MergeObjectsTaskViewModelResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MergeObjectsTaskViewModelResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Zetbox.Client.Presentables.ObjectEditor.MergeObjectsTaskViewModelResources", typeof(MergeObjectsTaskViewModelResources).Assembly);
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
        ///   Looks up a localized string similar to Merge objects.
        /// </summary>
        internal static string Name {
            get {
                return ResourceManager.GetString("Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Replace.
        /// </summary>
        internal static string Source {
            get {
                return ResourceManager.GetString("Source", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This object will be replaced by the other object. This object will be deleted..
        /// </summary>
        internal static string Source_Tooltip {
            get {
                return ResourceManager.GetString("Source_Tooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Swap.
        /// </summary>
        internal static string SwapCommand {
            get {
                return ResourceManager.GetString("SwapCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Swap new and replace objects.
        /// </summary>
        internal static string SwapCommand_Tooltip {
            get {
                return ResourceManager.GetString("SwapCommand_Tooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New.
        /// </summary>
        internal static string Target {
            get {
                return ResourceManager.GetString("Target", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This object will replace the other object..
        /// </summary>
        internal static string Target_Tooltip {
            get {
                return ResourceManager.GetString("Target_Tooltip", resourceCulture);
            }
        }
    }
}
