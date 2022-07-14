using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MusicQueues.Application.Common.Interfaces;

public interface ICallbackHandler
{
    public IEnumerable<string> Handles { get; }
    public Task<IActionResult> Handle(HttpContext context);
}