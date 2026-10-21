using MeasureItAll.Dtos;
using MeasureItAll.Options;
using Microsoft.Extensions.Options;

namespace MeasureItAll.WebServices;

// Calls the customer API and wraps the responses in DTOs.
// The HttpClient's base address and ApiKey header are configured in Program.cs.
public class CustomerApiWebService : ICustomerApiWebService
{
    private readonly HttpClient httpClient;
    private readonly CustomerApiOptions customerApiOptions;

    public CustomerApiWebService(HttpClient httpClient, IOptions<CustomerApiOptions> customerApiOptions)
    {
        this.httpClient = httpClient;
        this.customerApiOptions = customerApiOptions.Value;
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
        var requestPath = $"api/v{customerApiOptions.ApiVersion}/{resourceName}";
        var items = await httpClient.GetFromJsonAsync<List<TDto>>(requestPath);
        return items ?? new List<TDto>();
    }
}
