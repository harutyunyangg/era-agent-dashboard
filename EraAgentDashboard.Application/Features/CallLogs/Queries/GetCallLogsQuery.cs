// EraAgentDashboard.Application/Features/CallLogs/Queries/GetCallLogsQueryHandler.cs
using MediatR;
using EraAgentDashboard.Application.Interfaces;
using EraAgentDashboard.Application.Features.CallLogs.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq; // For mapping
                   // using AutoMapper; // Or use AutoMapper

namespace EraAgentDashboard.Application.Features.CallLogs.Queries;

public record GetCallLogsQuery : IRequest<IEnumerable<CallLogDto>>; // Request returns a list of DTOs

public class GetCallLogsQueryHandler : IRequestHandler<GetCallLogsQuery, IEnumerable<CallLogDto>>
{
    private readonly IVapiClient _vapiClient;
    // private readonly IMapper _mapper; // If using AutoMapper

    public GetCallLogsQueryHandler(IVapiClient vapiClient /*, IMapper mapper*/)
    {
        _vapiClient = vapiClient;
        // _mapper = mapper;
    }

    public async Task<IEnumerable<CallLogDto>> Handle(GetCallLogsQuery request, CancellationToken cancellationToken)
    {
        var domainLogs = await _vapiClient.GetCallLogsAsync(cancellationToken);

        // Manual Mapping (or use AutoMapper)
        var dtos = domainLogs.Select(log => new CallLogDto
        {
            Id = log.Id,
            CreatedAt = log.CreatedAt,
            Type = log.Type,
            Status = log.Status,
            Cost = log.Cost,
            DestinationNumber = log.DestinationNumber
            // Map other needed properties
        }).ToList();

        // return _mapper.Map<IEnumerable<CallLogDto>>(domainLogs); // AutoMapper version
        return dtos;
    }
}
