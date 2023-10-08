using BlogTemplate.Application.Abstractions.Mappings;
using BlogTemplate.Domain.Models;

namespace BlogTemplate.Application.DataTransfer.Post
{
    public class PostDto : IMapWith<Domain.Models.Page>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Comment>? Comments { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Domain.Models.Post, PostDto>()
                .ForMember(postDto => postDto.Id,
                    opt => opt.MapFrom(post => post.Id))
                .ForMember(postDto => postDto.Title,
                    opt => opt.MapFrom(post => post.Title))
                .ForMember(postDto => postDto.AuthorName,
                    opt => opt.MapFrom(post => post.ApplicationUser!.UserName))
                .ForMember(postDto => postDto.CreatedDate,
                    opt => opt.MapFrom(post => post.CreatedDate))
                .ForMember(postDto => postDto.Comments,
                    opt => opt.MapFrom(post => post.Comments))
                .ForMember(postDto => postDto.ThumbnailUrl,
                    opt => opt.MapFrom(post => post.ThumbnailUrl))
                .ForMember(postDto => postDto.ShortDescription,
                    opt => opt.MapFrom(post => post.ShortDescription))
                .ForMember(postDto => postDto.Slug,
                    opt => opt.MapFrom(post => post.Slug))
                .ForMember(postDto => postDto.Description,
                    opt => opt.MapFrom(post => post.Description));
        }
    }
}
