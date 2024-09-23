using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Domain.Models;

namespace LinkTree.Application.DTOs
{
    public class LinksDTO
    {
        public List<LinkModel>? Links { get; set; } = new List<LinkModel>();

        public LinksDTO(){}
    }
}