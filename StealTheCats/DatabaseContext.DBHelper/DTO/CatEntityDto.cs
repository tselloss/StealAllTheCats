using System.Text.Json.Serialization;

namespace DatabaseContext.DBHelper.DTO
{
    public class CatEntityDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        [JsonIgnore]
        public List<BreedsDto> Breeds { get; set; }
    }
}
