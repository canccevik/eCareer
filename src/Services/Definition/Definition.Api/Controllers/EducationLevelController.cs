﻿using System.Threading.Tasks;
using Career.Data.Pagination;
using Definition.Api.Constants;
using Definition.Api.Controllers.Base;
using Definition.Application.Education.EducationLevel;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    [Authorize]
    [Route("api/education/levels")]
    public class EducationLevelController : DefinitionApiController
    {
        private readonly IEducationLevelService _educationLevelService;

        public EducationLevelController(IEducationLevelService educationLevelService)
        {
            _educationLevelService = educationLevelService;
        }
        
        /// <summary>
        /// Get all education levels
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _educationLevelService.GetAsync(paginationFilter));
        
        /// <summary>
        /// Get specific education level by id
        /// </summary>
        /// <param name="id">Education level id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
            => Ok(await _educationLevelService.GetByIdAsync(id));
        
        /// <summary>
        /// Create new education level
        /// </summary>
        /// <param name="request">Education level info</param>
        /// <returns>Created education level info</returns>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.Manage)]
        public virtual async Task<EducationLevelDto> CreateAsync([FromBody] EducationLevelRequestModel request)
            => await _educationLevelService.CreateAsync(request);
        
        /// <summary>
        /// Update existing education level
        /// </summary>
        /// <param name="id">Education level id to be updaed</param>
        /// <param name="request">Education level info</param>
        /// <returns>Updated education level info</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Manage)]
        public virtual async Task<EducationLevelDto> UpdateAsync(string id, [FromBody] EducationLevelRequestModel request)
            => await _educationLevelService.UpdateAsync(id, request);
        
        /// <summary>
        /// Delete existing education level
        /// </summary>
        /// <param name="id">Education level id to be deleted</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.Manage)]
        public virtual async Task DeleteAsync(string id)
            => await _educationLevelService.DeleteAsync(id);
    }
}