using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Application.DTOs;
using LinkTree.Application.Services.Interfaces;
using LinkTree.Domain.Exceptions;
using LinkTree.Domain.Models;
using LinkTree.Infrastruture.Repository.Interfaces;

namespace LinkTree.Application.Services.Implementation
{
    public class LinksServices: ILinksService
    {
        private readonly ILogger<ILinksService> _logger;
        private readonly ILinkRepository _linkRepository;
        private readonly IUserRepository _userRepository;

        public LinksServices(ILinkRepository linkRepository, ILogger<ILinksService> logger, IUserRepository userRepository){
            _linkRepository = linkRepository;
            _userRepository = userRepository;
            _logger = logger;
        }


        public  async Task addLinks(LinksDTO? links){
            if (links == null){
                _logger.LogError("Parametre is null in link services");
                throw new BadRequestException("Parametre is null");
            }
            try{
                var linksRepository = new List<LinkModel>();
                linksRepository.AddRange(links.Links);
                await _linkRepository.AddLinkAsync(linksRepository);
                _logger.LogInformation("Success in add links");

            }catch(Exception ex){
                _logger.LogError("An exception occurred while add links in repository => " + ex);
                throw new InternalServerErrorException("An error occurred in database => " + ex.Message);
            }
        }
        public  async Task<LinksDTO> GetLinks(int? idUser){
            try{
                var links = await _linkRepository.GetLinksByIdUserAsync(idUser);
                LinksDTO linksDTO = new LinksDTO();
                linksDTO.Links.AddRange(links);
                _logger.LogInformation("Success in fetch links in repository");
                return linksDTO;

            }catch(Exception ex){
                _logger.LogError("An exception occurred while fetch links in repository => " + ex);
                throw new InternalServerErrorException("An error occurred while fetch links in reopository => " + ex.Message);
            }
        }
        public  async Task updateLinks(LinkModel? link){
            try{
                await _linkRepository.UpdateLinkAync(link);
                _logger.LogInformation("Success in update link in repository");

            }catch(Exception ex){
                _logger.LogError("An exception occurred while update link in repository => " + ex);
                throw new InternalServerErrorException("An error occurred while update link in reopository => " + ex.Message);
            }
        }
        public  async Task removeLinks(int? id){
            try{
                await _linkRepository.RemoveLinkAsync(id);
                _logger.LogInformation("Success in remove link in repository ");

            }catch(Exception ex){
                _logger.LogError("An exception occurred while remove links in repository => " + ex);
                throw new InternalServerErrorException("An error occurred while remove links in reopository => " + ex.Message);
            }
        }
    }
}