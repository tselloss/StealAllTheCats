using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.DBHelper.Models
{
    public class CatTag
    {
        public int CatId { get; set; }

        [Required]
        public CatEntity Cat { get; set; }

        public int TagId { get; set; }

        [Required]
        public TagEntity Tag { get; set; }
    }
}
