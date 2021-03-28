using System;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Job.Application.Job.Commands.CreateJob;
using Job.Application.Job.Dtos;
using Job.Domain.JobAggregate.Constants;
using Job.Domain.JobAggregate.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Job.Test.IntegrationTests.Job
{
    public class CreateJobHandlerTests
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<CreateJobCommandHandler> _logger;

        public CreateJobHandlerTests()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _logger = Substitute.For<ILogger<CreateJobCommandHandler>>();
        }
        
        [Fact]
        public async Task CreateJobHandler_ShouldReturnCreatedJobId_WhenJobCreated()
        {
            // Arrange
            var command = GetCommand();
            var commandHandler = new CreateJobCommandHandler(_jobRepository, _logger);

            // Act
            Guid jobId = await commandHandler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.NotEqual(Guid.Empty, jobId);
            await _jobRepository.Received().AddAsync(Arg.Any<Domain.JobAggregate.Job>());
        }
        
        [Fact]
        public async Task CreateJobHandler_ShouldBeLogInformation_WhenJobCreated()
        {
            // Arrange
            var command = GetCommand();
            var commandHandler = new CreateJobCommandHandler(_jobRepository, _logger);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            
            // Assert
            _logger.ReceivedWithAnyArgs().LogInformation("");
        }

        private CreateJobCommand GetCommand()
        {
            var jobDto = new Faker<JobInputDto>()
                .Rules((faker,job) =>
                {
                    job.Title = faker.Lorem.Sentence(3);
                    job.Description = faker.Lorem.Paragraph();
                    job.Gender = faker.PickRandom<GenderType>();
                    job.LanguageId = faker.Random.Guid().ToString();
                    job.PersonCount = faker.Random.Short(0, 50);
                    job.SectorId = faker.Random.Guid().ToString();
                    job.IsCanDisabilities = faker.Random.Bool();
                    job.JobPositionId = faker.Random.Guid().ToString();
                    job.MinExperienceYear = faker.Random.Byte(0, 20);
                    job.MaxExperienceYear = faker.Random.Byte(job.MinExperienceYear.Value, 20);
                }).Generate();

            return new CreateJobCommand(Guid.NewGuid(), jobDto);
        }
    }
}