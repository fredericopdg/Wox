using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Wox.Core.Plugin;
using Wox.Infrastructure.Storage;
using Wox.Plugin;
using Newtonsoft.Json;

namespace Wox.Core.UserSettings
{
    public class UserSettings
    {
        
        public bool DontPromptUpdateMsg { get; set; }

        public int ActivateTimes { get; set; }

        public bool EnableUpdateLog { get; set; }

        public string Hotkey { get; set; }

        public string Language { get; set; }

        public string Theme { get; set; }

        public string QueryBoxFont { get; set; }

        public string QueryBoxFontStyle { get; set; }

        public string QueryBoxFontWeight { get; set; }

        public string QueryBoxFontStretch { get; set; }

        public string ResultFont { get; set; }

        public string ResultFontStyle { get; set; }

        public string ResultFontWeight { get; set; }

        public string ResultFontStretch { get; set; }

        public double WindowLeft { get; set; }

        public double WindowTop { get; set; }

        // Order defaults to 0 or -1, so 1 will let this property appear last
        [JsonProperty(Order = 1)]
        public Dictionary<string, CustomizedPluginConfig> CustomizedPluginConfigs { get; set; }

        public List<CustomPluginHotkey> CustomPluginHotkeys { get; set; }

        public bool StartWoxOnSystemStartup { get; set; }

        [Obsolete]
        public double Opacity { get; set; }

        [Obsolete]
        public OpacityMode OpacityMode { get; set; }

        public bool LeaveCmdOpen { get; set; }

        public bool HideWhenDeactive { get; set; }

        public bool RememberLastLaunchLocation { get; set; }

        public bool IgnoreHotkeysOnFullscreen { get; set; }

        public string ProxyServer { get; set; }

        public bool ProxyEnabled { get; set; }

        public int ProxyPort { get; set; }

        public string ProxyUserName { get; set; }

        public string ProxyPassword { get; set; }

        public int MaxResultsToShow { get; set; }

        public void IncreaseActivateTimes()
        {
            ActivateTimes++;
            if (ActivateTimes % 15 == 0)
            {
            }
        }

        // happlebao todo
        protected UserSettings LoadDefault()
        {
            DontPromptUpdateMsg = false;
            Theme = "Dark";
            Language = "en";
            CustomizedPluginConfigs = new Dictionary<string, CustomizedPluginConfig>();
            Hotkey = "Alt + Space";
            QueryBoxFont = FontFamily.GenericSansSerif.Name;
            ResultFont = FontFamily.GenericSansSerif.Name;
            Opacity = 1;
            OpacityMode = OpacityMode.Normal;
            LeaveCmdOpen = false;
            HideWhenDeactive = false;
            CustomPluginHotkeys = new List<CustomPluginHotkey>();
            RememberLastLaunchLocation = false;
            MaxResultsToShow = 6;
            return this;
        }

        public void OnAfterLoad()
        {
            var metadatas = PluginManager.AllPlugins.Select(p => p.Metadata);
            if (CustomizedPluginConfigs == null)
            {
                var configs = new Dictionary<string, CustomizedPluginConfig>();
                foreach (var metadata in metadatas)
                {
                    addPluginMetadata(configs, metadata);
                }
                CustomizedPluginConfigs = configs;
            }
            else
            {
                var configs = CustomizedPluginConfigs;
                foreach (var metadata in metadatas)
                {
                    if (configs.ContainsKey(metadata.ID))
                    {
                        var config = configs[metadata.ID];
                        if (config.ActionKeywords?.Count > 0)
                        {
                            metadata.ActionKeywords = config.ActionKeywords;
                            metadata.ActionKeyword = config.ActionKeywords[0];
                        }
                    }
                    else
                    {
                        addPluginMetadata(configs, metadata);
                    }
                }
            }


            if (QueryBoxFont == null)
            {
                QueryBoxFont = FontFamily.GenericSansSerif.Name;
            }
            if (ResultFont == null)
            {
                ResultFont = FontFamily.GenericSansSerif.Name;
            }
            if (Language == null)
            {
                Language = "en";
            }
        }


        private void addPluginMetadata(Dictionary<string, CustomizedPluginConfig> configs, PluginMetadata metadata)
        {
            configs[metadata.ID] = new CustomizedPluginConfig
            {
                ID = metadata.ID,
                Name = metadata.Name,
                ActionKeywords = metadata.ActionKeywords,
                Disabled = false
            };
        }

        public void UpdateActionKeyword(PluginMetadata metadata)
        {
            var config = CustomizedPluginConfigs[metadata.ID];
            config.ActionKeywords = metadata.ActionKeywords;
        }

    }

    public enum OpacityMode
    {
        Normal = 0,
        LayeredWindow = 1,
        DWM = 2
    }
}