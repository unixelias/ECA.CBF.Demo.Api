using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECA.CBF.Demo.Api.Controllers.v1;

public class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> Logger;

    public BaseController(ILogger<BaseController> logger)
    {
        Logger = logger;
    }

    protected string GetRoute()
    {
        IHttpRequestFeature feature = HttpContext.Features.Get<IHttpRequestFeature>();
        return feature.RawTarget;
    }
}