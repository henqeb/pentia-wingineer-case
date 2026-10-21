using MeasureItAll.Dtos;

namespace MeasureItAll.WebServices;

public interface ICustomerApiWebService
{
    Task<IReadOnlyList<SalesPersonDto>> GetSalesPersonsAsync();

    Task<IReadOnlyList<OrderLineDto>> GetOrderLinesAsync();
}
