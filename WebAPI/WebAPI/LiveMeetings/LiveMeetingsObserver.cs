using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus.Management;

namespace WebAPI.LiveMeetings
{
    public class LiveMeetingObserver : BackgroundService
    {
        private readonly IHubContext<LiveMeetingsHub> _hub;
        private readonly IConfiguration _configuration;

        public LiveMeetingObserver(IHubContext<LiveMeetingsHub> hub,
            IConfiguration configuration)
        {
            _hub = hub;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var subscription = await CreateSubscription();
            //var receiver = CreateReceiver(subscription.SubscriptionName);

            //int clickCount = 0;
            //while (true)
            //{
            //    await Task.Delay(1000);
            //    var messages = await receiver.ReceiveMessagesAsync(1000);
            //    if (messages == null)
            //    {
            //        continue;
            //    }

            //    await _hub.Clients.All.SendAsync("clickCount", clickCount++);

            //    await CompleteMessages(receiver, messages);
            //}

            await Task.CompletedTask;
        }

        private ServiceBusReceiver CreateReceiver(string subscriptionName)
        {
            var client = new ServiceBusClient(_configuration["servicebus:connectionString"]);
            var receiver = client.CreateReceiver(_configuration["ServiceBus:TopicName"], subscriptionName);
            if (receiver == null)
            {
                throw new Exception("Cannot create service bus receiver");
            }

            return receiver;
        }

        private Task<SubscriptionDescription> CreateSubscription()
        {
            var serviceBusClient = new ManagementClient(_configuration["servicebus:connectionString"]);
            var subscriptionName = Guid.NewGuid().ToString();

            var subscriptionDescription = new SubscriptionDescription(
                _configuration["ServiceBus:TopicName"], subscriptionName)
            {
                AutoDeleteOnIdle = TimeSpan.FromMinutes(10),
                MaxDeliveryCount = 1000,
                EnableBatchedOperations = true,
                DefaultMessageTimeToLive = TimeSpan.FromMinutes(1)
            };

            return serviceBusClient.CreateSubscriptionAsync(subscriptionDescription);
        }

        private Task CompleteMessages(ServiceBusReceiver receiver, IReadOnlyList<ServiceBusReceivedMessage> serviceBusReceivedMessages) =>
            Task.WhenAll(serviceBusReceivedMessages.Select(msg => receiver.CompleteMessageAsync(msg)));
    }
}
