using CatsLibrary.Interface;
using CatsLibrary.Interface.Models;
using DatabaseContext.DBHelper.DTO;
using DatabaseContext.DBHelper.Methods;
using DatabaseContext.DBHelper.Models;
using Extensions.HttpClient;
using System.Globalization;
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

        public Task<CatEntityDto> GetCatById(int id)
        {
            var cat = _dbHelper.Cats.FirstOrDefault(x => x.Id == id);

            if (cat == null)
            {
                return Task.FromResult<CatEntityDto>(new CatEntityDto { });
            }

            var returnCat = new CatEntityDto
            {
                Id = cat.Id,
                Height = cat.Height,
                Url = cat.Image,
                Width = cat.Width
            };

            return Task.FromResult(returnCat);
        }

        public IEnumerable<CatEntityDto> GetCatsByPages(int page, int pageSize)
        {
            var cats = _dbHelper.Cats
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Select(cat => new CatEntityDto
                                 {
                                     Id = cat.Id,
                                     Height = cat.Height,
                                     Url = cat.Image,
                                     Width = cat.Width
                                 })
                                 .ToList();

            return cats;
        }

        public Task<IEnumerable<CatEntityDto>> GetCatsByPages(int page, int pageSize, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return Task.FromResult<IEnumerable<CatEntityDto>>(new List<CatEntityDto>());

            // Make the tag with the correct format
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string normalizedTag = textInfo.ToTitleCase(tag.ToLower());

            var getTag = _dbHelper.Tags.FirstOrDefault(x => x.Name == normalizedTag);

            if (getTag == null)
            {
                return Task.FromResult<IEnumerable<CatEntityDto>>(new List<CatEntityDto>());
            }

            var catIds = _dbHelper.CatTags
                          .Where(ct => ct.TagId == getTag.Id)
                          .Select(ct => ct.CatId);

            var cats = _dbHelper.Cats
                          .Where(c => catIds.Contains(c.Id))
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .Select(cat => new CatEntityDto
                          {
                              Id = cat.Id,
                              Height = cat.Height,
                              Url = cat.Image,
                              Width = cat.Width
                          })
                          .ToList();

            return Task.FromResult<IEnumerable<CatEntityDto>>(cats);
        }

        private List<string> NameTemperament(string temperament)
        {
            var spitTheTemperament = temperament.Split(',').ToList();

            return spitTheTemperament;
        }
    }
}
