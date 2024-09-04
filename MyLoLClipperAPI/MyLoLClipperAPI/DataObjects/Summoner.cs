using Newtonsoft.Json;

namespace MyLoLClipperAPI.DataObjects
{
    public class Summoner
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("profileIconId")]
        public int ProfileIconId { get; set; }

        [JsonProperty("revisionDate")]
        public long RevisionDate { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("puuid")]
        public string Puuid { get; set; }

        [JsonProperty("summonerLevel")]
        public long SummonerLevel { get; set; }

        public Summoner(string pAccountId, int pProfileIconId, long pRevisionDate, string pId, string pPuuid, long pSummonerLevel)
        {
            AccountId = pAccountId;
            ProfileIconId = pProfileIconId;
            RevisionDate = pRevisionDate;
            Id = pId;
            Puuid = pPuuid;
            SummonerLevel = pSummonerLevel;
        }
    }
}
