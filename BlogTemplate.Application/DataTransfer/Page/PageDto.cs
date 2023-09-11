using BlogTemplate.Application.Abstractions.Mappings;
using Microsoft.AspNetCore.Http;

namespace BlogTemplate.Application.DataTransfer.Page
{
    public class PageDto : IMapWith<Domain.Models.Page>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public IFormFile? Thumbnail { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Domain.Models.Page, PageDto>()
                .ForMember(pageDto => pageDto.Id,
                    opt => opt.MapFrom(page => page.Id))
                .ForMember(pageDto => pageDto.Title,
                    opt => opt.MapFrom(page => page.Title))
                .ForMember(pageDto => pageDto.ShortDescription,
                    opt => opt.MapFrom(page => page.ShortDescription))
                .ForMember(pageDto => pageDto.Description,
                    opt => opt.MapFrom(page => page.Description))
                .ForMember(pageDto => pageDto.ThumbnailUrl,
                    opt => opt.MapFrom(page => page.ThumbnailUrl));
        }
    }
}
