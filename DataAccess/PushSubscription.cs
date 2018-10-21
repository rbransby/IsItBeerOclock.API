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
    }
}
