using System;
using System.Collections.Generic;
using HR.Models;
using HR.Models.DepartmentModel;
using HR.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRManagement.Controllers
{
	[Authorize]
	[ApiController]
	[Route("department")]
	public class DepartmentController : ControllerBase
	{
		private IDepartmentService _departmentService;

		private readonly ILogger<DepartmentController> _logger;

		public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService)
		{
			_departmentService = departmentService;
			_logger = logger;
		}

		[HttpGet]
		[Route("getById")]
		public ApiResponse<DepartmentModel> GetById(int departmentId)
		{
			DepartmentModel byId = _departmentService.GetById(departmentId);
			return new ApiResponse<DepartmentModel>(byId);
		}

		[HttpGet]
		[Route("search")]
		public ApiResponse<DepartmentSearchModel> Search(string textSearch, int currentPage, int pageSize, string sortColumn, string sortDirection)
		{
			int totalPage;
			DepartmentSearchModel content = new DepartmentSearchModel
			{
				Departments = _departmentService.Search(currentPage, pageSize, textSearch, sortColumn, sortDirection, out totalPage),
				SortColumn = sortColumn,
				SortDirection = sortDirection,
				PageIndex = currentPage,
				PageSize = pageSize,
				TotalRecords = totalPage
			};
			return new ApiResponse<DepartmentSearchModel>(content);
		}

		[HttpGet]
		[Route("filter")]
		public ApiResponse<DepartmentSearchModel> Filter(string code, string name, int? status, int currentPage, int pageSize, string sortColumn, string sortDirection)
		{
			int totalPage;
			DepartmentSearchModel content = new DepartmentSearchModel
			{
				Departments = _departmentService.Filter(currentPage, pageSize, code, name, status, sortColumn, sortDirection, out totalPage),
				SortColumn = sortColumn,
				SortDirection = sortDirection,
				PageIndex = currentPage,
				PageSize = pageSize,
				TotalRecords = totalPage
			};
			return new ApiResponse<DepartmentSearchModel>(content);
		}

		[HttpGet]
		[Route("getAll")]
		public ApiResponse<List<DepartmentModel>> GetAll()
		{
			List<DepartmentModel> all = _departmentService.GetAll();
			return new ApiResponse<List<DepartmentModel>>(all);
		}

		[HttpPost]
		[Route("create")]
		public IActionResult Create([FromBody] DepartmentModel model)
		{
			var message = "";
			DepartmentModel departmentModel = _departmentService.CreateDepartment(model, out message);
			if (departmentModel == null)
			{
				return BadRequest(new ApiResponse<DepartmentModel>(model, message));
			}
			ApiResponse<DepartmentModel> value = new ApiResponse<DepartmentModel>(departmentModel, message);
			return Ok(value);
		}

		[HttpPost]
		[Route("update")]
		public IActionResult Update([FromBody] DepartmentModel model)
		{
			var message = "";
			if (!_departmentService.UpdateDepartment(model, out message))
			{
				return BadRequest(new ApiResponse<bool>(content: false, message));
			}
			ApiResponse<DepartmentModel> value = new ApiResponse<DepartmentModel>(model, message);
			return Ok(value);
		}

		[HttpPost]
		[Route("delete")]
		public IActionResult Delete(int departmentId)
		{
			try
			{
				if (!_departmentService.Delete(departmentId, out var message))
				{
					return BadRequest(new ApiResponse<bool>(content: false, message));
				}
				ApiResponse<bool> value = new ApiResponse<bool>(content: true, message);
				return Ok(value);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message, departmentId);
				return BadRequest(new ApiResponse<bool>(content: false, "Có lỗi xảy ra trong quá trình xử lý, liên hệ Admin để nhận trợ giúp."));
			}
		}
	}
}
