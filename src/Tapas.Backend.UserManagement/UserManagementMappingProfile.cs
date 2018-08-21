namespace Tapas.Backend.UserManagement
{
    using Areas.Backend.Models.CreateUser;
    using Areas.Backend.Models.EditUser;
    using AutoMapper;
    using Data.EntityFramework.Entities;

    public class UserManagementMappingProfile : Profile
    {
        public UserManagementMappingProfile()
        {
            CreateMap<CreateUserDto, CreateUserViewModel>().ReverseMap();
            CreateMap<EditUserDto, EditUserViewModel>().ReverseMap();
            CreateMap<ApplicationUser, EditUserViewModel>().ReverseMap();
        }
    }
}