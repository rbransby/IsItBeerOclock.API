using IsItBeerOclock.API.DataAccess;
using IsItBeerOclock.API.PushNotifications;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IsItBeerOclock.API.HostedService
{
    public class SendNotifications : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;                
        private readonly DateTime TimeToNotify = new DateTime(2018, 11, 16, 16, 00, 00);
        private readonly DayOfWeek DayOfWeekToNotify = DayOfWeek.Friday;

        public SendNotifications(ILogger<SendNotifications> logger)
        {
            _logger = logger;                        
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(120));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            DataContext context = new DataContext();
            // get all subscribers for which the trigger datetime is within 30 seconds
            var dateOfNextDay = GetNextWeekday(DayOfWeekToNotify);
            var dateToNotify = new DateTime(dateOfNextDay.Year, dateOfNextDay.Month, dateOfNextDay.Day, TimeToNotify.Hour, TimeToNotify.Minute, TimeToNotify.Second);
            double totalMinutesSeperation = dateToNotify.Subtract(DateTime.UtcNow).TotalMinutes;
            IEnumerable<DataAccess.PushSubscription> subscribers = new List<DataAccess.PushSubscription>();
            if (Math.Abs(totalMinutesSeperation) < (15 * 60))
            {
                var hoursSeperation = totalMinutesSeperation / 60;
                // less than 15 hours either side of UTC, so lets start checking
                subscribers = context.PushSubscriptions.Where(ps => Math.Abs(ps.TimeOffset.TotalHours - hoursSeperation) < (1D / 60D));
            }
            var pushNotificationManager = new PushNotificationManager();
            var pushMessage = new PushMessage("ITS BEER TIME")
            {
                Topic = "Beer",                
                Urgency = PushMessageUrgency.Normal,
            };

            foreach (var pushSubscription in subscribers)
            {
                pushNotificationManager.SendNotificationAsync(pushSubscription.ToPushSubscription(context.PushSubscriptionKeys.Where(psk => psk.Endpoint == pushSubscription.Endpoint)), pushMessage, CancellationToken.None).Wait();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
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

        private DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime result = DateTime.Now;
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }
    }
}
