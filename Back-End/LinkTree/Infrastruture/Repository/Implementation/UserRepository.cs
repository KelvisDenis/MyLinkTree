using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Domain.Exceptions;
using LinkTree.Domain.Models;
using LinkTree.Infrastruture.Data;
using LinkTree.Infrastruture.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkTree.Infrastruture.Repository.Implementation
{
    public class UserRepository:IUserRepository
    {
        private readonly ILogger<IUserRepository> _logger;
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context, ILogger<IUserRepository> logger){
            _context = context;
            _logger = logger;
        }

        public  async Task AddUserAsync(UserModel? user){
            if (user == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                await _context.UserSet.AddAsync(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Success in add user");

            }catch(Exception ex){
                _logger.LogError("An error occurred while add user in database => " + ex);
                throw new InternalServerErrorException("An error occurred while add user => " + ex.Message);    
            }
        }
        public  async Task<UserModel> GetUserByIdAsync(int? id){
            if (id == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var user = await _context.UserSet.FindAsync(id);
                if (user == null){
                    _logger.LogError($"Not Found user with ID equals {id}");
                    throw new NotFoundException($"Not Found user with ID {id}");
                }
                _logger.LogInformation($"Success in fetch user with ID {id}");
                return user;

            } catch(NotFoundException ex){
                _logger.LogError($"Not Found user with ID {id} => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while fetch user with ID {id} => " + ex);
                throw new InternalServerErrorException("An error occurred while fetch user => " + ex.Message);
            }
        }
        public  async Task<UserModel> GetUserByUsernameAsync(string? username){
            if (username == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var user = await _context.UserSet.FirstOrDefaultAsync(x => x.Username == username);
                if (user == null){
                    _logger.LogError($"Not Found user with username equals {username}");
                    throw new NotFoundException($"Not Found user with username {username}");
                }
                _logger.LogInformation($"Success in fetch user with username {username}");
                return user;

            } catch(NotFoundException ex){
                _logger.LogError($"Not Found user with username {username} => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while fetch user with username {username} => " + ex);
                throw new InternalServerErrorException("An error occurred while fetch user => " + ex.Message);
            }
        }
        public async Task<UserModel> GetUserByEmailAsync(string? email){
               if (email == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var user = await _context.UserSet.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null){
                    _logger.LogError($"Not Found user with email equals {email}");
                    throw new NotFoundException($"Not Found user with email {email}");
                }
                _logger.LogInformation($"Success in fetch user with email {email}");
                return user;

            } catch(NotFoundException ex){
                _logger.LogError($"Not Found user with email {email} => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while fetch user with email {email} => " + ex);
                throw new InternalServerErrorException("An error occurred while fetch user => " + ex.Message);
            }
        }

         public async Task<IEnumerable<UserModel>> GetAllUserAsync(){
            try{
                var users = await _context.UserSet.ToListAsync();
                _logger.LogInformation($"Success in fetch all user");
                return users;

            } catch(NotFoundException ex){
                _logger.LogError($"Not Found to all user with => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while fetch all user => " + ex);
                throw new InternalServerErrorException("An error occurred while fetch user => " + ex.Message);
            }
        }

        public async Task<bool> VerifyUserAsync(int? id){
              if (id == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var existUser = await _context.UserSet.AnyAsync(x => x.Id == id);
                _logger.LogInformation($"Success in veridy user with ID {id}");
                return existUser;

            }catch(Exception ex){
                _logger.LogError($"An error occurred while upadate user with this ID {id} => " + ex);
                throw new InternalServerErrorException("An error occurred while verify user => " + ex.Message);    
            }
        }
        public async Task<bool> VerifyUserByEmailAsync(string? email){
              if (email == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var existUser = await _context.UserSet.AnyAsync(x => x.Email == email);
                _logger.LogInformation($"Success in veridy user with this email {email}");
                return existUser;

            }catch(Exception ex){
                _logger.LogError($"An error occurred while veridy user with this email {email} => " + ex);
                throw new InternalServerErrorException("An error occurred while verify user => " + ex.Message);    
            }
        }

        public  async Task UpdateUserAsync(UserModel? user){
            if (user == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null"); 
            }
            try{
                var existUser = await _context.UserSet.AnyAsync(x => x.Email == user.Email);
                if (existUser == false){
                    _logger.LogError($"user with ID {user.Id} not found");
                    throw new NotFoundException($"Not Found user with ID {user.Id} to update");
                } 
                _context.UserSet.Update(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Success in update user with ID {user.Id}");

            }catch(Exception ex){
                _logger.LogError($"An error occurred while upadate user ID {user.Id} => " + ex);
                throw new InternalServerErrorException("An error occurred while update user => " + ex.Message);    
            }
        }
        public  async Task RemoveUserAsync(int? id){
             if (id == null){
                _logger.LogError("Parametre is null in user repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var user = await _context.UserSet.FindAsync(id);
                if (user == null){
                    _logger.LogError($"Not Found user with ID {id}");
                    throw new NotFoundException($"Not Found user with ID {id}");
                }
                _context.UserSet.Remove(user);
                await  _context.SaveChangesAsync();
                _logger.LogInformation($"Success in remove user with ID {id}");

            } catch(NotFoundException ex){
                _logger.LogError($"Not Found user with ID {id} => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while remove user with ID {id} => " + ex);
                throw new InternalServerErrorException("An error occurred while remove user => " + ex.Message);
            }
        }
    }
}