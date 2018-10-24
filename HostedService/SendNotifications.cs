using IsItBeerOclock.API.DataAccess;
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
        private readonly DateTime DATE_TO_NOTIFY = new DateTime(2018, 10, 24, 22, 00, 00);

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
            double totalMinutesSeperation = DATE_TO_NOTIFY.Subtract(DateTime.UtcNow).TotalMinutes;

            if (Math.Abs(totalMinutesSeperation) < (15 * 60))
            {
                var hoursSeperation = totalMinutesSeperation / 60;
                // less than 15 hours either side of UTC, so lets start checking
                var subscribers = context.PushSubscriptions.Where(ps => Math.Abs(ps.TimeOffset.TotalHours - hoursSeperation) < (1D / 60D));
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
    }
}
