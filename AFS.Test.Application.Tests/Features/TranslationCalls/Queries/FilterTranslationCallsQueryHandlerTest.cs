using AFS.Test.Application.Contracts.Persistence;
using AFS.Test.Application.Features.TranslationCalls.Queries.Common;
using AFS.Test.Application.Features.TranslationCalls.Queries.FilterTranslationCalls;
using AFS.Test.Application.Tests.Mocks;
using AFS.Test.Domain.Entities;
using FluentAssertions;
using Moq;

namespace AFS.Test.Application.Tests.Features.TranslationCalls.Queries;

public class FilterTranslationCallsQueryHandlerTest
{
    private readonly Mock<ITranslationCallRepository> _mockRepository;

    public FilterTranslationCallsQueryHandlerTest()
    {
        _mockRepository = MockTranslationCallsRepository.GetListRepository();
    }

    [Fact]
    public async Task FilterTranslationCallsQuery_Handler_ReturnPagedListOfTranslationCalls()
    {
        var handler = new FilterTranslationCallsQueryHandler(_mockRepository.Object);

        var query = new FilterTranslationCallsQuery(
            1,
            2,
            "",
            "",
            new DateTime());

        var result = await handler.Handle(query, CancellationToken.None);

        result.Count.Should().Be(2);
    }
}