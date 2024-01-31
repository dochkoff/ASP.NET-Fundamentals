using static ForumApp.Infrastructure.Constants.ValidationConstants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForumApp.Infrastructure.Data.Models
{
    [Comment("Posts Table")]
    public class Post
    {
        [Key]
        [Comment("Post Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Post Titile")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ContentMaxLength)]
        [Comment("Post Content")]
        public string Content { get; set; } = string.Empty;
    }
}
