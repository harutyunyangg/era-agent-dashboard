// EraAgentDashboard.Infrastructure/Mapping/VapiMappingProfile.cs
using AutoMapper;
using EraAgentDashboard.Domain.Entities;
using EraAgentDashboard.Infrastructure.HttpClients.VapiDtos;

namespace EraAgentDashboard.Infrastructure.Mapping;

public class VapiMappingProfile : Profile
{
    public VapiMappingProfile()
    {
        // Map from Vapi DTO to Domain Entity
        CreateMap<VapiCallLogDto, CallLog>()
            // AutoMapper maps properties with the same name by convention.
            // We only need explicit configuration for differences or complex logic.
            // Example: If DTO has 'Costs' and Domain has 'CostDetails'
            .ForMember(dest => dest.CostDetails, opt => opt.MapFrom(src => src.Costs));
        // If names match exactly (e.g., Id, OrgId, CreatedAt, etc.), no explicit mapping is needed here.

        // Map nested DTOs to nested Domain Entities
        // AutoMapper will automatically use these when mapping the parent object.
        CreateMap<VapiDestinationDto, CallLogDestination>();
        CreateMap<VapiCustomerDto, CallLogCustomer>();
        CreateMap<VapiCostBreakdownDto, CallLogCostBreakdown>(); // Ensure properties match or add .ForMember()
        CreateMap<VapiArtifactDto, CallLogArtifact>();         // Ensure properties match or add .ForMember()
        CreateMap<VapiCostDetailDto, CallLogCostDetail>();     // Ensure properties match or add .ForMember()

        // Add any other necessary mappings here.
        // Use .ForMember() for specific property configurations if names differ
        // or if custom transformations are needed.
        // Example:
        // CreateMap<VapiCustomerDto, CallLogCustomer>()
        //     .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name)); // If property name was different
    }
}
