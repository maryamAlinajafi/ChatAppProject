//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.ErrorMessages", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        ///   Looks up a localized string similar to Password and Confirm NOT Match ! .
        /// </summary>
        internal static string CompereError {
            get {
                return ResourceManager.GetString("CompereError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Email Address is NOT valid !.
        /// </summary>
        internal static string InvalidEmail {
            get {
                return ResourceManager.GetString("InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Join !.
        /// </summary>
        internal static string Join_btn {
            get {
                return ResourceManager.GetString("Join-btn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to this field must less than {1} characters .
        /// </summary>
        internal static string maxLenght {
            get {
                return ResourceManager.GetString("maxLenght", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to this field must more than {1} charecters.
        /// </summary>
        internal static string minLenght {
            get {
                return ResourceManager.GetString("minLenght", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to it must be between {0} and {1} characters!.
        /// </summary>
        internal static string min_max {
            get {
                return ResourceManager.GetString("min-max", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string PhoneNumber {
            get {
                return ResourceManager.GetString("PhoneNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is required.please fill this field !.
        /// </summary>
        internal static string RequireError {
            get {
                return ResourceManager.GetString("RequireError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select the semester from list !.
        /// </summary>
        internal static string semesterRenge {
            get {
                return ResourceManager.GetString("semesterRenge", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is required!.
        /// </summary>
        internal static string semesterRequire {
            get {
                return ResourceManager.GetString("semesterRequire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The access code you Entered is NOT exist ! .
        /// </summary>
        internal static string WrongAccessCode {
            get {
                return ResourceManager.GetString("WrongAccessCode", resourceCulture);
            }
        }
    }
}
