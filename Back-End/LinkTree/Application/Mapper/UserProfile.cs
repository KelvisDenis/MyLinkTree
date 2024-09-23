using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinkTree.Application.DTOs;
using LinkTree.Domain.Models;

namespace LinkTree.Application.Mapper
{
    public class UserProfile:Profile
    {
        public UserProfile(){
            CreateMap<UserModel, UserLoggedDTO>();
        }
    }
}