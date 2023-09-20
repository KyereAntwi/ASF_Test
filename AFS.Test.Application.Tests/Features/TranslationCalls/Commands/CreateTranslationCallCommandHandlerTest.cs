using AFS.Test.Application.Contracts.Persistence;
using AFS.Test.Application.Features.TranslationCalls.Commands.CreateTranslationCall;
using AFS.Test.Application.Profiles;
using AFS.Test.Application.Tests.Mocks;
using AFS.Test.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AFS.Test.Application.Tests.Features.TranslationCalls.Commands;

public class CreateTranslationCallCommandHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ITranslationCallRepository> _mockRepository;
    
    public CreateTranslationCallCommandHandlerTest()
    {
        _mockRepository = MockTranslationCallsRepository.GetListRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfiles>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateTranslationCall_Handle_ReturnTranslationCall()
    {
        var handler =new CreateTranslationCallCommandHandler(_mockRepository.Object, _mapper);

        var command = new CreateTranslationCallCommand(
            "user@example.com",
            "minions",
            "uil idi idsods iye  mine",
            "some simple example"
            );

        var result = 
            await handler.Handle(command, CancellationToken.None);

        var list = await _mockRepository.Object.ListCountAsync();

        result.Should().BeOfType<TranslationCall>();
    }
}