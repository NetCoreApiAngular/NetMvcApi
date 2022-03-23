using System.Collections.Generic;
using System.Linq;
using HR.Core;
using HR.DataAccess.Entity;
using HR.DataAccess.Repository;
using HR.Models.DepartmentModel;
using HR.Services.AutoMap;
using Microsoft.AspNetCore.Http;

namespace HR.Services.Services
{
	public class DepartmentService : EntityService<Department>, IDepartmentService, IEntityService<Department>
	{
		private readonly IDepartmentRepository _departmentRepository;

		private readonly IHttpContextAccessor _httpContextAccessor;

		public DepartmentService(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, IHttpContextAccessor httpContextAccessor)
			: base(unitOfWork, (IBaseRepository<Department>)departmentRepository)
		{
			_departmentRepository = departmentRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public List<DepartmentModel> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage)
		{
			return _departmentRepository.Search(currentPage, pageSize, textSearch, sortColumn, sortDirection, out totalPage).MapToModels();
		}

		public bool UpdateDepartment(DepartmentModel departmentModel, out string message)
		{
			if (string.IsNullOrEmpty(departmentModel.Code))
			{
				message = "Mã phòng, ban không được trống";
				return false;
			}
			if (departmentModel.Code.Length > 50)
			{
				message = "Mã phòng, ban không quá 50 ký tự";
				return false;
			}
			if (string.IsNullOrEmpty(departmentModel.Name))
			{
				message = "Tên phòng, ban không được trống";
				return false;
			}
			if (departmentModel.Name.Length > 50)
			{
				message = "Tên phòng, ban không quá 250 ký tự";
				return false;
			}
			Department byId = _departmentRepository.GetById(departmentModel.DepartmentId);
			if (byId != null)
			{
				departmentModel.UpdatedBy = "";
				departmentModel.UpdatedDate = Constants.CurrentDate;
				byId = departmentModel.MapToEntity(byId);
				_departmentRepository.Update(byId);
				UnitOfWork.SaveChanges();
				message = "Cập nhật thành công.";
				return true;
			}
			message = "Cập nhật thất bại.";
			return false;
		}

		public DepartmentModel CreateDepartment(DepartmentModel model, out string message)
		{
			if (string.IsNullOrEmpty(model.Code))
			{
				message = "Mã phòng, ban không được trống";
				return null;
			}
			if (model.Code.Length > 50)
			{
				message = "Mã phòng, ban không quá 50 ký tự";
				return null;
			}
			if (string.IsNullOrEmpty(model.Name))
			{
				message = "Tên phòng, ban không được trống";
				return null;
			}
			if (model.Name.Length > 50)
			{
				message = "Tên phòng, ban không quá 250 ký tự";
				return null;
			}

			model.CreatedBy = "";
			model.CreatedDate = Constants.CurrentDate;
			Department department = _departmentRepository.Insert(model.MapToEntity());
			UnitOfWork.SaveChanges();
			_ = string.Empty;
			if (department == null)
			{
				message = "Thêm mới thất bại.";
				return null;
			}
			message = "Thêm mới thành công.";
			return department.MapToModel();
		}

		public bool Delete(int departmentId, out string message)
		{
			try
			{
				if (_departmentRepository.GetById(departmentId) != null)
				{
					_departmentRepository.Delete(departmentId);
					UnitOfWork.SaveChanges();
					message = "Xóa dữ liệu thành công.";
					return true;
				}
				message = "Xóa dữ liệu thất bại.";
				return false;
			}
			catch
			{
				message = "Dữ liệu đang được sử dụng, không được phép xóa";
				return false;
			}
		}

		public new List<DepartmentModel> GetAll()
		{
			return _departmentRepository.GetAll().ToList().MapToModels();
		}

		public List<DepartmentModel> GetDepartmentCodes(List<string> departmentCodes)
		{
			return (from c in _departmentRepository.GetAll()
				where departmentCodes.Contains(c.Code)
				select c).ToList().MapToModels();
		}

		public List<DepartmentModel> Filter(int currentPage, int pageSize, string code, string name, int? status, string sortColumn, string sortDirection, out int totalPage)
		{
			return _departmentRepository.Filter(currentPage, pageSize, code, name, status, sortColumn, sortDirection, out totalPage).MapToModels();
		}

		DepartmentModel IDepartmentService.GetById(int id)
		{
			var department =  _departmentRepository.GetById(id);
			if (department != null)
				return department.MapToModel();
			return null;
		}
	}
}
