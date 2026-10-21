using MeasureItAll.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeasureItAll.Controllers;

public class OrderVolumeController : Controller
{
    private readonly IOrderVolumeService orderVolumeService;

    public OrderVolumeController(IOrderVolumeService orderVolumeService)
    {
        this.orderVolumeService = orderVolumeService;
    }

    public async Task<IActionResult> Index()
    {
        var monthlyOrderVolumes = await orderVolumeService.GetMonthlyOrderVolumesAsync();
        return View(monthlyOrderVolumes);
    }
}
