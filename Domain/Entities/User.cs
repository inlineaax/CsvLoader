using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("User")]
    public class User
    {

        [Name("id")]
        public int Id { get; set; }

        [Name("name")]
        public string Name { get; set; }

        [Name("city")]
        public string City { get; set; }

        [Name("country")]
        public string Country { get; set; }

        [Name("favorite_sport")]
        public string FavoriteSport { get; set; }
    }
}
