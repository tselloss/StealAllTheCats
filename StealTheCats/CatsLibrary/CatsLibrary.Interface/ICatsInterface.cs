using DatabaseContext.DBHelper.DTO;

namespace CatsLibrary.Interface
{
    public interface ICatsInterface
    {
        public Task FetchAndSaveTheCats();

        public Task<CatEntityDto> GetCatById(int id);
    }
}
