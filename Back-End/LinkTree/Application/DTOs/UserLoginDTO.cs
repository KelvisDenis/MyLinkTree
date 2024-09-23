using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkTree.Application.DTOs
{
    public class UserLoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set;}


        public UserLoginDTO(){}
        public UserLoginDTO(string? email, string? password){
            Email = email;
            Password = password;
        }
    }
}