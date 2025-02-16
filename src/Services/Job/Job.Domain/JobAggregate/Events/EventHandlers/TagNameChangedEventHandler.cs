using Career.CAP.DomainEvent;
using DotNetCore.CAP;
using Job.Domain.JobAggregate.Services;
using Job.Domain.TagAggregate.Events;

namespace Job.Domain.JobAggregate.Events.EventHandlers;

public class TagNameChangedEventHandler: CAPDomainEventHandler<TagNameChangedEvent>
{
    private readonly IJobDomainService _jobDomainService;

    public TagNameChangedEventHandler(IJobDomainService jobDomainService)
    {
        _jobDomainService = jobDomainService;
    }

    [CapSubscribe(nameof(TagNameChangedEvent))]
    public override async Task Handle(TagNameChangedEvent domainEvent)
    {
        await _jobDomainService.UpdateTagNameFromJobsAsync(domainEvent.Tag);
    }
}