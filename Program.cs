using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// CoreWCF configuration
builder.Services.AddServiceModelServices()
    .AddServiceModelMetadata()
    .AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>()
    .AddSingleton<CSVService>();

var app = builder.Build();

// Configure metadata behavior
var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

// Configure service endpoints
app.UseServiceModel(builder =>
{
    builder.AddService<CSVService>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = 
            app.Environment.IsDevelopment();
    });

    builder.AddServiceEndpoint<CSVService, ICSVService>(
        new BasicHttpBinding(), 
        "/CSVService"
    );
});

app.Run();