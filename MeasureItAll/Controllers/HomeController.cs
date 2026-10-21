using MeasureItAll.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeasureItAll.Controllers;

public class HomeController : Controller
{
    private readonly ISalesPersonService salesPersonService;

    public HomeController(ISalesPersonService salesPersonService)
    {
        this.salesPersonService = salesPersonService;
    }

    public async Task<IActionResult> Index()
    {
        var salesPersonOrderCounts = await salesPersonService.GetSalesPersonOrderCountsAsync();
        return View(salesPersonOrderCounts);
    }

    public IActionResult Error()
    {
        return View();
    }
}
