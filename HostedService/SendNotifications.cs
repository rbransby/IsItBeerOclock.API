using IsItBeerOclock.API.DataAccess;
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
        private DataContext _dataContext;

        public SendNotifications(ILogger<SendNotifications> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
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

        private async Task SendNotificationAsync(PushSubscription subscription, PushMessage message, CancellationToken cancellationToken)
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
