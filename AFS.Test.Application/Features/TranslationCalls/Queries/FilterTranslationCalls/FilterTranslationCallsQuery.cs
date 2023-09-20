using AFS.Test.Application.Contracts.Persistence;
using AFS.Test.Application.Features.TranslationCalls.Queries.Common;
using AFS.Test.Domain.Entities;
using MediatR;

namespace AFS.Test.Application.Features.TranslationCalls.Queries.FilterTranslationCalls;

public record FilterTranslationCallsQuery(int Page, int Size, string Text, string Type, 
    DateTime DateAdded) : IRequest<PaggedList<TranslationCall>>;

public class FilterTranslationCallsQueryHandler : 
    IRequestHandler<FilterTranslationCallsQuery, PaggedList<TranslationCall>>
{
    private readonly ITranslationCallRepository _translationCallRepository;

    public FilterTranslationCallsQueryHandler(ITranslationCallRepository translationCallRepository)
    {
        _translationCallRepository = translationCallRepository;
    }
    
    public async Task<PaggedList<TranslationCall>> Handle(FilterTranslationCallsQuery request, CancellationToken cancellationToken)
    {
        var list = await _translationCallRepository.FilterListAsync(request.Page, request.Size, request.Text, request.Type, request.DateAdded);
        var count = await _translationCallRepository.ListCountAsync();

        return new PaggedList<TranslationCall>()
        {
            Count = count,
            ListItems = list,
            Page = request.Page,
            Size = request.Size
        };
    }
}