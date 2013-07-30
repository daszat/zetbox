﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.2012
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zetbox.Client.Models {
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
    internal class FilterModelsResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FilterModelsResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Zetbox.Client.Models.FilterModelsResources", typeof(FilterModelsResources).Assembly);
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
        ///   Looks up a localized string similar to From.
        /// </summary>
        internal static string From {
            get {
                return ResourceManager.GetString("From", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fulltext search for any text.
        /// </summary>
        internal static string FulltextFilterModel_Description {
            get {
                return ResourceManager.GetString("FulltextFilterModel_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;p&gt;More information can be found here: &lt;a href=&quot;http://lucene.apache.org/core/3_0_3/queryparsersyntax.html&quot;&gt;http://lucene.apache.org/core/3_0_3/queryparsersyntax.html&lt;/a&gt;&lt;/p&gt;
        ///
        ///&lt;h2&gt;Terms&lt;/h2&gt;
        ///&lt;pre&gt;test hello&lt;/pre&gt;
        ///&lt;pre&gt;&quot;test&quot; &quot;hello&quot;&lt;/pre&gt;
        ///&lt;pre&gt;&quot;test hello&quot;&lt;/pre&gt;
        ///
        ///&lt;h2&gt;Fields&lt;/h2&gt;
        ///&lt;p&gt;When performing a search you can either specify a field, or use the default field&lt;/p&gt;
        ///&lt;pre&gt;title:&quot;The Right Way&quot; AND text:go&lt;/pre&gt;
        ///&lt;pre&gt;title:&quot;Do it right&quot; AND right&lt;/pre&gt;
        ///
        ///&lt;h2&gt;Wildcard Searches&lt;/h2&gt;
        ///&lt;pre&gt;te?t&lt;/pre&gt;        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FulltextFilterModel_HelpText {
            get {
                return ResourceManager.GetString("FulltextFilterModel_HelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fulltext search.
        /// </summary>
        internal static string FulltextFilterModel_Label {
            get {
                return ResourceManager.GetString("FulltextFilterModel_Label", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To.
        /// </summary>
        internal static string To {
            get {
                return ResourceManager.GetString("To", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Text contained in an Item.
        /// </summary>
        internal static string ToStringFilterModel_Description {
            get {
                return ResourceManager.GetString("ToStringFilterModel_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Text.
        /// </summary>
        internal static string ToStringFilterModel_Label {
            get {
                return ResourceManager.GetString("ToStringFilterModel_Label", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to With deactivated items.
        /// </summary>
        internal static string WithDeactivatedFilterModel_Description {
            get {
                return ResourceManager.GetString("WithDeactivatedFilterModel_Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WithDeactivated.
        /// </summary>
        internal static string WithDeactivatedFilterModel_Label {
            get {
                return ResourceManager.GetString("WithDeactivatedFilterModel_Label", resourceCulture);
            }
        }
    }
}
