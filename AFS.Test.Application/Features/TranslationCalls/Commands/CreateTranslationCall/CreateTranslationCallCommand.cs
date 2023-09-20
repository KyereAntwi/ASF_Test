using AFS.Test.Application.Contracts.Persistence;
using MediatR;
using AFS.Test.Domain.Entities;
using AutoMapper;

namespace AFS.Test.Application.Features.TranslationCalls.Commands.CreateTranslationCall;

public record CreateTranslationCallCommand(
    string User,
    string Translation,
    string Translated,
    string Text
) : IRequest<TranslationCall?>;

public class CreateTranslationCallCommandHandler : IRequestHandler<CreateTranslationCallCommand, TranslationCall?>
{
    private readonly ITranslationCallRepository _asyncRepository;
    private readonly IMapper _mapper;

    public CreateTranslationCallCommandHandler(ITranslationCallRepository asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }
    
    public async Task<TranslationCall?> Handle(CreateTranslationCallCommand request, CancellationToken cancellationToken)
    {
        var translationCall = _mapper.Map<TranslationCall>(request);
        var translationCallResult = await _asyncRepository.AddAsync(translationCall);
        return translationCallResult;
    }
}