using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IsItBeerOclock.API.PushNotifications
{
    public class PushNotificationManager
    {
        private PushServiceClient _pushClient;

        public PushNotificationManager()
        {
            _pushClient = new PushServiceClient();
        }
        public async Task SendNotificationAsync(Lib.Net.Http.WebPush.PushSubscription subscription, PushMessage message, CancellationToken cancellationToken)
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
