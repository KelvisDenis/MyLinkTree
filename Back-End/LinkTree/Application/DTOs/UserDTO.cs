using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkTree.Application.DTOs
{
    public class UserDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        [MinLength(8)]
        public string? Password { get; set;}
        [Required]
        [MinLength(6)]
        public string? UserName { get; set;}



        public UserDTO(){}
        public UserDTO(string? email, string? password, string? userName, int idPeople){
            Email = email;
            Password = password;
            UserName = userName;
        }
    }
}