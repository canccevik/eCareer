using Career.MediatR.Command;
using Job.Application.Job.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Job.Application.Job.Commands.RemoveWorkType;

public class RemoveWorkTypeCommandHandler: ICommandHandler<RemoveWorkTypeCommand>
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger<RemoveWorkTypeCommandHandler> _logger;

    public RemoveWorkTypeCommandHandler(IJobRepository jobRepository, ILogger<RemoveWorkTypeCommandHandler> logger)
    {
        _logger = logger;
        _jobRepository = jobRepository;
    }

    public async Task<Unit> Handle(RemoveWorkTypeCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job is null)
            throw new JobNotFoundException(request.JobId);

        job.RemoveWorkType(request.WorkTypeId);
        await _jobRepository.UpdateAsync(job.Id, job);
            
        _logger.LogInformation("Work type {WorkTypeId} removed from job: {JobId}", request.WorkTypeId, job.Id);
            
        return Unit.Value;
    }
}