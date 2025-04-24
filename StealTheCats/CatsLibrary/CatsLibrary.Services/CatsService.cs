using CatsLibrary.Interface;
using CatsLibrary.Interface.Models;
using DatabaseContext.DBHelper.Methods;
using Extensions.HttpClient;
using System.Text.Json;

namespace CatsLibrary.Services
{
    public class CatsService : ICatsInterface
    {
        private readonly HttpHelper _httpHelper;
        private readonly DbHelper _dbHelper;

        public CatsService(HttpHelper httpHelper, DbHelper dbHelper)
        {
            _httpHelper = httpHelper;
            _dbHelper = dbHelper;
        }

        public async Task FetchAndSaveTheCats()
        {
            var catsList = await _httpHelper.GetContent();

            var cats = JsonSerializer.Deserialize<List<CatsAPIRetrieveModel>>(catsList, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


        }
    }
}
