using AutoMapper;
using Net8StarterAuthApi.Models;
using Net8StarterAuthApi.Models.DTOs;

namespace Net8StarterAuthApi.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AdminRole, AdminRoleDto>().ReverseMap();
    }
}