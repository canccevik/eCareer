using Company.Application.Dtos;
using MediatR;

namespace Company.Application.Company.CreateCompany
{
    public class CreateCompanyCommand: IRequest<CompanyDto>
    {
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public int EmployeesCount { get; set; }
        public short EstablishedYear { get; set; }
        public string SectorId { get; set; }
        public string CountryId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
    }
}