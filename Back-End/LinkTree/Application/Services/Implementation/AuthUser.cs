using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkTree.Application.DTOs;
using LinkTree.Application.Services.Interfaces;
using LinkTree.Domain.Exceptions;
using LinkTree.Infrastruture.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LinkTree.Application.Services.Implementation
{
    public class AuthUser:IAuthUser
    {
        private readonly IMapper _mapper;
        private readonly ILogger<IAuthUser> _logger;
        private readonly IUserRepository _userRepository;

        public AuthUser(IMapper mapper, ILogger<IAuthUser> logger, IUserRepository userRepository){
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<UserLoggedDTO> Auth(UserLoginDTO? login){
            if (login == null){
                _logger.LogError("Parametre in auth is null");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var user = await _userRepository.GetUserByEmailAsync(login.Email);
                if (user == null){
                    _logger.LogError($"Not Found user with this email {login.Email}");
                    throw new NotFoundException($"Not Found user with this email {login.Email}");
                }
                var verifyHash = BCrypt.Net.BCrypt.Verify(login.Password, user.HashPassword);
                if(verifyHash == false){
                    _logger.LogError($"Was not possible fetch ");
                    throw new NotFoundException("Not Found user model, enrollment or password invalid");
                }

                 // Gera o token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("a_secure_key_with_32_bytes_minimum"); // Chave secreta para o token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Username)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // Mapper 
                var userDto = _mapper.Map<UserLoggedDTO>(user); 
                userDto.Token = tokenHandler.WriteToken(token);
                userDto.Expired = tokenDescriptor.Expires.Value.Minute;

                return userDto;
            
            }catch(Exception ex){
                _logger.LogError("An error occurred while authenticate user => " + ex);
                throw new BadRequestException("An error occurred in process is authenticate => " + ex.Message);
            }
        }
    }
}
