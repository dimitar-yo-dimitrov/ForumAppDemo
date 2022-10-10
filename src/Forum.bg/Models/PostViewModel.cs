using System.ComponentModel.DataAnnotations;
using static Forum.bg.Constants.DataConstants.Post;

namespace Forum.bg.Models;

/// <summary>
/// Post DTO
/// </summary>
public class PostViewModel
{
    public int Id { get; set; }

    [Display(Name = "Title")]
    [Required(ErrorMessage = "The field {0} is required!")]
    [StringLength(TitleMaxLength,
        MinimumLength = TitleMinLength,
        ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
    public string Title { get; set; } = null!;

    [Display(Name = nameof(Content))]
    [Required(ErrorMessage = "The field {0} is required!")]
    [StringLength(ContentMaxLength,
        MinimumLength = ContentMinLength,
        ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
    public string Content { get; set; } = null!;
}