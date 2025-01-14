using AutoMapper;
using Bogus;
using Career.Repositories.UnitOfWok;
using Company.Application.Company.Commands.UpdateCompanyDetails;
using Company.Application.Company.Dtos;
using Company.Application.Company.Exceptions;
using Company.Domain.Repositories;
using Company.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Company.Tests.IntegrationTests.Company;

public class UpdateCompanyDetailsTests
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly ISectorRefRepository _sectorRefRepository;
    private readonly ILogger<UpdateCompanyDetailsHandler> _logger;

    public UpdateCompanyDetailsTests()
    {
        _mapper = Substitute.For<IMapper>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _sectorRefRepository = Substitute.For<ISectorRefRepository>();
        _logger = Substitute.For<ILogger<UpdateCompanyDetailsHandler>>();
    }

    [Fact]
    public async Task UpdateCompanyDetailsHandler_ShouldReturnNewInfo_WhenCompanyInfoChanged()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var updateCompanyCommand = GetCommand(company.Id);
        var commandHandler = new UpdateCompanyDetailsHandler(_mapper, _unitOfWork, _companyRepository, _sectorRefRepository, _logger);

        _companyRepository.GetCompanyByIdAsync(updateCompanyCommand.CompanyId).Returns(company);
        _mapper.Map<CompanyDetailDto>(company).Returns(updateCompanyCommand.Company);

        // Act
        CompanyDetailDto result = await commandHandler.Handle(updateCompanyCommand, CancellationToken.None);

        // Assert
        await _unitOfWork.Received().SaveChangesAsync();
        Assert.Equal(updateCompanyCommand.Company, result);
        Assert.Equal(updateCompanyCommand.Company.Sector.RefId, company.Sector.RefId);
        Assert.Equal(updateCompanyCommand.Company.EmployeesCount, company.EmployeesCount);
    }

    [Fact]
    public async Task UpdateCompanyDetailsHandler_ThrowNotFoundException_WhenCompanyNotExists()
    {
        // Arrange
        var company = CompanyFaker.CreateFakeCompany();
        var updateCompanyCommand = GetCommand(company.Id);
        var commandHandler = new UpdateCompanyDetailsHandler(_mapper, _unitOfWork, _companyRepository, _sectorRefRepository, _logger);

        _companyRepository.GetCompanyByIdAsync(company.Id).ReturnsNull();

        // Act
        var actualException = await Assert.ThrowsAsync<CompanyNotFoundException>(() => commandHandler.Handle(updateCompanyCommand, CancellationToken.None));

        // Assert
        Assert.NotNull(actualException);
    }

    private UpdateCompanyDetailsCommand GetCommand(Guid companyId)
    {
        var faker = new Faker();
        var companyDetailDto = new CompanyDetailDto
        {
            Phone = faker.Phone.PhoneNumber(),
            Website = faker.Internet.Url(),
            EmployeesCount = faker.Random.Int(20, 1000),
            EstablishedYear = faker.Random.Short(1980, (short) DateTime.Now.Year),
            FaxNumber = faker.Phone.PhoneNumber(),
            MobilePhone = faker.Phone.PhoneNumber(),
            Sector = new IdNameRefDto()
            {
                RefId = faker.Random.Guid().ToString(),
                Name = faker.Random.Word()
            }
        };

        return new UpdateCompanyDetailsCommand(companyId, companyDetailDto);
    }
}