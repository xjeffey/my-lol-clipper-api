using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using MyLoLClipperAPI.DataObjects;
using System.Security.Principal;

namespace MyLoLClipperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyLoLClipperAPIController : ControllerBase
    {
        private IConfiguration _configuration;
        private string gApikey;

        public MyLoLClipperAPIController(IConfiguration pConfiguration)
        {
            _configuration = pConfiguration;

            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            if (!string.IsNullOrWhiteSpace(config["apikey"].ToString()))
            {
                gApikey = config["apikey"];
            }
            else
            {
                throw new Exception("API Key not loaded");
            }
        }

        private Task<string> GetAPICall(string pUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(pUrl);
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("X-Riot-Token", gApikey);
                    HttpResponseMessage response = client.Send(request);
                    var responseString = response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return responseString;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAccount")]
        public Account? GetAccount(string pGameName, string pTagLine)
        {
            string url = String.Format("https://americas.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{0}/{1}", pGameName, pTagLine);
            var responseTask = GetAPICall(url);

            if (responseTask != null)
            {
                Account? account = JsonConvert.DeserializeObject<Account>(responseTask.Result);

                if (account != null)
                {
                    return account;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetSummoner")]
        public Summoner? GetSummoner(string pPuuid)
        {
            string url = String.Format("https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{0}", pPuuid);
            var responseTask = GetAPICall(url);

            if (responseTask != null)
            {
                Summoner? summoner = JsonConvert.DeserializeObject<Summoner>(responseTask.Result);

                if (summoner != null)
                {
                    return summoner;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        [HttpGet]
        [Route("GetMatchIds")]
        public List<string> GetMatchIds(string pPuuid, int pStart, int pCount)
        {
            List<string> matchIds = new List<string>();

            string url = String.Format("https://americas.api.riotgames.com/lol/match/v5/matches/by-puuid/{0}/ids?start={1}&count={2}", pPuuid, pStart, pCount);

        //https://americas.api.riotgames.com/lol/match/v5/matches/by-puuid/Z1INT7tiCIlYipMB_dXsSGwOwTRz5OMdw7bIXvD87iV423FW0CyPR7LHZxQOr0HmDhH9YLEhwbdQZw
            var responseTask = GetAPICall(url);

            if (responseTask != null)
            {
                matchIds = JsonConvert.DeserializeObject<List<string>>(responseTask.Result);
            }

            return matchIds;
        }
    }
}
