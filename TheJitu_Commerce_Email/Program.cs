using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Email.Data;
using TheJitu_Commerce_Email.Exentions;
using TheJitu_Commerce_Email.Messaging;
using TheJitu_Commerce_Email.Messaging.IMessaging;
using TheJitu_Commerce_Products.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to Db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//services
builder.Services.AddSingleton<IAzureMessageBusConsumer, AzureMessageBusConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMigration();

app.useAzure();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
