using Domain.Entities;
using Domain.IRepositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICsvLoaderContext _context;

        public UserRepository(ICsvLoaderContext context) => _context = context;

        public List<User> SearchUsers(string query)
        {
            query = query.ToLower();

            var result = _context.Users.Where(user =>
        user.Name.ToLower().Contains(query) ||
        user.City.ToLower().Contains(query) ||
        user.Country.ToLower().Contains(query) ||
        user.FavoriteSport.ToLower().Contains(query)).ToList();

            return result;
        }

        public void SaveUsers(List<User> users)
        {
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        public bool UserExists(string name, string city, string country, string favoriteSport)
        {
            return _context.Users.Any(u =>
                u.Name == name &&
                u.City == city &&
                u.Country == country &&
                u.FavoriteSport == favoriteSport);
        }

    }
}
