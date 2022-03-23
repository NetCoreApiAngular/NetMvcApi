using System.Collections.Generic;
using HR.DataAccess.Entity;
using HR.Models.DepartmentModel;

namespace HR.Services.Services
{
	public interface IDepartmentService : IEntityService<Department>
	{
		List<DepartmentModel> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage);

		List<DepartmentModel> Filter(int currentPage, int pageSize, string code, string name, int? status, string sortColumn, string sortDirection, out int totalPage);

		bool UpdateDepartment(DepartmentModel departmentModel, out string message);

		new DepartmentModel GetById(int id);

		bool Delete(int departmentId, out string message);

		DepartmentModel CreateDepartment(DepartmentModel model, out string message);

		new List<DepartmentModel> GetAll();

		List<DepartmentModel> GetDepartmentCodes(List<string> departmentCodes);
	}
}
