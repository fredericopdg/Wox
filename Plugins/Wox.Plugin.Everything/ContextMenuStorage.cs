using System.Collections.Generic;
using Newtonsoft.Json;
using Wox.Infrastructure.Storage;

namespace Wox.Plugin.Everything
{
    public class Settings
    {
        [JsonProperty]
        public List<ContextMenu> ContextMenus = new List<ContextMenu>();

        [JsonProperty]
        public int MaxSearchCount { get; set; }

        protected Settings LoadDefault()
        {
            MaxSearchCount = 100;
            return this;
        }
    }

    public class ContextMenu
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Command { get; set; }

        [JsonProperty]
        public string Argument { get; set; }

        [JsonProperty]
        public string ImagePath { get; set; }
    }
}