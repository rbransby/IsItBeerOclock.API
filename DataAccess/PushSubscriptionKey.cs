using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsItBeerOclock.API.DataAccess
{
    public class PushSubscriptionKey
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PushSubscription")]
        public string Endpoint { get; set; }

        public string KeyType { get; set; }
        public string KeyValue { get; set; }

        public PushSubscription PushSubscription { get; set; }
    }
}