﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMMPGuiApp.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.0.3.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://github.com/pmmp/PocketMine-MP/releases/latest/download/PocketMine-MP.phar" +
            "")]
        public string PocketMineURL {
            get {
                return ((string)(this["PocketMineURL"]));
            }
            set {
                this["PocketMineURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://github.com/pmmp/PocketMine-MP/releases/latest/download/start.cmd")]
        public string StartcmdURL {
            get {
                return ((string)(this["StartcmdURL"]));
            }
            set {
                this["StartcmdURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://jenkins.pmmp.io/job/PHP-8.0-Aggregate/lastSuccessfulBuild/artifact/PHP-8." +
            "0-Windows-x64.zip")]
        public string BinURL {
            get {
                return ((string)(this["BinURL"]));
            }
            set {
                this["BinURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://raw.githubusercontent.com/pmmp/PocketMine-MP/stable/composer.json")]
        public string ComposerJsonURL {
            get {
                return ((string)(this["ComposerJsonURL"]));
            }
            set {
                this["ComposerJsonURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://getcomposer.org/download/latest-stable/composer.phar")]
        public string ComposerPharURL {
            get {
                return ((string)(this["ComposerPharURL"]));
            }
            set {
                this["ComposerPharURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("PocketMine-MP")]
        public string Path {
            get {
                return ((string)(this["Path"]));
            }
            set {
                this["Path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DeveloperMode {
            get {
                return ((bool)(this["DeveloperMode"]));
            }
            set {
                this["DeveloperMode"] = value;
            }
        }
    }
}
