using MeasureItAll.Models;
using MeasureItAll.WebServices;

namespace MeasureItAll.Services;

public class SalesPersonService : ISalesPersonService
{
    private readonly IWingineerApiWebService wingineerApiWebService;

    public SalesPersonService(IWingineerApiWebService wingineerApiWebService)
    {
        this.wingineerApiWebService = wingineerApiWebService;
    }

    public async Task<IReadOnlyList<SalesPersonOrderCountModel>> GetSalesPersonOrderCountsAsync()
    {
        var salesPersons = await wingineerApiWebService.GetSalesPersonsAsync();
        var orderLines = await wingineerApiWebService.GetOrderLinesAsync();

        var orderCountsBySalesPersonId = orderLines
            .GroupBy(orderLine => orderLine.SalesPersonId)
            .ToDictionary(orderLineGroup => orderLineGroup.Key, orderLineGroup => orderLineGroup.Count());

        return salesPersons
            .Select(salesPerson => new SalesPersonOrderCountModel
            {
                SalesPersonId = salesPerson.Id,
                Name = salesPerson.Name,
                OrderCount = orderCountsBySalesPersonId.GetValueOrDefault(salesPerson.Id)
            })
            .ToList();
    }

    public async Task<SalesPersonDetailsModel?> GetSalesPersonDetailsAsync(int salesPersonId)
    {
        var salesPersons = await wingineerApiWebService.GetSalesPersonsAsync();
        var salesPerson = salesPersons.FirstOrDefault(candidate => candidate.Id == salesPersonId);
        if (salesPerson is null)
        {
            return null;
        }

        var orderLines = await wingineerApiWebService.GetOrderLinesAsync();
        var salesPersonOrders = orderLines
            .Where(orderLine => orderLine.SalesPersonId == salesPersonId)
            .ToList();

        return new SalesPersonDetailsModel
        {
            SalesPerson = salesPerson,
            Orders = salesPersonOrders
        };
    }
}
