using MeasureItAll.Dtos;
using MeasureItAll.Options;
using Microsoft.Extensions.Options;

namespace MeasureItAll.WebServices;

// Calls the Wingineer API and wraps the responses in DTOs.
// The HttpClient's base address and ApiKey header are configured in Program.cs.
public class WingineerApiWebService : IWingineerApiWebService
{
    private readonly HttpClient httpClient;
    private readonly WingineerApiOptions wingineerApiOptions;

    public WingineerApiWebService(HttpClient httpClient, IOptions<WingineerApiOptions> wingineerApiOptions)
    {
        this.httpClient = httpClient;
        this.wingineerApiOptions = wingineerApiOptions.Value;
    }

    public Task<IReadOnlyList<SalesPersonDto>> GetSalesPersonsAsync()
    {
        return GetCollectionAsync<SalesPersonDto>("SalesPersons");
    }

    public Task<IReadOnlyList<OrderLineDto>> GetOrderLinesAsync()
    {
        return GetCollectionAsync<OrderLineDto>("Orderlines");
    }

    private async Task<IReadOnlyList<TDto>> GetCollectionAsync<TDto>(string resourceName)
    {
        var requestPath = $"api/v{wingineerApiOptions.ApiVersion}/{resourceName}";
        var items = await httpClient.GetFromJsonAsync<List<TDto>>(requestPath);
        return items ?? new List<TDto>();
    }
}
