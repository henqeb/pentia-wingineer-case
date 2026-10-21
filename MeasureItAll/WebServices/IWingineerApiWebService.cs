using MeasureItAll.Dtos;

namespace MeasureItAll.WebServices;

public interface IWingineerApiWebService
{
    Task<IReadOnlyList<SalesPersonDto>> GetSalesPersonsAsync();

    Task<IReadOnlyList<OrderLineDto>> GetOrderLinesAsync();
}
