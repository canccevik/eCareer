using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore;
using AspectCore.Aspects;
using Career.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Career.Domain.DomainEvent.Dispatcher
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class DispacthDomainEventAttribute : AspectAttribute
    {
        private IDomainEventDispatcher _dispatcher;
        private ILogger<DispacthDomainEventAttribute> _logger;

        public override void OnSuccess(MethodExecutionArgs args)
        {
            IEnumerable<IDomainEvent> domainEvents = GetDomainEventsFromArgs(args.Arguments);
            if (domainEvents.Any())
            {
                _dispatcher.Dispatch(domainEvents);
                ClearAllDomainEventsFromArgs(args.Arguments);
                
                _logger.LogInformation("Domain events were dispatched and clear.");
            }
        }

        public override async Task OnSuccessAsync(MethodExecutionArgs args)
        {
            IEnumerable<IDomainEvent> domainEvents = GetDomainEventsFromArgs(args.Arguments);
            if (domainEvents.Any())
            {
                await _dispatcher.Dispatch(domainEvents);
                ClearAllDomainEventsFromArgs(args.Arguments);
                
                _logger.LogInformation("Domain events were dispatched and clear.");
            }
        }

        public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
        {
            _dispatcher ??= serviceProvider.GetRequiredService<IDomainEventDispatcher>();
            _logger ??= serviceProvider.GetRequiredService<ILogger<DispacthDomainEventAttribute>>();

            if (_dispatcher == null)
                throw new ArgumentException("IDomainEventDispatcher is not registered on DI.");

            return base.LoadDependencies(serviceProvider);
        }

        private IEnumerable<IDomainEvent> GetDomainEventsFromArgs(object[] args)
        {
            var domainEventList = new List<IDomainEvent>();

            if (args != null && args.Any())
            {
                foreach (object argument in args)
                {
                    if (argument is DomainEntity domainEntity)
                        domainEventList.AddRange(domainEntity.DomainEvents);
                }
            }

            return domainEventList;
        }
        
        private void ClearAllDomainEventsFromArgs(object[] args)
        {
            if (args != null && args.Any())
            {
                foreach (object argument in args)
                {
                    if (argument is DomainEntity domainEntity)
                        domainEntity.ClearDomainEvents();
                }
            }
        }
    }
}