using Application.IServices;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Domain.Helpers;
using Domain.IRepositories;
using System.Globalization;


namespace Application.Services
{
    public class CsvLoaderService : ICsvLoaderService
    {
        private readonly IUserRepository _userRepository;
        public CsvLoaderService(IUserRepository userRepository) => _userRepository = userRepository;

        public (List<User> uniqueUsers, List<User> duplicateUsers) ReadCsvFile(Stream csvStream)
        {
            var reader = new CsvReader(new StreamReader(csvStream), new CsvConfiguration(CultureInfo.InvariantCulture));
            reader.Context.RegisterClassMap<UserMap>();
            var users = reader.GetRecords<User>().ToList();

            var uniqueUsers = new List<User>();
            var duplicateUsers = new List<User>();

            foreach (var user in users)
            {
                if (!_userRepository.UserExists(user.Name, user.City, user.Country, user.FavoriteSport))
                {
                    uniqueUsers.Add(user);
                }
                else
                {
                    duplicateUsers.Add(user);
                }
            }

            return (uniqueUsers, duplicateUsers);
        }

        public void SaveUsers(List<User> users)
        {
            _userRepository.SaveUsers(users);
        }

        public List<User> SearchUsers(string query)
        {
            return _userRepository.SearchUsers(query);
        }

    }
}
