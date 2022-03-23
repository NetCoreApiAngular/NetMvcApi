using System.Collections.Generic;
using System.Linq;
using HR.DataAccess.Entity;
using HR.Models.DepartmentModel;

namespace HR.Services.AutoMap
{
	public static class DepartmentMapper
	{
		public static DepartmentModel MapToModel(this Department entity)
		{
			return new DepartmentModel
			{
				DepartmentId = entity.DepartmentId,
				Code = entity.Code,
				Name = entity.Name,
				Description = entity.Description,
				Status = entity.Status,
				IsCalculateSalaryAssignment = entity.IsCalculateSalaryAssignment,
				CreatedDate = entity.CreatedDate,
				CreatedBy = entity.CreatedBy,
				UpdatedDate = entity.UpdatedDate,
				UpdatedBy = entity.UpdatedBy
			};
		}

		public static DepartmentModel MapToModel(this Department entity, DepartmentModel model)
		{
			model.DepartmentId = entity.DepartmentId;
			model.Code = entity.Code;
			model.Name = entity.Name;
			model.Description = entity.Description;
			model.Status = entity.Status;
			model.IsCalculateSalaryAssignment = entity.IsCalculateSalaryAssignment;
			model.CreatedDate = entity.CreatedDate;
			model.CreatedBy = entity.CreatedBy;
			model.UpdatedDate = entity.UpdatedDate;
			model.UpdatedBy = entity.UpdatedBy;
			return model;
		}

		public static Department MapToEntity(this DepartmentModel model)
		{
			return new Department
			{
				DepartmentId = model.DepartmentId,
				Code = model.Code,
				Name = model.Name,
				Description = model.Description,
				Status = model.Status,
				IsCalculateSalaryAssignment = model.IsCalculateSalaryAssignment,
				CreatedDate = model.CreatedDate,
				CreatedBy = model.CreatedBy,
				UpdatedDate = model.UpdatedDate,
				UpdatedBy = model.UpdatedBy
			};
		}

		public static Department MapToEntity(this DepartmentModel model, Department entity)
		{
			entity.DepartmentId = model.DepartmentId;
			entity.Code = model.Code;
			entity.Name = model.Name;
			entity.Description = model.Description;
			entity.Status = model.Status;
			entity.IsCalculateSalaryAssignment = model.IsCalculateSalaryAssignment;
			entity.UpdatedDate = model.UpdatedDate;
			entity.UpdatedBy = model.UpdatedBy;
			return entity;
		}

		public static List<Department> MapToEntities(this List<DepartmentModel> models)
		{
			return models.Select((DepartmentModel x) => x.MapToEntity()).ToList();
		}

		public static List<DepartmentModel> MapToModels(this List<Department> entities)
		{
			return entities.Select((Department x) => x.MapToModel()).ToList();
		}
	}
}
