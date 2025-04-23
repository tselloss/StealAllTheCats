using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.DBHelper.Models
{
    public class TagEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public ICollection<CatTag> CatTags { get; set; } = new List<CatTag>();
    }
}
