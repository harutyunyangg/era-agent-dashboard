// EraAgentDashboard.Application/Features/CallLogs/Queries/GetCallLogsQueryHandler.cs
using AutoMapper;
using EraAgentDashboard.Application.Interfaces; // Assuming IVapiClient is here
using EraAgentDashboard.Application.Features.CallLogs.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EraAgentDashboard.Application.Features.CallLogs.Queries;

public class GetCallLogsQuery : IRequest<IEnumerable<CallLogDto>> { }

public class GetCallLogsQueryHandler : IRequestHandler<GetCallLogsQuery, IEnumerable<CallLogDto>>
{
    private readonly IVapiClient _vapiClient;
    private readonly IMapper _mapper; // Ensure IMapper is injected
    private readonly ILogger<GetCallLogsQueryHandler> _logger;

    public GetCallLogsQueryHandler(IVapiClient vapiClient, IMapper mapper, ILogger<GetCallLogsQueryHandler> logger)
    {
        _vapiClient = vapiClient ?? throw new ArgumentNullException(nameof(vapiClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CallLogDto>> Handle(GetCallLogsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Fetch Domain Entities (VapiClient now returns Domain Entities thanks to Infrastructure mapping)
            var domainLogs = await _vapiClient.GetCallLogsAsync(cancellationToken);

            if (domainLogs == null || !domainLogs.Any())
            {
                return Enumerable.Empty<CallLogDto>();
            }

            // 2. Map Domain Entities to Application DTOs using Application mapping profile
            var dtos = _mapper.Map<IEnumerable<CallLogDto>>(domainLogs);

            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving or mapping call logs in GetCallLogsQueryHandler.");
            // Depending on requirements, you might return empty or throw a specific application exception
            return Enumerable.Empty<CallLogDto>();
        }
    }
}
