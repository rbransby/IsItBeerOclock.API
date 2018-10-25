using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsItBeerOclock.API.DataAccess
{
    public class PushSubscription
    {
        [Key]
        public string Endpoint { get; set; }

        public IEnumerable<PushSubscriptionKey> Keys { get; set; }

        public TimeSpan TimeOffset { get; set; } 

        public Lib.Net.Http.WebPush.PushSubscription ToPushSubscription()
        {
            return new Lib.Net.Http.WebPush.PushSubscription()
            {
                Endpoint = this.Endpoint,
                Keys = this.Keys.ToDictionary(psk => psk.KeyType, psk => psk.KeyValue)
            };
        }
    }
}
