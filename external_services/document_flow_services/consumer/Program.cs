using Consumer;
using Consumer.Services;
using Consumer.Core.Application;
using Consumer.Core.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

DirectoryInfo mailConfigurationDirectory = Startup.GetMailConfiguration();

builder.Configuration
    .AddJsonFile( mailConfigurationDirectory.GetFiles()
        .Where(x => x.Extension == ".json")
        .Select(x => x.FullName)
        .First() 
        );

builder.Configuration.AddJsonFile("./message_conf/ticket_conf.json");

builder.Services.Configure<MailConfiguration>( builder.Configuration );
builder.Services.Configure<MailMessageSettings>( builder.Configuration );

builder.Services.AddTransient<IRSAWorker, RSAWorker>( 
    (services) => new RSAWorker
        (
            2048, File.ReadAllText(
                    mailConfigurationDirectory.GetFiles()
                    .Where(x => x.Name == "privatekey.pem")
                    .Select(x => x.FullName)
                    .First()
                )
        ) 
);

builder.Services.AddTransient<ServiceMailMessageBuilder>();

builder.Services.AddScoped<FileGenerator>();

builder.Services.AddScoped<IMailHost, MailHost>();


var app = builder.Build();

if (app.Environment.IsDevelopment()){
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
