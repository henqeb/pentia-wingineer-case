using System.Globalization;

namespace MeasureItAll.Models;

public class MonthlyOrderVolumeModel
{
    public int Year { get; init; }

    public int Month { get; init; }

    public int OrderCount { get; init; }

    public string MonthName => CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Month);
}
