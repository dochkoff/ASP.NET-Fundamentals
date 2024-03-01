using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Data.Models
{
    [Comment("Shopping List Product")]
    public class Product
    {
        [Key]
        [Comment("Product Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }=string.Empty;


        [MaxLength(500)] 
        public string? Description { get; set; }

        public List<ProductNote> ProductNotes { get; set; }=new List<ProductNote>();
    }
}
