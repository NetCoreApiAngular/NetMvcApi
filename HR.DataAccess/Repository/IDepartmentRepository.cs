using System.Collections.Generic;
using HR.DataAccess.Entity;

namespace HR.DataAccess.Repository
{
	public interface IDepartmentRepository : IBaseRepository<Department>
	{
		List<Department> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage);

		List<Department> Filter(int currentPage, int pageSize, string code, string name, int? status, string sortColumn, string sortDirection, out int totalPage);

		new Department GetById(int departmentId);

		new List<Department> GetAll();

		Department GetByDepartmentCode(string code, int? departmentId = null);

		List<Department> GetDepartmentForToxic();
	}
}
