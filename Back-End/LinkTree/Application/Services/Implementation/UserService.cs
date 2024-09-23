using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using LinkTree.Application.DTOs;
using LinkTree.Application.Services.Interfaces;
using LinkTree.Domain.Exceptions;
using LinkTree.Domain.Models;
using LinkTree.Infrastruture.Repository.Interfaces;

namespace LinkTree.Application.Services.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthUser _auth;
        

        public UserService(IUserRepository userRepository, IAuthUser auth){
            _userRepository = userRepository;
            _auth = auth;
        }

        public async Task AddUserAsync(UserDTO? user){
            var verifUser = await _userRepository.VerifyUserByEmailAsync(user.Email);
            if (verifUser != false){
                throw new ConflictException("User already exists");
            }
            var userRepository = new UserModel(){
                Email = user.Email,
                HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Username = user.UserName,
                Id = 0

            };
            await _userRepository.AddUserAsync(userRepository);

        }
        public async Task<UserLoggedDTO> LoginUserAsync(UserLoginDTO? login){
            var logUser = await _auth.Auth(login);
            return logUser;

        }

        public async Task<UserDTO> GetUserByEmailAsync(string? email){
            var user = await _userRepository.GetUserByEmailAsync(email);
            var userDTO = new UserDTO{
                Email = user.Email,
                UserName = user.Username,
                
            };
            return userDTO;
        }
        public async Task UpdateUserAsync(UserUpdateDTO? user){
            var userExist = await _userRepository.GetUserByEmailAsync(user.Email);
            userExist.Username =  user.Username;
            userExist.Email = user.Email;    
            userExist.HashPassword =  BCrypt.Net.BCrypt.HashPassword(user.NewPassword);

            await _userRepository.UpdateUserAsync(userExist);
        }
        public async Task RemoveUserAsync(int? id){
            await _userRepository.RemoveUserAsync(id);
        }

        public async Task<UserModel> GetUserByUsernameUserAsync(string? username){
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return user;
        } 

        public async Task<UserDTO> GetUserByIDAsync(int? id){
            var user = await _userRepository.GetUserByIdAsync(id);
            var userDTO = new UserDTO{
                Email = user.Email,
                UserName = user.Username,
                
            };
            return userDTO;    
        }

    }
}