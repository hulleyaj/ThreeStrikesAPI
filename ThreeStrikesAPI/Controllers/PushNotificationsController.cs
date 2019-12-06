using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThreeStrikesAPI.Models;

namespace ThreeStrikesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushNotificationsController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> SendPushNotification([FromBody] List<PushNotification> pushNotification)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("https://exp.host/--/api/v2/push/send", pushNotification);

                this.HttpContext.Response.RegisterForDispose(response);

                return new HttpResponseMessageResult(response);
            }
        }
    }
}