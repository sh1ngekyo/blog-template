using BlogTemplate.Application.Abstractions.Mappings;
using BlogTemplate.Domain.Models;

namespace BlogTemplate.Application.DataTransfer.User
{
    public class UserDto : IMapWith<ApplicationUser>
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? About { get; set; }
        public string? ThumbnailUrl { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserDto>()
                .ForMember(userDto => userDto.Id,
                    opt => opt.MapFrom(user => user.Id))
                .ForMember(userDto => userDto.FirstName,
                    opt => opt.MapFrom(user => user.FirstName))
                .ForMember(userDto => userDto.LastName,
                    opt => opt.MapFrom(user => user.LastName))
                .ForMember(userDto => userDto.UserName,
                    opt => opt.MapFrom(user => user.UserName))
                .ForMember(userDto => userDto.Email,
                    opt => opt.MapFrom(user => user.Email))
                .ForMember(userDto => userDto.About,
                    opt => opt.MapFrom(user => user.About))
                .ForMember(userDto => userDto.ThumbnailUrl,
                    opt => opt.MapFrom(user => user.ThumbnailUrl));
        }
    }
}
