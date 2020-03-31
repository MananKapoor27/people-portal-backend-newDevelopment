using Microsoft.AspNetCore.Mvc;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Shared.Helpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PeoplePortalDomainLayer.Entities.DTO.SkillDto;
using PeoplePortalDomainLayer.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace PeoplePortal.Controllers
{
    /// <summary>
    /// This <c>SkillController</c> class handles all skill related operations
    /// </summary>
    /// <remark><para>Here we call adding, viewing and updating skills functions 
    /// </para></remark>

    [Route("api/skill")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        /// <summary>
        /// this constructor is uesd for services Dependency Injection 
        /// </summary>
        /// <param name="skillService">skill service object</param>
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        /// <summary>
        /// This method is used to create new Skills
        /// </summary>
        /// <param name="skill"> skill information</param>
        /// <returns>returns new skill with success status</returns>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Skill>> PublishSkillAsync([FromBody]AddSkillDto skill)
        {
            var result = await _skillService.AddSkillAsync(skill);
            return Created("", result);
        }
        /// <summary>
        /// This method is used to update Skills
        /// </summary>
        /// <param name="skill"> skill information</param>
        /// <returns>returns new skill with success status</returns>        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Skill>> UpdateSkillAsync([FromBody]Skill skill)
        {
            var result = await _skillService.UpdateSkillAsync(skill);
            return Ok(result);
        }
        /// <summary>
        /// This method is used to get all Skills
        /// </summary>
        /// <returns>returns all skill with success status</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Skill>> ShowSkillAsync()
        {
            var result = await _skillService.ShowSkillAsync();
            return Ok(result);
        }

        /// <summary>
        /// This method is used to create new Skills
        /// </summary>
        /// <param name="skillId"> skill information</param>
        /// <returns>returns new skill with success status</returns>        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Skill>> DeleteSkillAsync(int skillId)
        {
            await _skillService.DeleteSkillAsync(skillId);
            return Ok();
        }
    }
}