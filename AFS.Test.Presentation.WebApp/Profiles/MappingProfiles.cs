using AFS.Test.Application.Features.TranslationCalls.Queries.Common;
using AFS.Test.Domain.Entities;
using AFS.Test.Presentation.WebApp.Models;
using AutoMapper;

namespace AFS.Test.Presentation.WebApp.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PaggedList<TranslationCall>, HomeViewModel>();
        CreateMap<TranslationCall, TranslationCallViewModel>();
    }
}