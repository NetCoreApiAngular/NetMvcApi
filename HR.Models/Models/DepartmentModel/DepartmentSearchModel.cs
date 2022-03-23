using System.Collections.Generic;
using HR.Models.Common;

namespace HR.Models.DepartmentModel
{
	public class DepartmentSearchModel : Paging
	{
		public string TextSearch { get; set; }

		public string SortColumn { get; set; }

		public string SortDirection { get; set; }

		public List<DepartmentModel> Departments { get; set; }
	}
}
