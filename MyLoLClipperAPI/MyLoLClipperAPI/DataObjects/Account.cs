using Newtonsoft.Json;

namespace MyLoLClipperAPI.DataObjects
{
    public class Account
    {
        [JsonProperty("puuid")]
        public string Puuid { get; set; }

        [JsonProperty("gameName")]
        public string GameName { get; set; }

        [JsonProperty("tagLine")]
        public string TagLine { get; set; }

        public Account(string pPuuid, string pGameName, string pTagLine)
        {
            Puuid = pPuuid;
            GameName = pGameName;
            TagLine = pTagLine;
        }
    }
}
