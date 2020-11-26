using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;

namespace Definition.HttpClient.EducationType
{
    public interface IEducationTypeHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<EducationTypeDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<EducationTypeDto>> GetByIdAsync(string id, string version = null);
    }
}