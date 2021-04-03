using AutoMapper;
using Job.Application.Job.Dtos;
using Job.Domain.JobAggregate;

namespace Job.Application.Job
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            CreateMap<Domain.JobAggregate.Job, JobDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(j => j.Status.Name))
                .IncludeAllDerived();

            CreateMap<Domain.JobAggregate.Job, JobDetailDto>();
            CreateMap<TagRef, TagDto>();

            CreateMap<WorkTypeRef, WorkTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.WorkTypeId));

            CreateMap<EducationLevelRef, EducationLevelDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.EducationLevelId));

            CreateMap<LocationRef, JobLocationDto>();
        }
    }
}