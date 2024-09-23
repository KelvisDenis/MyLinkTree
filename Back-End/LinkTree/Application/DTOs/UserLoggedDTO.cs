using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkTree.Application.DTOs
{
    public class UserLoggedDTO
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set;}
        public int? Expired { get; set; }


        public UserLoggedDTO(){}
        public UserLoggedDTO(int? id,string? userName, string? token, int? expired){
            Id = id;
            UserName = userName;
            Token = token;
            Expired = expired;
        }
    }
}