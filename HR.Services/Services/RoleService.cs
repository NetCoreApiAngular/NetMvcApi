using HR.DataAccess.Entity;
using HR.DataAccess.Repository;
using HR.Models;
using HR.Models.Role;
using HR.Services.AutoMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.Services.Services
{
    public interface IRoleService : IEntityService<Roles>
    {
        RoleModel GetByCode(string code);
        List<RoleModel> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage);
        bool UpdateRole(RoleModel roleModel, out string message);
        bool Delete(int roleId, out string message);
        List<RoleModel> GetRoles();
        RoleModel ChangeStatus(int roleId, out string message);
        RoleModel CreateRole(RoleModel model, out string message);
        IEnumerable<Roles> GetListRoleAsEnumerable();

    }
    public class RoleService : EntityService<Roles>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleModuleActionRepository _roleModuleActionRepository;
        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IRoleModuleActionRepository roleModuleActionRepository)
            : base(unitOfWork, roleRepository)
        {
            _roleRepository = roleRepository;
            _roleModuleActionRepository = roleModuleActionRepository;
        }

        public List<RoleModel> Search(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage)
        {
            var roleEntities = _roleRepository.Search(currentPage, pageSize, textSearch, sortColumn, sortDirection, out totalPage);
            if (roleEntities != null)
            {
                return roleEntities.MapToModels();
            }
            return null;
        }

        public RoleModel GetByCode(string code)
        {
            var roleEntitie = _roleRepository.Query(c=>c.Code == code).FirstOrDefault();
            if (roleEntitie != null)
            {
                return roleEntitie.MapToModel();
            }
            return null;
        }

        public bool UpdateRole(RoleModel roleModel, out string message)
        {
            var roleEntity = _roleRepository.GetById(roleModel.RoleId);
            if (roleEntity != null)
            {
                if(roleModel.Code != roleEntity.Code)
                {
                    var existMaQuyen = _roleRepository.GetAll().FirstOrDefault(c => c.Code == roleModel.Code);
                    if (existMaQuyen != null)
                    {
                        message = "Mã quyền đã tồn tại";
                        return false;
                    }
                }
                if (roleModel.Name.ToLower().Trim() != roleEntity.Name.ToLower().Trim())
                {
                    var existTenQuyen = _roleRepository.GetAll().FirstOrDefault(c => c.Name.ToLower().Trim() == roleModel.Name.ToLower().Trim());
                    if (existTenQuyen != null)
                    {
                        message = "Tên quyền đã tồn tại";
                        return false;
                    }
                }
                roleEntity.UpdatedDate = DateTime.Now;
                roleEntity.UpdatedUserId = roleModel.UpdatedUserId;
                roleEntity.Code = roleModel.Code;
                roleEntity.Name = roleModel.Name;
                roleEntity.Note = roleModel.Note;
                roleEntity.Status = roleModel.Status;

                _roleRepository.Update(roleEntity);
                UnitOfWork.SaveChanges();

                if (!AssignRoleModuleAction(roleEntity.RoleId, roleModel.ModuleActions, out message))
                {
                    message = "Cập nhật quyền thất bại.";
                    return false;
                }

                message = "Cập nhật thành công";
                return true;
            }

            message = "Cập nhật quyền thất bại.";
            return false;
        }

        public RoleModel CreateRole(RoleModel model, out string message)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    message = "Tên quyền không được bỏ trống.";
                    return null;
                }

                var existMaQuyen = _roleRepository.GetAll().FirstOrDefault(c=>c.Code == model.Code);
                var existTenQuyen = _roleRepository.GetAll().FirstOrDefault(c => c.Name.ToLower().Trim() == model.Name.ToLower().Trim());
                if (existMaQuyen != null)
                {
                    message = "Mã quyền đã tồn tại";
                    return null;
                }
                if (existTenQuyen != null)
                {
                    message = "Tên quyền đã tồn tại";
                    return null;
                }
                var createdRole = _roleRepository.Insert(model.MapToEntity());
                UnitOfWork.SaveChanges();
                var errorMsg = string.Empty;
                if (createdRole == null)
                {
                    message = "Tạo mới thất bại";
                    return null;
                }
                AssignRoleModuleAction(createdRole.RoleId, model.ModuleActions, out errorMsg);
                message = "Tạo mới thành công";
                return createdRole.MapToModel();
            }
            catch (Exception ex)
            {
                //Log.Error("Create role error", ex);
                message = "Tạo mới thất bại: " + ex.Message;
                return null;
            }

        }

        public bool Delete(int roleId, out string message)
        {
            var entity = _roleRepository.GetById(roleId);
            if (entity != null)
            {
                var isExists = _roleRepository.GetAll().Any(c => c.RoleId == roleId
                        && c.UserRole.Any(ce => ce.RoleId == roleId));
                if (isExists)
                {
                    message = "Bạn không thể thực hiện xóa quyền này.";
                    return false;
                }

                var roleModules = _roleModuleActionRepository.FindAll(x => x.RoleId == roleId);
                if (roleModules != null)
                {
                    _roleModuleActionRepository.DeleteMulti(roleModules);
                }
                _roleRepository.Delete(roleId);
                UnitOfWork.SaveChanges();

                message = "Xóa quyền thành công";
                return true;
            }

            message = "Xóa quyền thất bại";
            return false;
        }

        public RoleModel ChangeStatus(int roleId, out string message)
        {
            var position = _roleRepository.Query(c => c.RoleId == roleId).FirstOrDefault();
            if (position != null)
            {
                if (position.Status)
                {
                    position.Status = false;
                }
                else
                {
                    position.Status = true;
                }

                _roleRepository.Update(position);
                UnitOfWork.SaveChanges();

                message = "Cập nhật trạng thái quyền thành công.";
                return position.MapToModel();
            }

            message = "Cập nhật trạng thái quyền thất bại.";
            return null;
        }

        public List<RoleModel> GetRoles()
        {
            //Igrone role system
            return _roleRepository.GetAll().ToList().MapToModels();
        }

        public bool AssignRoleModuleAction(int roleId, List<ModuleActionModel> roleModuleActions, out string message)
        {
            try
            {
                message = "";
                if (roleModuleActions != null && roleModuleActions.Any())
                {
                    var listRoleModule = roleModuleActions.Select(item => new RoleModuleAction
                    {
                        RoleId = roleId,
                        ModuleActionId = item.ModuleActionId
                    }).ToList();

                    //check exists role
                    var roleModuleAction = _roleModuleActionRepository.Query(c => c.RoleId == roleId).ToList();
                    if (roleModuleAction.Any())
                    {
                        _roleModuleActionRepository.DeleteMulti(roleModuleAction);
                    }

                    _roleModuleActionRepository.InsertMulti(listRoleModule);
                    UnitOfWork.SaveChanges();
                    message = "Phân Quyền thành công.";
                }
                return true;
                //else
                //{
                //    var roleModuleAction = _roleModuleActionRepository.Query(c => c.RoleId == roleId).ToList();
                //    if (roleModuleAction.Any())
                //    {
                //        _roleModuleActionRepository.DeleteMulti(roleModuleAction);
                //    }
                //    UnitOfWork.SaveChanges();
                //    message = "Phân Quyền thành công.";
                //    return true;
                //}
            }
            catch (Exception)
            {
                message = "Phân Quyền thất bại.";
                return false;
            }
        }


        public IEnumerable<Roles> GetListRoleAsEnumerable()
        {
            IEnumerable<Roles> roles = _roleRepository.GetAll().Where(x => x.RoleId > 2).ToList();
            return roles;
        }
    }
}
