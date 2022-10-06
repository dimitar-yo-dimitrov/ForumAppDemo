using Forum.bg.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.bg.Data.Configure
{
    /// <summary>
    /// Seed posts
    /// </summary>
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            List<Post> posts = GetPost();

            builder.HasData(posts);
        }

        private List<Post> GetPost()
        {
            return new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first post will be about performing CRUD operations in MVC."
                },

                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "This is my second post.CRUD operation in MVC."
                },

                new Post()
                {
                    Id = 3,
                    Title = "My third post",
                    Content = "This is my third post.CRUD operation in MVC."
                }
            };
        }
    }
}
