using System;
using System.Collections.Generic;

namespace HR.DataAccess.Entity
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public bool IsCalculateSalaryAssignment { get; set; }
        public int? NumericalOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
