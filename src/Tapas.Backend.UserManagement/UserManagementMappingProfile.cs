namespace Tapas.Backend.UserManagement
{
    using Areas.Backend.Models.Roles.CreateRole;
    using Areas.Backend.Models.Roles.EditRoles;
    using Areas.Backend.Models.Users.CreateUser;
    using Areas.Backend.Models.Users.EditUser;
    using AutoMapper;
    using Data.EntityFramework.Entities;

    public class UserManagementMappingProfile : Profile
    {
        public UserManagementMappingProfile()
        {
            CreateMap<CreateUserDto, CreateUserViewModel>().ReverseMap();
            CreateMap<EditUserDto, EditUserViewModel>().ReverseMap();
            CreateMap<ApplicationUser, EditUserViewModel>().ReverseMap();

            CreateMap<CreateRoleDto, CreateRoleViewModel>().ReverseMap();
            CreateMap<EditUserDto, EditRoleViewModel>().ReverseMap();
            CreateMap<ApplicationRole, EditRoleViewModel>().ReverseMap();
        }
    }
}