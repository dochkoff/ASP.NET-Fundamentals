using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Infrastructure.Data.Configuration
{
    public class PostConfugation : IEntityTypeConfiguration<Post>
    {
        private Post[] initialPosts = new Post[]
        {
                new Post() {Id = 1, Title = "My Furst Post", Content = "My First Post Content"},
                new Post() {Id = 2, Title = "My Second Post", Content = "My Second Post Content"},
                new Post() {Id = 3, Title = "My Third Post", Content = "My Third Post Content"},
        };
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(initialPosts);
        }
    }
}
