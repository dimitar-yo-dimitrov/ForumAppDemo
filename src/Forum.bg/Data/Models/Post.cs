using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static Forum.bg.Constants.DataConstants.Post;

namespace Forum.bg.Data.Models
{
    [Comment("Published post")]
    public class Post
    {
        [Key]
        [Comment("Post identifier")]
        public int Id { get; set; }

        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        [Comment("Post title")]
        public string Title { get; set; } = null!;

        [MinLength(ContentMinLength)]
        [MaxLength(ContentMaxLength)]
        [Comment("Content")]
        public string Content { get; set; } = null!;
    }
}
