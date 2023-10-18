using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using TheJitu_Commerce_Email.Messaging.IMessaging;
using TheJitu_Commerce_Email.Model.Dtos;
using TheJitu_Commerce_Email.Services;

namespace TheJitu_Commerce_Email.Messaging
{
    public class AzureMessageBusConsumer : IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly string QueueName;
        private readonly ServiceBusProcessor _registrationProcessor;
        private readonly EmailSendService _emailSendService;
        public AzureMessageBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();
            QueueName = _configuration.GetSection("QueuesAndTopics:RegisterUser").Get<string>();
            var serviceBusClient = new ServiceBusClient(ConnectionString);
            _registrationProcessor = serviceBusClient.CreateProcessor(QueueName);
            _emailSendService = new EmailSendService();
        }

        public async Task Start()
        {
            //start processing
            _registrationProcessor.ProcessMessageAsync += OnRegistration;
            _registrationProcessor.ProcessErrorAsync += ErrorHandler;
            await _registrationProcessor.StartProcessingAsync();

        }

        public async Task Stop()
        {
            //stop processing
            await _registrationProcessor.StopProcessingAsync();
            await _registrationProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            //TODO Send an email to admin
            throw new NotImplementedException();
        }

        private async Task OnRegistration(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var userMessage = JsonConvert.DeserializeObject<UserMessageDto>(body);

            //TODO send an email

            try 
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2023/04/20/10/19/coding-7939372_1280.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to The Jitu Shopping Site ");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p> Start Shopping here</p>");
                await _emailSendService.SendEmail(userMessage, stringBuilder.ToString());
                //you can delete the message from the queue
                await args.CompleteMessageAsync(message);
            } catch (Exception e) 
            {
                
            }

        }

        
    }
}
