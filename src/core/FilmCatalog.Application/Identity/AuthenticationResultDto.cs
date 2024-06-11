using AutoMapper;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Domain.Entities;

namespace FilmCatalog.Application.Identity;

public class AuthenticationResultDto : IMapFrom<AuthenticationResult>
{
    public AuthenticationResultDto()
    {
    }
    
    public string Token { get; set; }

    public int Id { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string Role { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AuthenticationResult, AuthenticationResultDto>()
            .ForMember(d => d.Token, opt => opt.MapFrom(s => s.Token))
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.User.Id))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
            .ForMember(d => d.Username, opt => opt.MapFrom(s => s.User.Username))
            .ForMember(d => d.Role, opt => opt.MapFrom(s => s.User.Role));
    }
}
