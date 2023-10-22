using Domain.Entities;

namespace Application.IServices
{
    public interface ICsvLoaderService
    {
        (List<User> uniqueUsers, List<User> duplicateUsers) ReadCsvFile(Stream csvStream);
        void SaveUsers(List<User> users);
        List<User> SearchUsers(string query);
    }
}
