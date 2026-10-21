using MeasureItAll.Options;
using MeasureItAll.Services;
using MeasureItAll.WebServices;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddOptions<CustomerApiOptions>()
    .Bind(builder.Configuration.GetSection(CustomerApiOptions.SectionName))
    .Validate(
        customerApiOptions => !string.IsNullOrWhiteSpace(customerApiOptions.ApiKey),
        "The customer API key is missing. Set the environment variable CustomerApi__ApiKey.")
    .ValidateOnStart();

builder.Services.AddHttpClient<ICustomerApiWebService, CustomerApiWebService>((serviceProvider, httpClient) =>
{
    var customerApiOptions = serviceProvider.GetRequiredService<IOptions<CustomerApiOptions>>().Value;
    httpClient.BaseAddress = new Uri(customerApiOptions.BaseUrl);
    httpClient.DefaultRequestHeaders.Add("ApiKey", customerApiOptions.ApiKey);
});

builder.Services.AddScoped<ISalesPersonService, SalesPersonService>();
builder.Services.AddScoped<IOrderVolumeService, OrderVolumeService>();

var app = builder.Build();

// Always show the friendly error page, so users never see a stack trace.
app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
