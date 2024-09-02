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
        private string gBaseUrl = "https://americas.api.riotgames.com/";

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

        [HttpGet]
        [Route("GetAccount")]
        public Account? GetAccount(string pGameName, string pTagLine)
        {
            string url = String.Format(gBaseUrl + "riot/account/v1/accounts/by-riot-id/{0}/{1}", pGameName, pTagLine);

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(url);
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("X-Riot-Token", gApikey);
                    HttpResponseMessage response = client.Send(request);
                    var responseString =  response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        Account? account = JsonConvert.DeserializeObject<Account>(responseString.Result);

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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
