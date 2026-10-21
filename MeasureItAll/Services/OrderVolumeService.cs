using System.Globalization;
using MeasureItAll.Models;
using MeasureItAll.WebServices;

namespace MeasureItAll.Services;

public class OrderVolumeService : IOrderVolumeService
{
    // The API returns order dates as either "DD-MM-YYYY HH:mm" or "YYYY-MM-DD HH:mm".
    private static readonly string[] OrderDateFormats = { "dd-MM-yyyy HH:mm", "yyyy-MM-dd HH:mm" };

    private readonly IWingineerApiWebService wingineerApiWebService;

    public OrderVolumeService(IWingineerApiWebService wingineerApiWebService)
    {
        this.wingineerApiWebService = wingineerApiWebService;
    }

    // Returns one entry per month, from January of the first order's year up to the last order's month,
    // in chronological order. Months without orders are included with a count of 0.
    public async Task<IReadOnlyList<MonthlyOrderVolumeModel>> GetMonthlyOrderVolumesAsync()
    {
        var orderLines = await wingineerApiWebService.GetOrderLinesAsync();
        var orderMonths = orderLines
            .Select(orderLine => ParseOrderMonth(orderLine.OrderDate))
            .OfType<DateOnly>()
            .ToList();

        if (orderMonths.Count == 0)
        {
            return new List<MonthlyOrderVolumeModel>();
        }

        var orderCountsByMonth = orderMonths
            .GroupBy(orderMonth => orderMonth)
            .ToDictionary(orderMonthGroup => orderMonthGroup.Key, orderMonthGroup => orderMonthGroup.Count());

        var firstMonth = new DateOnly(orderMonths.Min().Year, 1, 1);
        var lastMonth = orderMonths.Max();

        var monthlyOrderVolumes = new List<MonthlyOrderVolumeModel>();
        for (var month = firstMonth; month <= lastMonth; month = month.AddMonths(1))
        {
            monthlyOrderVolumes.Add(new MonthlyOrderVolumeModel
            {
                Year = month.Year,
                Month = month.Month,
                OrderCount = orderCountsByMonth.GetValueOrDefault(month)
            });
        }

        return monthlyOrderVolumes;
    }

    private static DateOnly? ParseOrderMonth(string? orderDate)
    {
        if (!DateOnly.TryParseExact(orderDate?.Trim(), OrderDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedOrderDate))
        {
            return null;
        }

        return new DateOnly(parsedOrderDate.Year, parsedOrderDate.Month, 1);
    }
}
