using ReadAndAnalysis.App.DTOs.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserLoginedDto> GetUserForLogin(string username,string password);
        Task<bool> CreateUser(CreateUserDto create);
        Task<List<RolesDto>> GetAllRoles();
        Task<List<UsersDto>> GetAllUsers();
        Task<UsersDto> GetUserById(long id);
        Task<bool> ChangePassword(long userId, string password, long userLoginId);
        Task<bool> HasAdminAccess(long userId);
        Task<bool> IsSecretary(long userId);
    }
}
