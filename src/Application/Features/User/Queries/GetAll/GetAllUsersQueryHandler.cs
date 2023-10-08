using AutoMapper;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.User;
using BlogTemplate.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.User.Queries.GetAll
{
    public class GetAllUsersQueryHandler 
        : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUserManagerProxy<ApplicationUser> userManager, IMapper mapper) =>
            (_userManager, _mapper) = (userManager, mapper);

        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var output = new List<UserDto>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var (user, userDto) in from user in users
                                            let userDto = _mapper.Map<UserDto>(user)
                                            select (user, userDto))
            {
                userDto.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                output.Add(userDto);
            }

            return new Result<List<UserDto>>(ResultType.Ok)
                .SetOutput(output);
        }
    }
}
