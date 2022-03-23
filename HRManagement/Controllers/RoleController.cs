using HR.Models;
using HR.Models.Role;
using HR.Services.AutoMap;
using HR.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HRManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("role")]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getById")]
        public ApiResponse<RoleModel> GetById(int roleId)
        {
            var model = _roleService.GetById(roleId).MapToModel();
            return new ApiResponse<RoleModel>(model);
        }

        /// <summary>
        /// BetByCode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getByCode")]
        public ApiResponse<RoleModel> getByCode(string code)
        {
            var model = _roleService.GetByCode(code);
            return new ApiResponse<RoleModel>(model);
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="textSearch"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public ApiResponse<RoleSearchModel> Search(string textSearch, int currentPage, int pageSize, string sortColumn, string sortDirection)
        {
            int totalRecords;
            var model = new RoleSearchModel
            {
                Roles = _roleService.Search(currentPage, pageSize, textSearch, sortColumn, sortDirection, out totalRecords),
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                PageIndex = currentPage,
                PageSize = pageSize,
                TotalRecords = totalRecords,
            };
            return new ApiResponse<RoleSearchModel>(model);
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll")]
        public ApiResponse<List<RoleModel>> GetAll()
        {
            var roles = _roleService.GetRoles();
            return new ApiResponse<List<RoleModel>>(roles);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public ApiResponse<RoleModel> Create([FromBody]RoleModel model)
        {
            string msgError;
            var role = _roleService.CreateRole(model, out msgError);
            return new ApiResponse<RoleModel>(role, msgError);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public ApiResponse<bool> Update([FromBody]RoleModel model)
        {
            string msgError;
            var result = _roleService.UpdateRole(model, out msgError);
            return new ApiResponse<bool>(result, msgError);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int roleId)
        {
            try
            {
                string msgError;
                var result = _roleService.Delete(roleId, out msgError);
                if (!result)
                {
                    return BadRequest(new ApiResponse<bool>(false, msgError));
                }
                var response = new ApiResponse<bool>(true, msgError);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message, roleId);
                return BadRequest(new ApiResponse<bool>(false, HR.Core.Constants.ErrorOnProcess));
            }
        }

        /// <summary>
        /// Invisibe
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("invisibe")]
        public ApiResponse<RoleModel> Invisibe(int roleId)
        {
            string message;
            var result = _roleService.ChangeStatus(roleId, out message);
            return new ApiResponse<RoleModel>(result, message);
        }

    }
}