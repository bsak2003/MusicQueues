using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Api.Controllers;

[ApiController]
[Route("callback")]
public class CallbackController : ControllerBase
{
    private readonly IEnumerable<ICallbackHandler> _handlers;
    
    public CallbackController(IEnumerable<ICallbackHandler> handlers)
    {
        _handlers = handlers;
    }
    
    [HttpGet]
    public Task<IActionResult> HandleCallback(string type)
    {
        return _handlers.First(x => x.Handles.Contains(type)).Handle(HttpContext);
    }
}