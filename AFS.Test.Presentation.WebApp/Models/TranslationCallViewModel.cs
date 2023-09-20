namespace AFS.Test.Presentation.WebApp.Models;

public class TranslationCallViewModel
{
    public Guid Id { get; set; }
    public string User { get; set; } = String.Empty;
    public string Translation { get; set; } = String.Empty;
    public string Text { get; set; } = String.Empty;
    public string Translated { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}