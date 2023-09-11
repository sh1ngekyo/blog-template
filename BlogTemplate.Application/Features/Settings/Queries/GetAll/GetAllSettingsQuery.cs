using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Settings;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Settings.Queries.GetAll
{
    public class GetAllSettingsQuery : IRequest<Result<List<SettingsDto>>>
    {
    }
}
