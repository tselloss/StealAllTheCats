using System.Text.Json.Serialization;

namespace CatsLibrary.Interface.Models
{
    public class CatsAPIRetrieveModel
    {
        [JsonPropertyName("id")]
        public string CatId { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("url")]
        public string Image { get; set; }

        [JsonPropertyName("breeds")]
        public List<TagAPIRetrieveModel> Tags { get; set; }
    }

    public class TagAPIRetrieveModel
    {
        [JsonPropertyName("temperament")]
        public string Name { get; set; }
    }
}
