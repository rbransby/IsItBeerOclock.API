using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using System.Threading;
using IsItBeerOclock.API.Model;

namespace IsItBeerOclock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationControllerController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody]SendNotificationDTO sendNotificationDTO)
        {
            //await SendNotificationAsync(sendNotificationDTO.PushSubscription, sendNotificationDTO.PushMessage, CancellationToken.None);
            return NoContent();
        }


        
    }
}
