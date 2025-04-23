using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.DBHelper.Models
{
    public class CatEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CatId { get; set; }

        [Required]
        public string Width { get; set; }

        [Required]
        public string Height { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public ICollection<CatTag> CatTags { get; set; } = new List<CatTag>();
    }
}
