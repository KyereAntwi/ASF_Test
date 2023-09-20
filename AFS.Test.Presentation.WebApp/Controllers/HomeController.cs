using System.Diagnostics;
using AFS.Test.Application.Features.TranslationCalls.Queries.FilterTranslationCalls;
using Microsoft.AspNetCore.Mvc;
using AFS.Test.Presentation.WebApp.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AFS.Test.Presentation.WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public HomeController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] DateTime date = new DateTime(), 
        [FromQuery] string keyword = "", 
        [FromQuery] string type = "")
    {
        HomeViewModel vm = new HomeViewModel();
        
        var translationCallList = await _mediator.Send(new FilterTranslationCallsQuery(page, size,keyword, type, date));

        if (translationCallList.Count > 0)
        {
            vm = _mapper.Map<HomeViewModel>(translationCallList);
        }
        
        return View(vm);
    }
    
    [HttpGet]
    public IActionResult Translate()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}