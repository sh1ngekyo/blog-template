using AutoMapper;

namespace BlogTemplate.Application.Abstractions.Mappings
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
