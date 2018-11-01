using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using System.Threading;
using IsItBeerOclock.API.Model;
using IsItBeerOclock.API.DataAccess;
using IsItBeerOclock.API.PushNotifications;

namespace IsItBeerOclock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private DataContext _dataContext;

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody]PushMessage pushMessage)
        {
            var pushNotificationManager = new PushNotificationManager();
            foreach (var pushSubscription in _dataContext.PushSubscriptions)
            {
                await pushNotificationManager.SendNotificationAsync(pushSubscription.ToPushSubscription(), pushMessage, CancellationToken.None);
            }
            
            return NoContent();
        }        
    }
}
