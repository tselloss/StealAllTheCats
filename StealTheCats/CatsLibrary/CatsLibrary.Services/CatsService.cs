using CatsLibrary.Interface;
using CatsLibrary.Interface.Models;
using DatabaseContext.DBHelper.Methods;
using DatabaseContext.DBHelper.Models;
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

            if (cats == null) return;

            foreach (var cat in cats)
            {
                if (!_dbHelper.Cats.Any(x => x.CatId == cat.CatId))
                {
                    // Initialize new Cat
                    var newCat = new CatEntity
                    {
                        CatId = cat.CatId,
                        Height = cat.Height,
                        Image = cat.Image,
                        Width = cat.Width,
                    };
                    _dbHelper.Cats.Add(newCat);


                    // Split the name with temperament values
                    var listOfTemperament = NameTemperament(cat.Tags[0].Name);
                    foreach (var temperament in listOfTemperament)
                    {
                        // FirstOrDefault value of List of temperament
                        var tag = _dbHelper.Tags.FirstOrDefault(t => t.Name == temperament);
                        if (tag == null)
                        {
                            tag = new TagEntity { Name = temperament };
                            _dbHelper.Tags.Add(tag);
                        }

                        
                        _dbHelper.CatTags.Add(new CatTag
                        {
                            Cat = newCat,
                            Tag = tag
                        });
                    }

                }
                await _dbHelper.SaveChangesAsync();
            }
        }


        private List<string> NameTemperament(string temperament)
        { 
            var spitTheTemperament = temperament.Split(',').ToList();

            return spitTheTemperament;
        }
    }
}
