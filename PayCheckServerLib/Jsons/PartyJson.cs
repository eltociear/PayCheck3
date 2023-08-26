using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace PayCheckServerLib.Jsons {
    public class PartyJoinCodePostBody
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
    public class PartyPatchJsonBody
    {
        [JsonProperty("attributes")]
        public PartyJson.DataJson.AttributesJson Attributes { get; set; }
        [JsonProperty("inactiveTimeout")]
        public int InactiveTimeout { get; set; }
        [JsonProperty("inviteTimeout")]
        public int InviteTimeout { get; set; }
        [JsonProperty("minPlayers")]
        public int MinPlayers { get; set; }
    }
    public class PartyPostJsonBody {
		public partial class MembersJson
        {
			[JsonProperty("iD")]
			public string Id { get; set; }
			[JsonProperty("statusV2")]
			public string StatusV2 { get; set; }
			[JsonProperty("platformID")]
			public string PlatformId { get; set; }
			[JsonProperty("platformUserID")]
			public string PlatformUserId { get; set; }
		}
		[JsonProperty("attributes")]
        public PartyJson.DataJson.AttributesJson Attributes { get; set; }
        [JsonProperty("configurationName")]
        public string ConfigurationName { get; set; }
        [JsonProperty("members")]
        public List<MembersJson> Members { get; set; }
    }
	public class PartyJson
        {
        public partial class PagingJson
            {
            [JsonProperty("first")]
            public string First { get; set; }
            [JsonProperty("last")]
            public string Last { get; set; }
            [JsonProperty("next")]
            public string Next { get; set; }
            [JsonProperty("previous")]
            public string Previous { get; set; }
        }
        public partial class ConfigurationJson
        {
            public partial class NativeSessionSettingJson
            {
                public partial class LocalizedSessionNameJson
                {
                    public partial class LocalizedTextJson {
                        [JsonProperty("de-DE")]
                        public string DeDE { get; set; }
                        [JsonProperty("en-US")]
                        public string EnUS { get; set; }
                        [JsonProperty("es-419")]
                        public string Es419 { get; set; }
                        [JsonProperty("es-ES")]
                        public string EsES { get; set; }
                        [JsonProperty("fr-FR")]
                        public string FrFR { get; set; }
                        [JsonProperty("it-IT")]
                        public string ItIT { get; set; }
                        [JsonProperty("ja-JP")]
                        public string JaJP { get; set; }
                        [JsonProperty("ko-KR")]
                        public string KoKR { get; set; }
                        [JsonProperty("pl-PL")]
                        public string PlPL { get; set; }
                        [JsonProperty("pt-BR")]
                        public string PtBR { get; set; }
                        [JsonProperty("ru-RU")]
                        public string RuRU { get; set; }
                        [JsonProperty("tr-TR")]
                        public string TrTR { get; set; }
                        [JsonProperty("zh-Hans")]
                        public string ZhHans { get; set; }
                        [JsonProperty("zh-Hant")]
                        public string ZhHant { get; set; }
                    }
                    [JsonProperty("defaultLanguage")]
                    public string DefaultLanguage { get; set; }
                    [JsonProperty("localizedText")]
                    public LocalizedTextJson LocalizedText { get; set; }
                }
                [JsonProperty("XboxSessionTemplateName")]
                public string XboxSessionTemplateName { get; set; }
                [JsonProperty("XboxServiceConfigID")]
                public string XboxServiceConfigId { get; set; }
                [JsonProperty("PSNServiceLabel")]
                public int PSNServiceLabel { get; set; }
                [JsonProperty("SessionTitle")]
                public string SessionTitle { get; set; }
                [JsonProperty("ShouldSync")]
                public bool ShouldSync { get; set; }
                [JsonProperty("PSNSupportedPlatforms")]
                // might be another type
                public List<string> PSNSupportedPlatforms { get; set; }
                [JsonProperty("localizedSessionName")]
                public LocalizedSessionNameJson LocalizedSessionName { get; set; }
            }
            [JsonProperty("persistent")]
            public bool Persistent { get; set; }
            [JsonProperty("textChat")]
            public bool TextChat { get; set; }
            [JsonProperty("autoJoin")]
            public bool AutoJoin { get; set; }
            [JsonProperty("tieTeamsSessionLifetime")]
            public bool TieTeamsSessionLifetime { get; set; }
            [JsonProperty("minPlayers")]
            public int MinPlayers { get; set; }
            [JsonProperty("maxPlayers")]
            public int MaxPlayers { get; set; }
            [JsonProperty("inviteTimeout")]
            public int InviteTimeout { get; set; }
            [JsonProperty("inactiveTimeout")]
            public int InactiveTimeout { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("joinability")]
            public string Joinability { get; set; }
            [JsonProperty("deployment")]
            public string Deployment { get; set; }
            [JsonProperty("clientVersion")]
            public string ClientVersion { get; set; }
            [JsonProperty("requestedRegions")]
            public List<string> RequestedRegions { get; set; }
            [JsonProperty("dsSource")]
            public string DsSource { get; set; }
            [JsonProperty("preferredClaimKeys")]
            public object PreferredClaimKeys { get; set; }
            [JsonProperty("fallbackClaimKeys")]
            public object FallbackClaimKeys { get; set; }
            [JsonProperty("nativeSessionSetting")]
            public NativeSessionSettingJson NativeSessionSetting { get; set; }
            [JsonProperty("PSNBaseURL")]
            public string PSNBaseURL { get; set; }
        };
        public partial class DataJson
        {
            public partial class MembersJson
            {
                [JsonProperty("id")]
                public string Id { get; set; }
                [JsonProperty("status")]
                public string Status { get; set; }
                [JsonProperty("statusV2")]
                public string StatusV2 { get; set; }
                [JsonProperty("updatedAt")]
                public string UpdatedAt { get; set; }
                [JsonProperty("platformID")]
                public string PlatformId { get; set; }
                [JsonProperty("platformUserID")]
                public string PlatformUserId { get; set; }
            }
            public partial class AttributesJson {
                public partial class AttributesPreferenceJson
                {
                    [JsonProperty("crossplay_enabled")]
                    public bool CrossplayEnabled { get; set; }
                    [JsonProperty("current_platform")]
                    public string CurrentPlatform { get; set; }
                }
                [JsonProperty("DIFFICULTYIDX")]
                public int DifficultyIdx { get; set; }
                [JsonProperty("LEVELIDX")]
                public int LevelIdx { get; set; }
                [JsonProperty("PLAYERSNUM")]
                public int PlayersNum { get; set; }
                [JsonProperty("PORTBEACON")]
                public int PortBeacon { get; set; }
                [JsonProperty("PORTGAME")]
                public int PortGame { get; set; }
                [JsonProperty("SECURITYCOMPANIES")]
                public string SecurityCompanies { get; set; }
                [JsonProperty("SESSIONTEMPLATENAME")]
                public string SessionTemplateName { get; set; }
                [JsonProperty("STATUS")]
                public string Status { get; set; }
                [JsonProperty("TEXTCHAT")]
                public bool TextChat { get; set; }
                [JsonProperty("preference")]
                public AttributesPreferenceJson Preference { get; set; }
            }
            [JsonProperty("code")]
            public string Code { get; set; }
            [JsonProperty("isActive")]
            public bool IsActive { get; set; }
            [JsonProperty("isFull")]
            public bool IsFull { get; set; }
            [JsonProperty("version")]
            public int Version { get; set; }
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("namespace")]
            public string Namespace { get; set; }
            [JsonProperty("createdAt")]
            public string CreatedAt { get; set; }
            [JsonProperty("createdBy")]
            public string CreatedBy { get; set; }
            [JsonProperty("updatedAt")]
            public string UpdatedAt { get; set; }
            [JsonProperty("leaderID")]
            public string LeaderId { get; set; }
            [JsonProperty("configuration")]
            public ConfigurationJson Configuration { get; set; }
            [JsonProperty("members")]
            public List<MembersJson> Members { get; set; }
            [JsonProperty("attributes")]
            public AttributesJson Attributes { get; set; }
        }
        [JsonProperty("paging")]
        public PagingJson Paging { get; set; }
        [JsonProperty("data")]
        public List<DataJson> Data { get; set; }
    }
}
