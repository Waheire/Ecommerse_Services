namespace TheJitu_Commerce_Email.Messaging.IMessaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
