using Lib.Net.Http.WebPush;
using System;

namespace IsItBeerOclock.API.Model
{
    public class SendNotificationDTO
    {
        public PushMessage PushMessage { get; set; }
        public PushSubscription PushSubscription { get; set; }
    }
}
