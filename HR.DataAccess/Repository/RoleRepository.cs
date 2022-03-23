using System.Collections.Generic;
using System.Linq;
using HR.DataAccess.Entity;
using HR.DataAccess.Extensions;

namespace HR.DataAccess.Repository
{
    public interface IRoleRepository: IBaseRepository<Roles>
    {
        List<Roles> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage);
    }
    public class RoleRepository : BaseRepository<Roles>, IRoleRepository
    {
        public RoleRepository(HrManagementContext context) : base(context)
        {
        }

        public List<Roles> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection,
          out int totalPage)
        {
            currentPage = (currentPage <= 0) ? 1 : currentPage;
            pageSize = (pageSize <= 0) ? 20 : pageSize;

            var query = Dbset.AsQueryable();
            if (!string.IsNullOrEmpty(textSearch))
            {
                query = query.Where(c => c.Name.Contains(textSearch) || c.Code.Equals(textSearch));
            }

            totalPage = query.Count();

            if(!string.IsNullOrEmpty(sortColumn))
            {
                query = query.OrderByField(sortColumn.Trim(), sortDirection);
            }
            else
                query = query.OrderByDescending(c => c.RoleId);

            return query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
