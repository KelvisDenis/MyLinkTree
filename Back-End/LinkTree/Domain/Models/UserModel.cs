using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkTree.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? HashPassword { get; set; }



        public UserModel(){}
        public UserModel(int id, string? email, string? username, string? hashPassword){
            Id = id;
            Email = email;
            Username = username;
            HashPassword = hashPassword;
        }



    }
}