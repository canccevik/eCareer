using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAdvertAggregate.DomainEvents
{
    public class JobAdvertRevokedEvent : DomainEvent
    {
        public JobAdvertRevokedEvent(JobAdvert jobAdvert)
        {
            Check.NotNull(jobAdvert, nameof(jobAdvert));
            JobAdvert = jobAdvert;
        }

        public JobAdvert JobAdvert { get; }
    }
}