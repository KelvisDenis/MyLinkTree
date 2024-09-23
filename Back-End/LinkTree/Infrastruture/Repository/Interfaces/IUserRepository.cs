using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Domain.Models;

namespace LinkTree.Infrastruture.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserModel? user);
        Task<UserModel> GetUserByIdAsync(int? id);
        Task<UserModel> GetUserByUsernameAsync(string? username);
        Task<UserModel> GetUserByEmailAsync(string? username);
        Task<IEnumerable<UserModel>> GetAllUserAsync();
        Task<bool> VerifyUserByEmailAsync(string? email);
        Task<bool> VerifyUserAsync(int? id);
        Task UpdateUserAsync(UserModel? user);
        Task RemoveUserAsync(int? id);
    }
}