using CsvHelper.Configuration;
using Domain.Entities;

namespace Domain.Helpers
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Map(u => u.Name).Name("name");
            Map(u => u.City).Name("city");
            Map(u => u.Country).Name("country");
            Map(u => u.FavoriteSport).Name("favorite_sport");
        }
    }
}
