using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Application.DTOs;
using LinkTree.Domain.Models;

namespace LinkTree.Application.Services.Interfaces
{
    public interface ILinksService
    {
        Task addLinks(LinksDTO? links);
        Task<LinksDTO> GetLinks(int? idUser);
        Task updateLinks(LinkModel? link);
        Task removeLinks(int? links);
    }
}