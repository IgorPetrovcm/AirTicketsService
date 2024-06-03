using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Producer.Core.Application;
using Producer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions( (options) => 
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        );
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRabbitHost, RabbitHost>( (services) =>
    new RabbitHost(
            host: "localhost",
            username: Environment.GetEnvironmentVariable("DATABUS_USERNAME"),
            password: Environment.GetEnvironmentVariable("DATABUS_PASSWORD"),
            port: 5672 
        )
);

builder.Services.AddScoped<IMessageSendingWorker, MessageSendingWorker>();

var app = builder.Build();

if ( app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
