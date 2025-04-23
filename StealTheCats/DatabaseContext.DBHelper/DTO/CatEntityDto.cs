namespace DatabaseContext.DBHelper.DTO
{
    public class CatEntityDto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BreedsDto> Breeds { get; set; }
    }
}
