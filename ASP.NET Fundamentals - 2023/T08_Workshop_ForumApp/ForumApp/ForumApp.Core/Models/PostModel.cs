using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ForumApp.Infrastructure.Constants.ValidationConstants;

namespace ForumApp.Core.Models
{
    /// <summary>
    /// Post data transfer model
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Post Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Post title
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(TitleMaxLength,
            MinimumLength =TitleMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Post Content
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(ContentMaxLength,
            MinimumLength = ContentMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;
    }
}
