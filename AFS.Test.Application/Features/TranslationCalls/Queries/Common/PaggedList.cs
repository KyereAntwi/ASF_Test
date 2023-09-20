namespace AFS.Test.Application.Features.TranslationCalls.Queries.Common;

public class PaggedList<T> where T : class
{
    public int Count { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public IReadOnlyList<T>? ListItems { get; set; }
}