using AutoMapper;

using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.User;
using BlogTemplate.Domain.Models;
using MediatR;

namespace BlogTemplate.Application.Features.User.Queries.GetByName
{
    public class GetUserByNameQueryHandler 
        : IRequestHandler<GetUserByNameQuery, Result<UserDto>>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetUserByNameQueryHandler(IUserManagerProxy<ApplicationUser> userManager, IMapper mapper) =>
            (_userManager, _mapper) = (userManager, mapper);

        public async Task<Result<UserDto>> Handle(GetUserByNameQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByUserNameAsync(request.UserName!);
            if(user == null)
            {
                return new Result<UserDto>(ErrorType.NotFound);
            }
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Role = (await _userManager.GetRolesAsync(user!)).FirstOrDefault();
            return new Result<UserDto>(ResultType.Ok).SetOutput(userDto);
        }
    }
}
