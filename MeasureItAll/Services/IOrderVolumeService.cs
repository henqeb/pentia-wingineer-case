using MeasureItAll.Models;

namespace MeasureItAll.Services;

public interface IOrderVolumeService
{
    Task<IReadOnlyList<MonthlyOrderVolumeModel>> GetMonthlyOrderVolumesAsync();
}
