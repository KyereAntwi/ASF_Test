namespace AFS.Test.Presentation.WebApp.Models;

public class HomeViewModel
{
    public int Count { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public IEnumerable<TranslationCallViewModel> ListItems = new List<TranslationCallViewModel>();

    public int NextPage => (Size * Page) < Count ? Page + 1 : 0;
    public int PrevPage => Page > 1 ? Page - 1 : 0;
}