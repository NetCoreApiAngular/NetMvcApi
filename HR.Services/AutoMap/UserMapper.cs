using HR.DataAccess.Entity;
using HR.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Services.AutoMap
{
    public static class UserMapper
    {
        #region Mapping User
        public static User MapToEntity(this UserModel model)
        {
            var user = new User();
            user.Email = model.Email;
            user.Password = model.Password;
            user.PasswordSalt = model.PasswordSalt;
            user.UserName = model.UserName;
            user.CreatedDate = model.CreatedDate;
            user.IsLockedOut = model.IsLockedOut;
            user.IsSupperAdmin = model.IsSupperAdmin;
            user.Status = model.Status;
            user.Avatar = model.Avatar;
            user.PasswordSalt = model.PasswordSalt;
            user.Tel = model.Tel;
            user.FullName = model.FullName;
            user.Description = model.Description;
            return user;
        }
        public static User MapToEntity(this UserModel model, User entity)
        {
            entity.Email = model.Email;
            entity.Password = model.Password;
            return entity;
        }
        public static UserModel MapToModel(this User entity)
        {
            var user = new UserModel();
            user.UserId = entity.UserId;
            user.UserName = entity.UserName;
            user.Email = entity.Email;
            user.CreatedDate = entity.CreatedDate;
            user.Status = entity.Status;
            user.Password = entity.Password;
            user.Avatar = entity.Avatar;
            user.Tel = entity.Tel;
            user.IsLockedOut = entity.IsLockedOut;
            user.FullName = entity.FullName;
            return user;
        }
        public static List<User> MapToEntities(this List<UserModel> models)
        {
            return models.Select(x => x.MapToEntity()).ToList();
        }

        public static List<UserModel> MapToModels(this List<User> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }
        #endregion
        
    }
}
