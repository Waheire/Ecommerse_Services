using System.Runtime.CompilerServices;
using TheJitu_Commerce_Email.Messaging.IMessaging;

namespace TheJitu_Commerce_Email.Exentions
{
    public static class AzureServiceStarter
    {
        public static IAzureMessageBusConsumer ServiceBusConsumerInstance { get; set; }
        public static IApplicationBuilder useAzure(this IApplicationBuilder app) 
        {
            ServiceBusConsumerInstance = app.ApplicationServices.GetService<IAzureMessageBusConsumer>();
            var HostLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            HostLifetime.ApplicationStarted.Register(OnStart);
            HostLifetime.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStop()
        {
            //stop our email Processor
            ServiceBusConsumerInstance.Stop();
        }

        private static void OnStart()
        {
            //Start our email Processor
            ServiceBusConsumerInstance.Start();
        }
    }
}
