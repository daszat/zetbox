﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zetbox.Client {
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
    internal class ZetboxContextExceptionHandlerResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ZetboxContextExceptionHandlerResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Zetbox.Client.ZetboxContextExceptionHandlerResources", typeof(ZetboxContextExceptionHandlerResources).Assembly);
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
        ///   Looks up a localized string similar to Concurrency Error.
        /// </summary>
        internal static string ConcurrencyException_Caption {
            get {
                return ResourceManager.GetString("ConcurrencyException_Caption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; changed by {1} on {2}.
        /// </summary>
        internal static string ConcurrencyException_DetailFormatString {
            get {
                return ResourceManager.GetString("ConcurrencyException_DetailFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At least one object has changed between fetch and submit changes. Please reopen the workspace and try again..
        /// </summary>
        internal static string ConcurrencyException_Message {
            get {
                return ResourceManager.GetString("ConcurrencyException_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Details.
        /// </summary>
        internal static string DetailsLabel {
            get {
                return ResourceManager.GetString("DetailsLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage Error.
        /// </summary>
        internal static string FKViolationException_Caption {
            get {
                return ResourceManager.GetString("FKViolationException_Caption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} and {1} are in a relationship {2} {3} {4}.
        ///(Database message is &quot;{5}&quot;).
        /// </summary>
        internal static string FKViolationException_DetailFormatString {
            get {
                return ResourceManager.GetString("FKViolationException_DetailFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The object cannot be deleted or updated because is is used by another object..
        /// </summary>
        internal static string FKViolationException_Message {
            get {
                return ResourceManager.GetString("FKViolationException_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unique Error.
        /// </summary>
        internal static string UniqueConstraintViolationException_Caption {
            get {
                return ResourceManager.GetString("UniqueConstraintViolationException_Caption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Database message is &quot;{0}&quot;.
        /// </summary>
        internal static string UniqueConstraintViolationException_DetailFormatString {
            get {
                return ResourceManager.GetString("UniqueConstraintViolationException_DetailFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At least one value is not unique..
        /// </summary>
        internal static string UniqueConstraintViolationException_Message {
            get {
                return ResourceManager.GetString("UniqueConstraintViolationException_Message", resourceCulture);
            }
        }
    }
}
