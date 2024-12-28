using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;

var builder = WebApplication.CreateBuilder(args);

// CoreWCF services Adding
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

// Service registration
builder.Services.AddSingleton<CSVService>();

var app = builder.Build();

// WSDL endpoint Configuration
var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

// Service Endpoint
app.UseServiceModel(builder =>
{
    builder.AddService<CSVService>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    builder.AddServiceEndpoint<CSVService, ICSVService>(new BasicHttpBinding(), "/CSVService");
});

// WSDL Endpoint (basic service/wsdl)
app.MapGet("/CSVService/wsdl", async context =>
{
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    var host = $"{context.Request.Scheme}://{context.Request.Host}";
    await serviceMetadataBehavior.HttpGetAsync(new HttpGetRequestContext(context, $"{host}/CSVService"));
});

app.Run();