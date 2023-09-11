using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Settings;
using MediatR;

namespace BlogTemplate.Application.Features.Settings.Queries.GetAll
{
    public class GetAllSettingsQuery : IRequest<Result<List<SettingsDto>>>
    {
    }
}
