using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAggregate.Events;

public class JobValidityDateChangedEvent : DomainEvent
{
    private JobValidityDateChangedEvent(){} // for serialization

    public JobValidityDateChangedEvent(Job job)
    {
        Check.NotNull(job, nameof(job));

        JobId = job.Id;
        ValidityDate = job.ValidityDate;
    }

    public Guid JobId { get; private set; }
    public DateTime ValidityDate { get; private set; }
}