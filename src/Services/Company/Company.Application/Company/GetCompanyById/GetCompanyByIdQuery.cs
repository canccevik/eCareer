using System;
using Company.Application.Dtos;
using MediatR;

namespace Company.Application.Company.GetCompanyById
{
    public class GetCompanyByIdQuery: IRequest<CompanyDto>
    {
        public GetCompanyByIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}