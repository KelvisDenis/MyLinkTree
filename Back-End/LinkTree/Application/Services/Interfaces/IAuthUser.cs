using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Application.DTOs;

namespace LinkTree.Application.Services.Interfaces
{
    public interface IAuthUser
    {
        Task<UserLoggedDTO> Auth(UserLoginDTO? login);
    }
}