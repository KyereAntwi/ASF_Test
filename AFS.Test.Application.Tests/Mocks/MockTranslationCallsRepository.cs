using AFS.Test.Application.Contracts.Persistence;
using AFS.Test.Domain.Entities;
using Moq;

namespace AFS.Test.Application.Tests.Mocks;

public static class MockTranslationCallsRepository
{
    public static Mock<ITranslationCallRepository> GetListRepository()
    {

        ICollection<TranslationCall> TranslationCalls = new List<TranslationCall>()
        {
            new TranslationCall()
            {
                Id = new Guid("8245fe4a-d402-451c-b9ed-9c1a04247482"),
                Translated = "uiio lkisd ksnd i  ls kdfsl lsdfkdjs",
                Translation = "minions",
                TimeStamp = DateTime.Now,
                User = "user@example.com",
                Text = "Some basic sample"
            },
            new TranslationCall()
            {
                Id = new Guid("8245fe4a-d402-451c-b9ed-9c1a04247482"),
                Translated = "uiio lkisd ksnd i  ls kdfsl lsdfkdjs",
                Translation = "minions",
                TimeStamp = DateTime.Now,
                User = "john@example.com",
                Text = "Some basic sample 2"
            }
        };

        var mockRepo = new Mock<ITranslationCallRepository>();

        mockRepo.Setup(r => 
            r.FilterListAsync(1, 2, "", "", new DateTime()))
            .ReturnsAsync((int page, int size, string keyword, string type, DateTime date) =>
            {

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    TranslationCalls = TranslationCalls
                        .Where(l => l.Text.ToLower().Contains(keyword.ToLower()) ||
                                    l.User.ToLower().Contains(keyword.ToLower())).ToList();
                }

                if (!string.IsNullOrWhiteSpace(type))
                {
                    TranslationCalls = TranslationCalls.Where(l => l.Translation.ToLower().Contains(type.ToLower())).ToList();
                }

                if (date != new DateTime())
                {
                    TranslationCalls = TranslationCalls.Where(l => l.TimeStamp.Date == date.Date).ToList();
                }

                TranslationCalls = TranslationCalls.Skip((page - 1) * size).Take(size).ToList();

                IReadOnlyList<TranslationCall> Translations = new List<TranslationCall>();
                Translations = TranslationCalls.ToList();

                return Translations;
            });

        mockRepo.Setup(r =>
            r.AddAsync(It.IsAny<TranslationCall>())).ReturnsAsync((TranslationCall translationCall) =>
        {
            translationCall.Id = new Guid("2245fe4a-d402-451c-b9ed-9c1a04247482");
            TranslationCalls.Add(translationCall);
            return translationCall;
        });

        mockRepo.Setup(r => r.ListCountAsync()).ReturnsAsync(TranslationCalls.Count);

        return mockRepo;
    }
}