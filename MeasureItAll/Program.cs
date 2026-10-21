using MeasureItAll.Options;
using MeasureItAll.Services;
using MeasureItAll.WebServices;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddOptions<WingineerApiOptions>()
    .Bind(builder.Configuration.GetSection(WingineerApiOptions.SectionName))
    .Validate(
        wingineerApiOptions => !string.IsNullOrWhiteSpace(wingineerApiOptions.ApiKey),
        "The Wingineer API key is missing. Set the environment variable WingineerApi__ApiKey.")
    .ValidateOnStart();

builder.Services.AddHttpClient<IWingineerApiWebService, WingineerApiWebService>((serviceProvider, httpClient) =>
{
    var wingineerApiOptions = serviceProvider.GetRequiredService<IOptions<WingineerApiOptions>>().Value;
    httpClient.BaseAddress = new Uri(wingineerApiOptions.BaseUrl);
    httpClient.DefaultRequestHeaders.Add("ApiKey", wingineerApiOptions.ApiKey);
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
