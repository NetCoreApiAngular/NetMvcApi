using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HR.DataAccess.Entity;
using HR.DataAccess.Extensions;

namespace HR.DataAccess.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User RetrieveUser(int userId);
        List<User> SearchUser(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage);
        User GetUserByEmail(string email);
        User GetUserByUserName(string username);

    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HrManagementContext context)
            : base(context)
        {
        }

        public User RetrieveUser(int userId)
        {
            return Dbset.Include(c => c.UserRole).FirstOrDefault(x => x.UserId == userId);
        }

        public User GetUserByUserName(string username)
        {
            return Dbset.Include(c => c.UserRole).FirstOrDefault(c => c.UserName.Equals(username) && (!c.IsLockedOut));
        }

        public List<User> SearchUser(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage)
        {
            currentPage = (currentPage <= 0) ? 1 : currentPage;
            var query = Dbset.Include(x => x.UserRole).ThenInclude(x=>x.Role).AsQueryable().Where(x => !x.IsSupperAdmin);
            if (!string.IsNullOrEmpty(textSearch))
            {
                query = query.Where(c => c.UserName.Contains(textSearch) || c.Email.Contains(textSearch) || c.UserRole.Any(y => y.Role.Name.Contains(textSearch)));
            }

            totalPage = query.Count();
            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = query.OrderByField(sortColumn.Trim(), sortDirection);
            }
            else
                query = query.OrderByDescending(c => c.UserId);

            return query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        }

        public User GetUserByEmail(string email)
        {
            return Dbset.Include(ct => ct.UserRole).FirstOrDefault(c => c.Email.Equals(email));
        }

    }
}
