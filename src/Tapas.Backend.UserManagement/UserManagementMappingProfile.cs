namespace Tapas.Backend.UserManagement
{
    using Areas.Backend.Models.CreateUser;
    using AutoMapper;

    public class UserManagementMappingProfile : Profile
    {
        public UserManagementMappingProfile()
        {
            CreateMap<CreateUserDto, CreateUserViewModel>().ReverseMap();
        }
    }
}