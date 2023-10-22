using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        List<User> SearchUsers(string query);
        void SaveUsers(List<User> users);
        bool UserExists(string name, string city, string country, string favoriteSport);
    }
}
