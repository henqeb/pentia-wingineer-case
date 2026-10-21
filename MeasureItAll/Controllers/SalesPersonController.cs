using MeasureItAll.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeasureItAll.Controllers;

public class SalesPersonController : Controller
{
    private readonly ISalesPersonService salesPersonService;

    public SalesPersonController(ISalesPersonService salesPersonService)
    {
        this.salesPersonService = salesPersonService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var salesPersonDetails = await salesPersonService.GetSalesPersonDetailsAsync(id);
        if (salesPersonDetails is null)
        {
            return NotFound();
        }

        return View(salesPersonDetails);
    }
}
