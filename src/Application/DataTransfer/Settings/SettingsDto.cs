using BlogTemplate.Application.Abstractions.Mappings;
using Microsoft.AspNetCore.Http;

namespace BlogTemplate.Application.DataTransfer.Settings
{
    public class SettingsDto : IMapWith<Domain.Models.Setting>
    {
        public int Id { get; set; }
        public string? SiteName { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? GithubUrl { get; set; }
        public IFormFile? Thumbnail { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Domain.Models.Setting, SettingsDto>()
                .ForMember(d => d.Id,
                    opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Title,
                    opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.SiteName,
                    opt => opt.MapFrom(s => s.SiteName))
                .ForMember(d => d.ShortDescription,
                    opt => opt.MapFrom(s => s.ShortDescription))
                .ForMember(d => d.ThumbnailUrl,
                    opt => opt.MapFrom(s => s.ThumbnailUrl))
                .ForMember(d => d.FacebookUrl,
                    opt => opt.MapFrom(s => s.FacebookUrl))
                .ForMember(d => d.TwitterUrl,
                    opt => opt.MapFrom(s => s.TwitterUrl))
                .ForMember(d => d.GithubUrl,
                    opt => opt.MapFrom(s => s.GithubUrl));
        }
    }
}
