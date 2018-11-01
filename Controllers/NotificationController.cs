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

namespace IsItBeerOclock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private DataContext _dataContext;

        public NotificationController(DataContext context)
        {
            this._dataContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody]PushMessage pushMessage)
        {
            foreach (var pushSubscription in _dataContext.PushSubscriptions)
            {
                await SendNotificationAsync(pushSubscription.ToPushSubscription(), pushMessage, CancellationToken.None);
            }
            
            return NoContent();
        }

        private async Task SendNotificationAsync(Lib.Net.Http.WebPush.PushSubscription subscription, PushMessage message, CancellationToken cancellationToken)
        {
            var _pushClient = new PushServiceClient();
            _pushClient.DefaultAuthentication = new VapidAuthentication("BP3KYW8aPpClaCjP2MUceUNTwqBSaK88kTnl6SWg0k134zAy_dNNub8LfGHo83bbkm-LUGAd_aLKM0z_4cpUlY8", "SSvrSz8WiLxNJB4SOZXiBs12n3VPLyftqHR05xpobGo");
            try
            {
                await _pushClient.RequestPushMessageDeliveryAsync(subscription, message, cancellationToken);
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, "Failed requesting push message delivery to {0}.", subscription.Endpoint);
            }
        }

    }
}

using IsItBeerOclock.API.Model;
using IsItBeerOclock.API.DataAccess;
using IsItBeerOclock.API.PushNotifications;

            var pushNotificationManager = new PushNotificationManager();
            foreach (var pushSubscription in _dataContext.PushSubscriptions)
            {
                await pushNotificationManager.SendNotificationAsync(pushSubscription.ToPushSubscription(), pushMessage, CancellationToken.None);
        }        