using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkTree.Domain.Models
{
    public class LinkModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string? Name { get; set; }
        [Required]
        [Url]
        public string? Url { get; set; }
        [Required]
        public int IdUser { get; set; }

        public LinkModel(){}
        public LinkModel(int id, string? name, string? url, int idUser){
            Id = id;
            Name = name;
            Url = url;
            IdUser = idUser;
        }
    }
}