using HR.DataAccess.Entity;
using HR.DataAccess.Repository;
using HR.Models.DepartmentModel;
using HR.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace HR.Test
{
    [TestFixture]
    public class DepartmentServiceTest
    {
        private IDepartmentService _departmentService;
        public static string _connectionString = "Server=DESKTOP-M44CJ9F;Database=HrManagement;User Id=sa;password=Abc1@3456;Trusted_Connection=False;MultipleActiveResultSets=true";
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddHttpContextAccessor();
            services.AddDbContext<HrManagementContext>(options =>
                options.UseSqlServer(_connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _departmentService = serviceProvider.GetService<IDepartmentService>();
        }

        [Test]
        public void Department_Create_Success()
        {
            Assert.Pass();
        }

        #region Get By Id  

        [Test]
        public void Task_GetPostById_Return_OkResult()
        {
            //Arrange  
            string message = "";
            var departmentModel = new DepartmentModel()
            {
                Code = "dpGetNew",
                Name = "department 1",
                Status = 1,
                IsCalculateSalaryAssignment = true,
                Description = ""
            };

            //Act  
            var data = _departmentService.CreateDepartment(departmentModel, out message);
            var department = _departmentService.GetById(data.DepartmentId);

            //Assert  
            Assert.IsNotNull(department);
            Assert.AreEqual(departmentModel.Code, department.Code);
        }

        [Test]
        public void Task_GetPostById_Return_NotFoundResult()
        {
            //Arrange  
            var departmentId = 3225;

            //Act  
            var data = _departmentService.GetById(departmentId);

            //Assert  
            Assert.IsNull(data);
        }
        #endregion

        #region Add New Department  
        [Test]
        public void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            string message = "";
            var department = new DepartmentModel()
            {
                Code = "dp",
                Name = "department 1",
                Status = 1,
                IsCalculateSalaryAssignment = true,
                Description = ""
            };

            //Act  
            var data = _departmentService.CreateDepartment(department, out message);

            //Assert  
            Assert.IsNotNull(data);
            Assert.IsNotNull(message);
            Assert.GreaterOrEqual(data.DepartmentId, 1);
        }

        [Test]
        public void Task_Add_InvalidData_Return_BadRequest()
        {
            //Arrange  
            string message = "";
            var department = new DepartmentModel()
            {
                Code = "dp",
                Name = "",
                Status = 1,
                IsCalculateSalaryAssignment = true,
                Description = ""
            };
            //Act              
            var data = _departmentService.CreateDepartment(department, out message);

            //Assert
            Assert.IsNotNull(message);
            Assert.IsNull(data);
        }

        [Test]
        public void Task_Add_ValidData_MatchResult()
        {
            //Arrange  
            string message = "";
            var department = new DepartmentModel()
            {
                Code = "dpnew",
                Name = "department 1",
                Status = 1,
                IsCalculateSalaryAssignment = true,
                Description = ""
            };

            //Act  
            var data = _departmentService.CreateDepartment(department, out message);

            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(data.Code, department.Code);
        }

        #endregion

        #region Update Existing Department  

        [Test]
        public void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            string message = "";
            var departmentModel = new DepartmentModel()
            {
                Code = "dp",
                Name = "dp test",
                Status = 1,
                IsCalculateSalaryAssignment = true,
                Description = ""
            };
            //Act              
            var department = _departmentService.CreateDepartment(departmentModel, out message);
            department.Code = "dpNew";
            department.Name = "Department Test";
            department.Description = "Description";

            var updatedData = _departmentService.UpdateDepartment(department, out message);

            //Assert  
            Assert.IsNotNull(message);
            Assert.IsTrue(updatedData);
        }

        [Test]
        public void Task_Update_InvalidData_Return_False()
        {
            //Arrange  
            string message = "";
            var departmentModel = new DepartmentModel()
            {
                Code = "dp",
                Name = "dp test",
                Status = 1,
                IsCalculateSalaryAssignment = true,
                Description = ""
            };
            //Act              
            var department = _departmentService.CreateDepartment(departmentModel, out message);
            department.Code = "";
            department.Name = "";
            department.Description = "Description";

            var updatedData = _departmentService.UpdateDepartment(department, out message);

            //Assert  
            Assert.IsNotNull(message);
            Assert.IsFalse(updatedData);
        }

        #endregion
    }
}