using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Domain.Models;

namespace LinkTree.Infrastruture.Repository.Interfaces
{
    public interface ILinkRepository
    {
        Task AddLinkAsync(List<LinkModel>? links);
        Task<IEnumerable<LinkModel>> GetLinksByIdUserAsync(int? id);
        Task UpdateLinkAync(LinkModel? link);
        Task RemoveLinkAsync(int? id);
        // Task<LinkModel> GetLinkByUrlAsync(string? url);
        // Task<LinkModel> GetLinkByIdAsync(int? id);
    }
}