using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAdvertAggregate.Events
{
    public class JobAdvertDeletedEvent : DomainEvent
    {
        public JobAdvertDeletedEvent(JobAdvert jobAdvert)
        {
            Check.NotNull(jobAdvert, nameof(jobAdvert));
            JobAdvert = jobAdvert;
        }

        public JobAdvert JobAdvert { get; }
    }
}