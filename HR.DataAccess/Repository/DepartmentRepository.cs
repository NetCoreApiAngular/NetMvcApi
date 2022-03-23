using System;
using System.Collections.Generic;
using System.Linq;
using HR.DataAccess.Entity;
using HR.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HR.DataAccess.Repository
{
	public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository, IBaseRepository<Department>
	{
		public DepartmentRepository(HrManagementContext context)
			: base(context)
		{
		}

		public List<Department> Filter(int currentPage, int pageSize, string code, string name, int? status, string sortColumn, string sortDirection, out int totalPage)
		{
			currentPage = ((currentPage <= 0) ? 1 : currentPage);
			pageSize = ((pageSize <= 0) ? 20 : pageSize);
			IQueryable<Department> queryable = Dbset.AsQueryable();
			if (!string.IsNullOrEmpty(code))
			{
				queryable = queryable.Where((Department c) => c.Code.Contains(code));
			}
			if (!string.IsNullOrEmpty(name))
			{
				queryable = queryable.Where((Department c) => c.Name.Contains(name));
			}
			if (status.HasValue)
			{
				queryable = queryable.Where((Department x) => (int?)x.Status == status);
			}
			totalPage = queryable.Count();
			queryable = (string.IsNullOrEmpty(sortColumn) ? queryable.OrderByDescending((Department c) => c.DepartmentId) : queryable.OrderByField(sortColumn.Trim(), sortDirection));
			return queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
		}

		public Department GetByDepartmentCode(string code, int? departmentId = null)
		{
			IQueryable<Department> source = ((IQueryable<Department>)Dbset).Where((Department x) => x.Code == code);
			if (departmentId.HasValue)
			{
				source = source.Where((Department c) => (int?)c.DepartmentId != departmentId);
			}
			return source.FirstOrDefault();
		}

		public new Department GetById(int departmentId)
		{
			return ((IQueryable<Department>)Dbset).Where((Department x) => x.DepartmentId == departmentId).FirstOrDefault();
		}

		public List<Department> GetDepartmentForToxic()
		{
			string text = "SELECT * FROM Department d INNER JOIN [Group] g ON g.DepartmentId = d.DepartmentId AND g.GroupId IN (SELECT GroupId FROM TimeLeaveToxic)";
			return RelationalQueryableExtensions.FromSqlRaw<Department>(Dbset, text, Array.Empty<object>()).ToList();
		}

		public List<Department> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage)
		{
			currentPage = ((currentPage <= 0) ? 1 : currentPage);
			pageSize = ((pageSize <= 0) ? 20 : pageSize);
			IQueryable<Department> queryable = Dbset.AsQueryable();
			if (!string.IsNullOrEmpty(textSearch))
			{
				queryable = queryable.Where((Department c) => c.Name.Contains(textSearch));
			}
			totalPage = queryable.Count();
			queryable = (string.IsNullOrEmpty(sortColumn) ? queryable.OrderByDescending((Department c) => c.DepartmentId) : queryable.OrderByField(sortColumn.Trim(), sortDirection));
			return queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
		}

		List<Department> IDepartmentRepository.GetAll()
		{
			return Dbset.AsQueryable().ToList();
		}
	}
}
