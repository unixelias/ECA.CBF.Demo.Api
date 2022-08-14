using Microsoft.AspNetCore.Mvc;

namespace ECA.CBF.Demo.Api.Controllers.v1;


[ApiVersion("1.0")]
[ApiController]
[AllowAnonymous]
[Route("api/v{version:apiVersion}/[controller]")]
public class TeamController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
