using MeasureItAll.Models;
using MeasureItAll.WebServices;

namespace MeasureItAll.Services;

public class SalesPersonService : ISalesPersonService
{
    private readonly ICustomerApiWebService customerApiWebService;

    public SalesPersonService(ICustomerApiWebService customerApiWebService)
    {
        this.customerApiWebService = customerApiWebService;
    }

    public async Task<IReadOnlyList<SalesPersonOrderCountModel>> GetSalesPersonOrderCountsAsync()
    {
        var salesPersons = await customerApiWebService.GetSalesPersonsAsync();
        var orderLines = await customerApiWebService.GetOrderLinesAsync();

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
        var salesPersons = await customerApiWebService.GetSalesPersonsAsync();
        var salesPerson = salesPersons.FirstOrDefault(candidate => candidate.Id == salesPersonId);
        if (salesPerson is null)
        {
            return null;
        }

        var orderLines = await customerApiWebService.GetOrderLinesAsync();
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
