using Career.MediatR.Command;
using CurriculumVitae.Application.Disability.Dtos;

namespace CurriculumVitae.Application.Disability.Commands.Add
{
    public class AddDisabilityCommand : ICommand<DisabilityDto>
    {
        public AddDisabilityCommand(string cvId, DisabilityInputDto disabilityInfo)
        {
            CvId = cvId;
            DisabilityInfo = disabilityInfo;
        }

        public string CvId { get; }
        public DisabilityInputDto DisabilityInfo { get; }
    }
}