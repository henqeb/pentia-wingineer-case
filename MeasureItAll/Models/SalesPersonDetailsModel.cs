using MeasureItAll.Dtos;

namespace MeasureItAll.Models;

public class SalesPersonDetailsModel
{
    public required SalesPersonDto SalesPerson { get; init; }

    public required IReadOnlyList<OrderLineDto> Orders { get; init; }
}
