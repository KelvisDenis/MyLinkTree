using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Application.DTOs;
using LinkTree.Application.Services.Interfaces;
using LinkTree.Domain.Exceptions;
using LinkTree.Infrastruture.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LinkTree.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;


        public UserApiController(IUserService userService, IUserRepository userRepository){
            _userService = userService;
            _userRepository = userRepository;
        }


        [HttpPost("User/Create-User/")]
        public async Task<IActionResult> Post([FromBody] UserDTO? userDTO){
            try{
                
                await _userService.AddUserAsync(userDTO);
                return Ok("Success");

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }

         [HttpPut("User/Update-User/")]
        public async Task<IActionResult> Put([FromBody] UserUpdateDTO? userDTO){
            try{
                await _userService.UpdateUserAsync(userDTO);
                return Ok("Success");

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }

         [HttpPost("User/Login-User/")]
        public async Task<IActionResult> Post([FromBody] UserLoginDTO? user){
            try{
                var login = await _userService.LoginUserAsync(user);
                return Ok(login);

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpDelete("User/Delete-User/{id}")]
        public async Task<IActionResult> Delete(int? id){
            try{
                await _userService.RemoveUserAsync(id);
                return Ok("Success");

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }
         [HttpGet("User/Get-User/{email}")]
        public async Task<IActionResult> Get(string? email){
            try{
                var user = await _userService.GetUserByEmailAsync(email);
                return Ok(user);

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("User/GetAll-User/")]
        public async Task<IActionResult> Get(){
            try{
                var users = await _userRepository.GetAllUserAsync();
                return Ok(users);

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }

         [HttpGet("User/Get-User-return-user/{username}")]
        public async Task<IActionResult> GetUser(string? username){
            try{
                var user = await _userService.GetUserByUsernameUserAsync(username);
                return Ok(user);

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }

         [HttpGet("User/Get-User-id/{id}")]
        public async Task<IActionResult> GetUser(int? id){
            try{
                var user = await _userService.GetUserByIDAsync(id);
                return Ok(user);

            }catch(Exception ex){
                throw new BadRequestException(ex.Message);
            }
        }
    }
}