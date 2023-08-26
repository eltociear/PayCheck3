using NetCoreServer;
using Newtonsoft.Json;
using PayCheckServerLib.Jsons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCheckServerLib.Responses
	{
	public class Party {
		// indexed by 6 character code
		static Dictionary<string, PartyJson.DataJson> ActiveParties = new();

		static PartyJson.DataJson CreateParty(string userid, PartyPostJsonBody body) {
			var partycode = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 6).Select(str => str[new Random().Next(str.Length)]).ToArray());
			PartyJson.DataJson party = new() {
				Code = partycode,
				IsActive = true,
				IsFull = false,
				Version = 5,
				Id = Guid.NewGuid().ToString().Replace("-", ""),
				Namespace = "pd3beta",
				CreatedAt = DateTime.Now.ToUniversalTime().ToString("o"),
				CreatedBy = userid,
				UpdatedAt = DateTime.Now.ToUniversalTime().ToString("o"),
				LeaderId = userid,
				Configuration = new() {
					Persistent = false,
					TextChat = true,
					AutoJoin = false,
					TieTeamsSessionLifetime = false,
					MinPlayers = 1,
					MaxPlayers = 4,
					InviteTimeout = 60,
					InactiveTimeout = 60,
					Name = body.ConfigurationName,
					Type = "NONE",
					// others might be: PUBLIC, PRIVATE, FRIENDS_ONLY
					Joinability = "INVITE_ONLY",
					Deployment = "",
					ClientVersion = "0",
					RequestedRegions = new() {"eu-central-1","eu-north-1","eu-west-1"},
					DsSource = "",
					PreferredClaimKeys = null,
					FallbackClaimKeys = null,
					NativeSessionSetting = new() {
						XboxSessionTemplateName = "Payday3",
						XboxServiceConfigId = "00000000-0000-0000-0000-00006056e5d6",
						PSNServiceLabel = 0,
						SessionTitle = "Payday3 Party",
						ShouldSync = true,
						PSNSupportedPlatforms = new() {},
						LocalizedSessionName = new() {
							DefaultLanguage = "en-US",
							LocalizedText = new() {
								DeDE = "GRUPPE Payday3",
								EnUS = "Payday3 Party",
								Es419 = "GRUPO Payday3",
								EsES = "GRUPO Payday3",
								FrFR = "GROUPE Payday3",
								ItIT = "SQUADRA Payday3",
								JaJP = "Payday3 パーティー",
								KoKR = "Payday3 GRUP",
								PlPL = "ZESPÓŁ Payday3",
								PtBR = "GRUPO Payday3",
								RuRU = "КОМАНДА Payday3",
								TrTR = "Payday3 파티",
								ZhHans = "Payday3 参加者",
								ZhHant = "Payday3 隊伍"
							}
						}
					},
					PSNBaseURL = "https://s2s.sp-int.playstation.net"
				},
				Members = new() {
					new() {
						Id = userid,
						Status = "JOINED",
						StatusV2 = "JOINED",
						UpdatedAt = DateTime.Now.ToUniversalTime().ToString("o"),
						PlatformId = body.Members[0].PlatformId,
						PlatformUserId = body.Members[0].PlatformUserId
					}
				},
				Attributes = new() {
					DifficultyIdx = 0,
					LevelIdx = -1,
					PlayersNum = 1,
					PortBeacon = 0,
					PortGame = 0,
					SecurityCompanies = "0",
					SessionTemplateName = body.Attributes.SessionTemplateName,
					Status = "Idle",
					TextChat = true,
					Preference = new() {
						CrossplayEnabled = true,
						CurrentPlatform = body.Members[0].PlatformId
					}
				}
			};

			Debugger.PrintInfo(String.Format("Party created with code: {0}", partycode));

			return party;
		}

		static List<PartyJson.DataJson> PartiesPlayerIsIn(string userid) {
			List<PartyJson.DataJson> ret = new();
			foreach (var pair in ActiveParties) {
				var party2 = pair.Value;
				foreach (var member in party2.Members) {
					if (member.Id == userid) {
						ret.Add(party2);
					}
				}
			}
			return ret;
		}

		static PartyJson.DataJson GetPartyById(string id) {
			foreach (var party in ActiveParties) {
				if(party.Value.Id == id) {
					return party.Value;
				}
			}
			return null;
		}

		[HTTP("GET", "/session/v1/public/namespaces/pd3beta/users/me/parties")]
		public static bool SessionsParties(HttpRequest request, PC3Server.PC3Session session) {
			// change this to be dynamic asap
			var userid = "29475976933497845197035744456968";

			ResponseCreator response = new ResponseCreator();

			List<PartyJson.DataJson> partiesPlayerIsIn = PartiesPlayerIsIn(userid);

			PartyJson party = new() {
				Data = partiesPlayerIsIn,
				Paging = new() {
					First = "",
					Last = "",
					Next = "",
					Previous = ""
				}
			};

			response.SetBody(JsonConvert.SerializeObject(party));
			session.SendResponse(response.GetResponse());
			return true;
		}

		[HTTP("POST", "/session/v1/public/namespaces/pd3beta/party")]
		public static bool PostParty(HttpRequest request, PC3Server.PC3Session session) {
			// change this to be dynamic asap
			var userid = "29475976933497845197035744456968";
			var body = JsonConvert.DeserializeObject<PartyPostJsonBody>(request.Body);

			ResponseCreator response = new ResponseCreator();

			var party = CreateParty(userid, body);

			ActiveParties.Add(party.Code, party);

			response.SetBody(JsonConvert.SerializeObject(party));
			session.SendResponse(response.GetResponse());
			return true;
		}

		[HTTP("DELETE", "/session/v1/public/namespaces/pd3beta/parties/{partyid}/users/me/leave")]
		public static bool LeaveParty(HttpRequest request, PC3Server.PC3Session session) {
			// change this to be dynamic asap
			var userid = "29475976933497845197035744456968";
			var partyid = session.HttpParam["partyid"];

			var party = GetPartyById(partyid);

			List<PartyJson.DataJson.MembersJson> newmembers = new();
			foreach (var member in party.Members) {
				if(member.Id != userid) {
					newmembers.Add(member);
				}
			}
			party.Members = newmembers;

			ActiveParties[party.Code] = party;

			ResponseCreator response = new ResponseCreator(204);
			session.SendResponse(response.GetResponse());
			return true;
		}

		[HTTP("PATCH", "/session/v1/public/namespaces/pd3beta/parties/{partyid}")]
		public static bool PatchParty(HttpRequest request, PC3Server.PC3Session session) {
			var newparty = JsonConvert.DeserializeObject<PartyPatchJsonBody>(request.Body);

			if(newparty == null) {
				ResponseCreator response2 = new ResponseCreator(400);
				session.SendResponse(response2.GetResponse());
				return true;
			}

			var partyid = session.HttpParam["partyid"];
			var party = GetPartyById(partyid);
			party.Attributes = newparty.Attributes;
			
			if(newparty.InactiveTimeout != 0) {
				party.Configuration.InactiveTimeout = newparty.InactiveTimeout;
			}
			if(newparty.InviteTimeout != 0) {
				party.Configuration.InviteTimeout = newparty.InviteTimeout;
			}
			if(newparty.MinPlayers != 0) {
				party.Configuration.MinPlayers = newparty.MinPlayers;
			}
			ActiveParties[party.Code] = party;

			ResponseCreator response = new ResponseCreator();
			response.SetBody(JsonConvert.SerializeObject(party));
			session.SendResponse(response.GetResponse());
			return true;
		}

		// todo: use slejmur's userid branch and add player to party
		[HTTP("POST", "/session/v1/public/namespaces/pd3beta/parties/users/me/join/code")]
		public static bool JoinPartyByCode(HttpRequest request, PC3Server.PC3Session session) {
			var body = JsonConvert.DeserializeObject<PartyJoinCodePostBody>(request.Body);

			if (ActiveParties[body.Code] == null) {
				return true;
			}

			ResponseCreator response = new ResponseCreator();
			response.SetBody(JsonConvert.SerializeObject(ActiveParties[body.Code]));
			session.SendResponse(response.GetResponse());
			return true;
		}
	}
}
