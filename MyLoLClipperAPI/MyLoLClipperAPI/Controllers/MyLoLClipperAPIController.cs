using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using MyLoLClipperAPI.DataObjects;

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
        public Summoner? GetSummoner(string pGameName, string pTagLine)
        {
            Account? account = GetAccount(pGameName, pTagLine);

            if (account != null)
            {
                string url = String.Format("https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{0}", account.Puuid);
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
            else
            {
                return null;
            }
        }
    }
}
