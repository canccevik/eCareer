using AutoMapper;
using Company.Application.Commands.CreateCompany;
using Company.Application.Commands.UpdateCompany;
using Company.Application.Dtos.Company;

namespace Company.Application
{
    public class CompanyMappinProfile: Profile
    {
        public CompanyMappinProfile()
        {
            CreateMap<Domain.Entities.Company, CompanyDto>();
            CreateMap<CreateCompanyCommand, Domain.Entities.Company>();
            CreateMap<UpdateCompanyCommand, Domain.Entities.Company>();
            CreateMap<CompanyCommandModel, UpdateCompanyCommand>();
        }
    }
}