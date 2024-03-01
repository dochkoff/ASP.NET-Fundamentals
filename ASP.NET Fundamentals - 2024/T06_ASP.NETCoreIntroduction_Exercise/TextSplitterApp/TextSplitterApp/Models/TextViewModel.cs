using System.ComponentModel.DataAnnotations;

namespace TextSplitterApp.Models
{
    public class TextViewModel
    {
        [Required]
        [StringLength(30,
            ErrorMessage = "The field {0} must be a string between {2} and {1} characters",
            MinimumLength = 2)]
        public string Text { get; set; } = string.Empty;
        public string SplitText { get; set; } = string.Empty;
    }
}
