using Lib.Net.Http.WebPush;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsItBeerOclock.API.Model
{
    public class PushSubscriptionDTO 
    {
        public int Timeoffset { get; set; }
        public PushSubscription PushSubscription { get; set; }  
        
    }
}
