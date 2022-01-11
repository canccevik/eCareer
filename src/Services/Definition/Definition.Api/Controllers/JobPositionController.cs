﻿using System.Threading.Tasks;
using Career.Data.Pagination;
using Definition.Api.Constants;
using Definition.Api.Controllers.Base;
using Definition.Application.Work.JobPosition;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    [Authorize]
    [Route("api/work/positions")]
    public class JobPositionController : DefinitionApiController
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        /// <summary>
        /// Get all job positions
        /// </summary>
        /// <param name="paginationFilter">Pagination configuration</param>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _jobPositionService.GetAsync(paginationFilter));
        
        /// <summary>
        /// Get specific job position by id
        /// </summary>
        /// <param name="id">Job position id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
            => Ok(await _jobPositionService.GetByIdAsync(id));

        /// <summary>
        /// Create new job position
        /// </summary>
        /// <param name="request">Job position info</param>
        /// <returns>Created job position info</returns>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.Manage)]
        public virtual async Task<JobPositionDto> CreateAsync([FromBody] JobPositionRequestModel request)
            => await _jobPositionService.CreateAsync(request);

        /// <summary>
        /// Update existing job position
        /// </summary>
        /// <param name="id">Job position id to be updaed</param>
        /// <param name="request">Job position info</param>
        /// <returns>Updated job position info</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Manage)]
        public virtual async Task<JobPositionDto> UpdateAsync(string id, [FromBody] JobPositionRequestModel request)
            => await _jobPositionService.UpdateAsync(id, request);

        /// <summary>
        /// Delete existing job position
        /// </summary>
        /// <param name="id">Job position id to be deleted</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Manage)]
        public virtual async Task DeleteAsync(string id)
            => await _jobPositionService.DeleteAsync(id);
    }
}