using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is a required field")]
        [MaxLength(10)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Display order is a required field")]
        [Range(1, 200)]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
