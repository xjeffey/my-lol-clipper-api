using Newtonsoft.Json;

namespace MyLoLClipperAPI.DataObjects
{
    public class Match
    {
        [JsonProperty("metadata")]
        public MetaDataDto MetaData { get; set; }

        [JsonProperty("info")]
        public InfoDto Info { get; set; } 

        public class MetaDataDto
        {
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }

            [JsonProperty("matchId")]
            public string MatchId { get; set; }

            [JsonProperty("participants")]
            public List<string> Participants { get; set; }
        }

        public class InfoDto
        {
            [JsonProperty("endOfGameResult")]
            public string EndOfGameResult { get; set; }

            [JsonProperty("gameCreation")]
            public long GameCreation { get; set; }

            [JsonProperty("gameDuration")]
            public long GameDuration { get; set; }

            [JsonProperty("gameEndTimestamp")]
            public long GameEndTimestamp {  get; set; }

            [JsonProperty("gameId")]
            public long GameId { get ; set; }

            [JsonProperty("gameMode")]
            public string GameMode { get; set; }

            [JsonProperty("gameName")]
            public string GameName { get; set; }

            [JsonProperty("gameStartTimestamp")]
            public long GameStartTimestamp { get; set; }

            [JsonProperty("gameType")]
            public string GameType { get; set; }

            [JsonProperty("gameVersion")]
            public string GameVersion { get; set; }

            [JsonProperty("mapId")]
            public int MapId { get; set; }

            //[JsonProperty("participants")]
            //public List<ParticipantsDto> Participants { get; set; }

            [JsonProperty("platformId")]
            public string PlatformId { get; set; }

            [JsonProperty("queueId")]
            public int queueId { get; set; }

            [JsonProperty("teams")]
            public List<TeamDto> Teams { get; set; }

            [JsonProperty("tournamentCode")]
            public string TournamentCode { get; set; }
        }

        public class ParticipantDto
        {

        }

        public class TeamDto
        {

        }
    }


}
