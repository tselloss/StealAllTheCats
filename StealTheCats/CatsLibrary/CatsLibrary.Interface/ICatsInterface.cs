using DatabaseContext.DBHelper.DTO;

namespace CatsLibrary.Interface
{
    public interface ICatsInterface
    {
        public Task FetchAndSaveTheCats();
        public Task<CatEntityDto> GetCatById(int id);
        public IEnumerable<CatEntityDto> GetCatsByPages(int page, int pageSize);
        public Task<IEnumerable<CatEntityDto>> GetCatsByPages(int page, int pageSize, string tag);
    }
}
