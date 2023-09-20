using AFS.Test.Application.Features.TranslationCalls.Commands.CreateTranslationCall;
using AFS.Test.Domain.Entities;
using AutoMapper;

namespace AFS.Test.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTranslationCallCommand, TranslationCall>();
    }
}