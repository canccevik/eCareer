using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.MediatR.Command;
using CurriculumVitae.Application.Cv.Exceptions;
using CurriculumVitae.Application.Disability.Commands.Update;
using CurriculumVitae.Application.Disability.Exceptions;
using CurriculumVitae.Application.DisabilityType.Exceptions;
using CurriculumVitae.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CurriculumVitae.Application.Disability.Commands.Delete
{
    public class DeleteDisabilityCommandHandler : ICommandHandler<DeleteDisabilityCommand>
    {
        private readonly ICVRepository _cvRepository;
        private readonly ILogger<DeleteDisabilityCommandHandler> _logger;

        public DeleteDisabilityCommandHandler(ICVRepository cvRepository, ILogger<DeleteDisabilityCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDisabilityCommand request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByKeyAsync(request.CvId);
            if (cv == null || cv.IsDeleted)
            {
                throw new CVNotFoundException(request.CvId);
            }
            
            var disability = cv.PersonalInfo.Disabilities.FirstOrDefault(x=> x.Id == request.Id && !x.IsDeleted);
            if (disability == null)
            {
                throw new DisabilityNotFoundException(request.Id);
            }

            disability.IsDeleted = true;
            await _cvRepository.UpdateAsync(cv.Id, cv);

            _logger.LogInformation("Disability ({DisabilityId}) deleted in CV ({CvId})", disability.Id, cv.Id);

            return Unit.Value;
        }
    }
}