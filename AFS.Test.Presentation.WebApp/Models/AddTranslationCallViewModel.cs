using System.ComponentModel.DataAnnotations;

namespace AFS.Test.Presentation.WebApp.Models;

public class AddTranslationCallViewModel
{
    [Required]
    public string Translation { get; set; } = String.Empty;
    [Required]
    public string Text { get; set; } = String.Empty;
    [Required]
    public string Translated { get; set; } = String.Empty;
}