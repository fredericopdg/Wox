using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Wox.Infrastructure.Storage;

namespace Wox.Plugin.Program
{
    [Serializable]
    public class Settings
    {
        [JsonProperty]
        public List<ProgramSource> ProgramSources { get; set; }


        [JsonProperty]
        public string[] ProgramSuffixes { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(true)]
        public bool EnableStartMenuSource { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(true)]
        public bool EnableRegistrySource { get; set; }

        protected Settings LoadDefault()
        {
            ProgramSources = new List<ProgramSource>();
            EnableStartMenuSource = true;
            EnableRegistrySource = true;
            return this;
        }

        protected void OnAfterLoad()
        {
            if (ProgramSuffixes == null || ProgramSuffixes.Length == 0)
            {
                ProgramSuffixes = new[] {"bat", "appref-ms", "exe", "lnk"};
            }
        }

    }
}
