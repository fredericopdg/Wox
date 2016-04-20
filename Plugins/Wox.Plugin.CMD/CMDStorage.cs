using System.Collections.Generic;
using Newtonsoft.Json;
using Wox.Infrastructure.Storage;

namespace Wox.Plugin.CMD
{
    public class CMDHistory
    {
        [JsonProperty]
        public bool ReplaceWinR { get; set; }

        [JsonProperty]
        public bool LeaveCmdOpen { get; set; }

        [JsonProperty]
        public Dictionary<string, int> Count = new Dictionary<string, int>();

        protected CMDHistory LoadDefault()
        {
            ReplaceWinR = true;
            return this;
        }

        public void AddCmdHistory(string cmdName)
        {
            if (Count.ContainsKey(cmdName))
            {
                Count[cmdName] += 1;
            }
            else
            {
                Count.Add(cmdName, 1);
            }
        }
    }
}
