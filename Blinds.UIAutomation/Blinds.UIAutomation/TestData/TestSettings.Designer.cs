﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blinds.UIAutomation.TestData {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class TestSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static TestSettings defaultInstance = ((TestSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new TestSettings())));
        
        public static TestSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.blinds.com")]
        public string url {
            get {
                return ((string)(this["url"]));
            }
            set {
                this["url"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("chrome")]
        public string browser {
            get {
                return ((string)(this["browser"]));
            }
            set {
                this["browser"] = value;
            }
        }
    }
}