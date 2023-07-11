using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [DisplayName("Display Order")]
        
        public string DisplayOrder { get; set; } = null!;

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;



    }
}
