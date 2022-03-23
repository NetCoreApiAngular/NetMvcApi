using HR.Core;
using HR.DataAccess.Entity;
using HR.DataAccess.Repository;
using HR.Models;
using HR.Models.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HR.Services.AutoMap;
using System.Collections.Generic;
using HR.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HR.Services.Services
{
    public interface IUserService : IEntityService<User>
    {
        UserCommon Authenticate(string username, string password, string secret, out string msgError);
        UserCommon CreateUser(UserModel userModel, string secret, out string message);
        List<UserModel> SearchUser(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage);
        bool ForgotPassword(string fromemail, string toEmail, string smtpMailServer, int smtpPort, bool useSsl, string smtpUserName, string smtpPassword, string emailServer, out string message);
        bool ValidationEmail(string email, out string message);
        UserModel GetByUserName(string userName);
    }
    public class UserService : EntityService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private IMemoryCache _cache;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMemoryCache memoryCache)
            : base(unitOfWork, userRepository)
        {
            _userRepository = userRepository;
            _cache = memoryCache;
        }

        public UserCommon Authenticate(string username, string password, string secret, out string msgError)
        {
            msgError = string.Empty;
            var result = new UserCommon();
            var user = _userRepository.GetAll().SingleOrDefault(x => x.UserName == username || x.Email == username);

            // return null if user not found
            if (user == null)
            {
                msgError = "Thông tin đăng nhập không hợp lệ.";
                result.Status = LoginResult.InvalidEmail;
                return null;
            }

            var passwordHasher = new CustomPasswordHasher<User>();
            var newPass = string.Concat(user.PasswordSalt, password);

            var verify = passwordHasher.VerifyHashedPassword(user, user.Password, newPass);
            if (verify == PasswordVerificationResult.Success)
            {
                if (user.IsLockedOut)
                {
                    msgError = "Tài khoản của bạn đã bị khóa, xin vui lòng liên hệ với người quản trị.";
                    result.Status = LoginResult.IsLockedOut;
                    return result;
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);

                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
                // Adding roles code
                claimsIdentity.AddClaims(user.UserRole.Select(role => new Claim(ClaimTypes.Role, role.Role.Name)));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Token = tokenHandler.WriteToken(token);

                result.Status = LoginResult.Success;
                result.Email = user.Email;
                result.UserId = user.UserId;
                result.UserName = user.UserName;
                result.Avatar = user.Avatar;
                result.IsAdmin = user.IsSupperAdmin;
                result.FullName = user.FullName;
                return result;
            }

            msgError = "Mật khẩu không hợp lệ.";
            result.Status = LoginResult.InvalidPassword;
            return result;


        }

        public UserModel GetByUserName(string userName)
        {
            var cacheKey = string.Format(Constants.CacheUserInfoKey, userName);
            var cacheEntry = _cache.Get<UserModel>(cacheKey);
            if (cacheEntry != null)
            {
                return cacheEntry;
            }
            var userEntitie = _userRepository.Query(c => c.UserName == userName).FirstOrDefault();
            if (userEntitie != null)
            {
                var useModel = userEntitie.MapToModel();
                _cache.Set(cacheKey, useModel, DateTime.UtcNow.AddHours(Constants.CacheInThreeHour));
                return userEntitie.MapToModel();
            }
            return null;
        }

        public UserCommon CreateUser(UserModel userModel, string secret, out string message)
        {

            if (string.IsNullOrEmpty(userModel.Password))
            {
                message = "Password cannot empty.";
                return null;
            }
            if (string.IsNullOrEmpty(userModel.UserName))
            {
                message = "UserName cannot empty.";
                return null;
            }

            var user = _userRepository.Query(x => x.Email == userModel.UserName).Any();
            if (!user && !string.IsNullOrEmpty(userModel.Email))
            {
                user = _userRepository.Query(x => x.Email == userModel.Email).Any();
            }

            if (!user)
            {
                var result = new UserCommon();
                // Add table User
                userModel.PasswordSalt = (new CustomPasswordHasher<UserModel>()).HashPassword(userModel, userModel.Password);
                var password = string.Concat(userModel.PasswordSalt, userModel.Password);
                userModel.Password = CryptographyHelper.HashPassword(userModel.Password);

                userModel.CreatedDate = DateTime.Now;
                userModel.Status = true;
                var userEntity = _userRepository.Insert(userModel.MapToEntity());
                UnitOfWork.SaveChanges();

                // Add table UserRole
                //var userRoleEntity = new UserRole();
                //userRoleEntity.UserId = userEntity.UserId;
                //userRoleEntity.RoleId = userModel.RoleId;
                //userRoleEntity.CreatedDate = DateTime.Now;
                //_userRoleRepository.Insert(userRoleEntity);
                //UnitOfWork.SaveChanges();

                // Return
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);

                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.Name, userEntity.UserId.ToString()),
                    new Claim(ClaimTypes.Email, userEntity.Email)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
                // Adding roles code
                claimsIdentity.AddClaims(userEntity.UserRole.Select(role => new Claim(ClaimTypes.Role, role.Role.Name)));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Token = tokenHandler.WriteToken(token);

                result.Status = LoginResult.Success;
                result.Email = userEntity.Email;
                result.UserId = userEntity.UserId;
                result.UserName = userEntity.UserName;
                result.Avatar = userEntity.Avatar;
                result.IsAdmin = userEntity.IsSupperAdmin;
                result.FullName = userEntity.FullName;
                message = "";
                return result;
            }
            message = "Email đã tồn tại";
            return null;
        }

        public List<UserModel> SearchUser(int currentPage, int pageSize, string textSearch, string sortColumn, string sortDirection, out int totalPage)
        {
            var userEntities = _userRepository.SearchUser(currentPage, pageSize, textSearch, sortColumn, sortDirection, out totalPage);
            if (userEntities != null)
            {
                return userEntities.MapToModels();
            }
            return null;
        }
        public bool ValidationEmail(string email, out string message)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                message = Constants.EmailIsNotExist;
                return false;
            }
            message = "";
            return true;
        }
        public bool ForgotPassword(string fromemail, string toEmail, string smtpMailServer, int smtpPort, bool useSsl, string smtpUserName, string smtpPassword, string emailServer, out string message)
        {
            try
            {
                //Tạo Pass mới
                string newPassword = CryptographyHelper.CreateRandomPassword(7);
                //Mã hóa pass mới
                var passwordHasher = new CustomPasswordHasher<User>();

                //Lưu lại pass vào hệ thống
                var currentUser = _userRepository.GetUserByEmail(toEmail);
                if (currentUser == null)
                {
                    message = Constants.EmailIsNotExist;
                    return false;
                }

                var hashPass = passwordHasher.HashPassword(currentUser, newPassword);
                currentUser.Password = hashPass;
                _userRepository.Update(currentUser);
                UnitOfWork.SaveChanges();

                string body = "";
                using (SmtpClient client = new SmtpClient(smtpMailServer, smtpPort))
                {
                    // Configure the client
                    client.EnableSsl = useSsl;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    // client.UseDefaultCredentials = true;
                    body = string.Format(Constants.ResetPasswordResult, toEmail, newPassword);
                    // A client has been created, now you need to create a MailMessage object
                    MailMessage messages = new MailMessage(
                                             fromemail, // From field
                                             toEmail, // Recipient field
                                             "Đặt lại mật khẩu", // Subject of the email message
                                             body // Email message body
                                          );
                    // Send the message
                    client.Send(messages);
                }
                message = body;
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
