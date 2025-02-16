using AutoMapper;
using Career.MediatR.Command;
using Job.Application.Job.Dtos;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate;
using Job.Domain.JobAggregate.Refs;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.AddLocation;

public class AddLocationCommandHandler: ICommandHandler<AddLocationCommand, JobLocationDto>
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<AddLocationCommandHandler> _logger;

    public AddLocationCommandHandler(IJobRepository jobRepository, IMapper mapper, ILogger<AddLocationCommandHandler> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<JobLocationDto> Handle(AddLocationCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job is null)
            throw new JobNotFoundException(request.JobId);

        var country = CountryRef.Create(request.LocationInputDto.CountryRef.RefId, request.LocationInputDto.CountryRef.Name);
        var city = CityRef.Create(request.LocationInputDto.CityRef.RefId, request.LocationInputDto.CityRef.Name);
            
        var location = JobLocation.Create(country, city);
        job.AddLocation(location);

        await _jobRepository.UpdateAsync(job.Id, job);
        _logger.LogInformation("New location added to job: {JobId}", job.Id);

        return _mapper.Map<JobLocationDto>(location);
    }
}