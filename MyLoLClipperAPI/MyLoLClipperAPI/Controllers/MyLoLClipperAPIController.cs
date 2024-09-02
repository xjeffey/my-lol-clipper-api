using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyLoLClipperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyLoLClipperAPIController : ControllerBase
    {
        private IConfiguration _configuration;

        public MyLoLClipperAPIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAPIKey")]
        public string GetAPIKey()
        {
            string apikey = "";

            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            if (!string.IsNullOrWhiteSpace(config["apikey"].ToString())) {
                apikey = config["apikey"];
            }

            return apikey;
        }
    }
}
