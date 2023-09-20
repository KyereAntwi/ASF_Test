using System.Security.Claims;
using AFS.Test.Application.Features.TranslationCalls.Commands.CreateTranslationCall;
using AFS.Test.Presentation.WebApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFS.Test.Presentation.WebApp.Controllers.Apis;

[Route("api/translations")]
[ApiController]
[Authorize]
public class TranslationCallsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TranslationCallsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateTranslationCall([FromBody] AddTranslationCallViewModel model)
    {
        var user = User;
        
        var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        if (emailClaim != null)
        {
            string userEmail = emailClaim.Value;

            _ = await _mediator.Send(new CreateTranslationCallCommand(userEmail, model.Translation, model.Translated, model.Text));

            return Accepted();
        }
        
        
        return BadRequest();
    }
}