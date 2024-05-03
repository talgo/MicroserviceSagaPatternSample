using MassTransit;
using Microservice.OrderApi.Infrastructure.Persistence.Extensions;
using Microservice.OrderApi.Application.Extensions;
using Microservice.OrderApi.WebApi.Consumers;
using Microservice.Common.Constants.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationRegistration(builder.Configuration);
builder.Services.AddInfrastructureRegistration(builder.Configuration);

builder.Services.AddMassTransit(x => {
    x.AddConsumer<PaymentComplatedEventConsumer>();
    x.AddConsumer<PaymentFailedEventConsumer>();
    x.AddConsumer<StockNotReservedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderPaymentCompletedEventQueueName, e =>
        {
            e.ConfigureConsumer<PaymentComplatedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderPaymentFailedEventQueueName, e =>
        {
            e.ConfigureConsumer<PaymentFailedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderStockNotReservedEventQueueName, e =>
        {
            e.ConfigureConsumer<StockNotReservedEventConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
