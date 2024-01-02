using Microsoft.EntityFrameworkCore;
using ReadAndAnalysis.App.DTOs.Accounting;
using ReadAndAnalysis.App.Generators;
using ReadAndAnalysis.App.Securities;
using ReadAndAnalysis.App.Services.Interfaces;
using ReadAndAnalysis.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private TxtPrcContext _context;
        public AccountService(TxtPrcContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangePassword(long userId, string password, long userLoginId)
        {
            var user = await _context.TbUsers.SingleAsync(u => u.Id == userId);
            var pass = HashEncode.GetHashCode(HashEncode.GetHashCode(password));
            user.Password = pass;
            user.ModifiedDate = DateTime.Now;
            user.ModifiedBy = userLoginId;
            _context.TbUsers.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateUser(CreateUserDto create)
        {
            var pass = HashEncode.GetHashCode(HashEncode.GetHashCode(create.Password));
            try
            {
                var user = new TbUser()
                {
                    Password = pass,
                    CreateBy = create.LoginUserId,
                    FullName = create.FullName,
                    Username = create.UserName.ToLower(),
                    Mobile = create.Mobile,
                    CreateDate = DateTime.Now,
                    CreateIp = GetIpAddress.GetIp(),
                    Gid = Guid.NewGuid()
                };
                await _context.TbUsers.AddAsync(user);
                await _context.SaveChangesAsync();
                foreach (var id in create.RoleIds)
                {
                    TbUserRole ur = new TbUserRole()
                    {
                        UserId = user.Id,
                        RoleId = id,
                        Gid = Guid.NewGuid(),
                        CreateIp = GetIpAddress.GetIp(),
                        CreateDate = DateTime.Now,
                        CreateBy = create.LoginUserId
                    };
                    await _context.TbUserRoles.AddAsync(ur);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<RolesDto>> GetAllRoles()
        {
            List<RolesDto> list = new List<RolesDto>();
            var roles = await _context.TbRoles.ToListAsync();
            foreach (var role in roles)
            {
                RolesDto dto = new RolesDto()
                {
                    Id = role.Id,
                    Title = role.Title
                };
                list.Add(dto);
            }
            return list;
        }

        public async Task<List<UsersDto>> GetAllUsers()
        {
            List<UsersDto> list = new List<UsersDto>();
            var users = await _context.TbUsers.Where(u=>!u.IsPrivateInList).ToListAsync();
            foreach (var user in users)
            {
                UsersDto dto = new UsersDto()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Mobile = user.Mobile,
                    Username = user.Username
                };
                list.Add(dto);
            }
            return list;
        }

        public async Task<UsersDto> GetUserById(long id)
        {
            var user = await _context.TbUsers.SingleAsync(u => u.Id == id);
            UsersDto dto = new UsersDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                Mobile = user.Mobile,
                Username = user.Username
            };
            return dto;
        }

        public async Task<UserLoginedDto> GetUserForLogin(string username, string password)
        {
            var pass = HashEncode.GetHashCode(HashEncode.GetHashCode(password));
            var user = await _context.TbUsers.SingleOrDefaultAsync(u => ((u.Username.ToLower() == username) ||
            u.Mobile == username) && u.Password == pass);
            UserLoginedDto dto = new UserLoginedDto();

            if (user != null)
            {
                dto.Id = user.Id;
                dto.Mobile = user.Mobile;
                dto.Fullname = user.FullName;
            }
            return dto;
        }

        public async Task<bool> HasAdminAccess(long userId)
        {
          var userRoles = await _context.TbUserRoles.Where(u=>u.UserId == userId).ToListAsync();
            var roleIds = userRoles.Select(u => u.RoleId).ToList();
            foreach (var id in roleIds)
            {
                var role = await _context.TbRoles.SingleAsync(r => r.Id == id);
                if (role.HasAdminAccess)
                    return true;
            }
            return false;
        }
        public async Task<bool> IsSecretary(long userId)
        {
            var userRoles = await _context.TbUserRoles.Where(u => u.UserId == userId).Select(u => u.RoleId).ToListAsync();
            foreach (var item in userRoles)
            {
                var role = await _context.TbRoles.SingleAsync(r => r.Id == item);
                if (role.IsSecretary)
                    return true;
            }
            return false;

        }
    }
}
