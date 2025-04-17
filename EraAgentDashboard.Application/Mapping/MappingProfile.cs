// EraAgentDashboard.Application/Mapping/MappingProfile.cs
using AutoMapper;
using EraAgentDashboard.Application.Features.CallLogs.DTOs;
using EraAgentDashboard.Domain.Entities; // Assuming Domain entities are referenced

namespace EraAgentDashboard.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map Domain CallLog to Application CallLogDto
        CreateMap<Domain.Entities.CallLog, CallLogDto>()
            .ForMember(dest => dest.DestinationNumber, opt => opt.MapFrom(src =>
                src.Destination != null ? src.Destination.Number : null)) // Existing mapping for destination
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src =>
                src.Customer != null ? src.Customer.Number : null)); // <-- Add mapping for customer number

        // Add other mappings as needed (e.g., for other features/DTOs)
    }
}
