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
    public class LinkRepository: ILinkRepository
    {
        private readonly ILogger<ILinkRepository> _logger;
        private readonly AppDbContext _context;


        public LinkRepository(AppDbContext context, ILogger<ILinkRepository> logger){
            _context = context;
            _logger = logger;
        }

        public async Task AddLinkAsync(List<LinkModel>? links){
            if (links.Count == 0){
                _logger.LogError("Parametre is null in link repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                await _context.LinkSet.AddRangeAsync(links);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Success in add links");

            }catch(Exception ex){
                _logger.LogError("An error occurred while add link in database => " + ex);
                throw new InternalServerErrorException("An error occurred while add link => " + ex.Message);    
            }
        }
        public async Task<IEnumerable<LinkModel>> GetLinksByIdUserAsync(int? id){
            if (id == null){
                _logger.LogError("Parametre is null in link repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var links = await _context.LinkSet.Where(x => x.IdUser == id).ToListAsync();
                if (links.Count == 0){
                    _logger.LogError($"Not Found links with ID user equals {id}");
                    throw new NotFoundException($"Not Found link with ID {id}");
                }
                _logger.LogInformation($"Success in fetch links with ID {id}");
                return links;

            } catch(NotFoundException ex){
                _logger.LogError($"Not Found links with ID {id} => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while fetch links with ID {id} => " + ex);
                throw new InternalServerErrorException("An error occurred while fetch links => " + ex.Message);
            }
        }
        public async Task UpdateLinkAync(LinkModel? link){
             if (link == null){
                _logger.LogError("Parametre is null in link repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var existLink = await _context.LinkSet.AnyAsync(x => x.Id == link.Id);
                if (existLink == false){
                    _logger.LogError($"Link with ID {link.Id} not found");
                    throw new NotFoundException($"Not Found link with ID {link.Id} to update");
                }
                _context.LinkSet.Update(link);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Success in update link with ID {link.Id}");

            }catch(NotFoundException ex){
                _logger.LogError($"Cannot be update link with this ID {link.Id} => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while update links with ID {link.Id} => " + ex);
                throw new InternalServerErrorException("An error occurred while update links => " + ex.Message);
            }
        }
        public async Task RemoveLinkAsync(int? id){
             if (id == null){
                _logger.LogError("Parametre is null in link repository");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var links = await _context.LinkSet.FindAsync(id);
                if (links == null){
                    _logger.LogError($"Not Found links with ID {id}");
                    throw new NotFoundException($"Not Found link with ID {id}");
                }
                _context.LinkSet.Remove(links);
                await  _context.SaveChangesAsync();
                _logger.LogInformation($"Success in remove link with ID {id}");

            } catch(NotFoundException ex){
                _logger.LogError($"Not Found link with ID {id} => " + ex);
                throw new NotFoundException(ex.Message);
            }catch(Exception ex){
                _logger.LogError($"An error occurred while remove link with ID {id} => " + ex);
                throw new InternalServerErrorException("An error occurred while remove link => " + ex.Message);
            }
        }


    }
}