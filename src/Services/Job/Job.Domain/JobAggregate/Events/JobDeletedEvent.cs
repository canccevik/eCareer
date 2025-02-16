using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events;

public class JobDeletedEvent : DomainEvent
{
    private JobDeletedEvent(){} // for serialization
        
    public JobDeletedEvent(Job job)
    {
        Check.NotNull(job, nameof(job));
        JobId = job.Id;
    }

    public Guid JobId { get; private set; }
}