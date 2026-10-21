using MeasureItAll.Models;

namespace MeasureItAll.Services;

public interface ISalesPersonService
{
    Task<IReadOnlyList<SalesPersonOrderCountModel>> GetSalesPersonOrderCountsAsync();

    Task<SalesPersonDetailsModel?> GetSalesPersonDetailsAsync(int salesPersonId);
}
