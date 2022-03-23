using System;

namespace HR.Models.DepartmentModel
{
	public class DepartmentModel
	{
		public int DepartmentId { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int Status { get; set; }

		public bool IsCalculateSalaryAssignment { get; set; }

		public DateTime CreatedDate { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? UpdatedDate { get; set; }

		public string UpdatedBy { get; set; }
	}
}
