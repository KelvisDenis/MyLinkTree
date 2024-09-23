using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Application.DTOs;
using LinkTree.Domain.Models;

namespace LinkTree.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(UserDTO? user);
        Task<UserLoggedDTO> LoginUserAsync(UserLoginDTO? login);
        Task<UserDTO> GetUserByEmailAsync(string? email);
        Task<UserModel> GetUserByUsernameUserAsync(string? username);
        Task<UserDTO> GetUserByIDAsync(int? id);
        Task UpdateUserAsync(UserUpdateDTO? user);
        Task RemoveUserAsync(int? id);
    }
}