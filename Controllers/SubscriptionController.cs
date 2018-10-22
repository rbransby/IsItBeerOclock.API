using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using System.Threading;
using IsItBeerOclock.API.DataAccess;
using IsItBeerOclock.API.Model;

namespace IsItBeerOclock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private DataContext _dataContext;

        public SubscriptionController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        [HttpPost]
        public async Task<IActionResult> StoreSubscription([FromBody]PushSubscriptionDTO subscription)
        {
            var pushSubscription = new DataAccess.PushSubscription()
            {
                Endpoint = subscription.PushSubscription.Endpoint,
                TimeOffset = new TimeSpan(subscription.Timeoffset, 0, 0)
            };

            List<PushSubscriptionKey> keys = new List<PushSubscriptionKey>();
            foreach (var key in subscription.PushSubscription.Keys)
            {
                keys.Add(new PushSubscriptionKey { Endpoint = subscription.PushSubscription.Endpoint, KeyType = key.Key, KeyValue = key.Value });
            }

            _dataContext.PushSubscriptions.Add(pushSubscription);
            _dataContext.PushSubscriptionKeys.AddRange(keys);

            _dataContext.SaveChanges();
            return NoContent();
        }        
    }
}
