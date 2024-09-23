using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkTree.Application.DTOs
{
    public class UserUpdateDTO
    {
        public string? Email { get; set; }
        public string? Username { get; set;}
        public string? NewPassword { get; set; }


        public UserUpdateDTO(){}
        public UserUpdateDTO(string? email, string? newPassword){
            Email = email;
            NewPassword = newPassword;
        }
    }
}