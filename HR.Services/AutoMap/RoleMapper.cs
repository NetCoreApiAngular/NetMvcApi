using HR.DataAccess.Entity;
using HR.Models.Role;
using System.Collections.Generic;
using System.Linq;

namespace HR.Services.AutoMap
{
    public static class RoleMapper
    {
        #region Mapping Role
        public static RoleModel MapToModel(this Roles entity)
        {
            return new RoleModel
            {
                RoleId = entity.RoleId,
                Code = entity.Code,
                Name = entity.Name,
                Note = entity.Note,
                Status = entity.Status,
                CreatedDate = entity.CreatedDate
            };
        }
        public static RoleModel MapToModel(this Roles entity, RoleModel model)
        {
            model.RoleId = entity.RoleId;
            model.Code = entity.Code;
            model.Name = entity.Name;
            model.Note = entity.Note;
            model.Status = entity.Status;
            return model;
        }
        public static Roles MapToEntity(this RoleModel model)
        {
            return new Roles
            {
                RoleId = model.RoleId,
                Code = model.Code,
                Name = model.Name,
                Note = model.Note,
                Status = model.Status,
                CreatedDate = model.CreatedDate,
                CreatedByUserId = model.CreatedByUserId
            };
        }
        public static Roles MapToEntity(this RoleModel model, Roles entity)
        {
            entity.RoleId = model.RoleId;
            entity.Code = model.Code;
            entity.Name = model.Name;
            entity.Note = model.Note;
            entity.Status = model.Status;
            entity.CreatedDate = model.CreatedDate;
            entity.CreatedByUserId = model.CreatedByUserId;
            return entity;
        }
        public static List<Roles> MapToEntities(this List<RoleModel> models)
        {
            return models.Select(x => x.MapToEntity()).ToList();
        }

        public static List<RoleModel> MapToModels(this List<Roles> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }
        #endregion
    }
}
