using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static Forum.bg.Constants.DataConstants.Post;

namespace Forum.bg.Data.Models
{
    [Comment("Published post")]
    public class Post
    {
        [Comment("Post identifier")]
        [Key]
        public int Id { get; set; }

        [Comment("Post title")]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Comment("Content")]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        [Comment("Marks entry to deleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
