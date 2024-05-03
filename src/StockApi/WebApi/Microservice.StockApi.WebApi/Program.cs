using MassTransit;
using Microservice.Common.Constants.RabbitMQ;
using Microservice.StockApi.WebApi.Consumers;
using Microservice.StockApi.Infrastructure.Persistence.Extensions;
using Microservice.StockApi.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x => {
    x.AddConsumer<OrderCreatedEventConsumer>();
    x.AddConsumer<PaymentFailedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockOrderCreatedEventQueueName, e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockPaymentFailedEventQueueName, e =>
        {
            e.ConfigureConsumer<PaymentFailedEventConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddApplicationRegistration(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
