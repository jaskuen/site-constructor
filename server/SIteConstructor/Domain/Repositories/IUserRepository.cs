using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteConstructor.Domain.Entities;
using SiteConstructor.Dto;

namespace SiteConstructor.Domain.Repositories;

public interface IUserRepository
{
    public bool IsUniqueUser(string username);
    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    public Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
}
