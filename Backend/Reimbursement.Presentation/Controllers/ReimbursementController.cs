using Microsoft.AspNetCore.Mvc;
using Reimbursement.Application.DTOs;
using Reimbursement.Application.Mappers.ReimbursementMappers;
using Reimbursement.Application.ServiceInterface;
using Reimbursement.Domain.Entities;
using Reimbursement.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reimbursement.Presentation.Controllers
{
    [ApiController]
    [Route("api/reimbursement")]
    public class ReimbursementController : ControllerBase
    {
        private readonly IReimbursementService _reimbursementService;
        private readonly IUnitOfWork _unitOfWork;

        public ReimbursementController(IReimbursementService reimbursementService,IUnitOfWork unitOfWork)
        {
            _reimbursementService = reimbursementService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetallReimbursement()
        {
            try
            {
                var reimbursements = await _reimbursementService.GetAllReimbursements();
                if (reimbursements == null || !reimbursements.Any())
                {
                    return NotFound(new { message = "No reimbursements found." });
                }
                var reimbursementDto = reimbursements.Select(ReimbursementMapper.ToReimbursementDto).ToList();
                return Ok(reimbursementDto);

            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message });

            }

        }

        [HttpPost]
        [Route("getPending")]
        public async Task<IActionResult> GetAllPending([FromBody] Filter filter)
        {
            try
            {
                List<ReimbursementModel> reimbursements = await _reimbursementService.GetAllPending(filter.mailId, filter.type);
                if (reimbursements == null )
                {
                    return NotFound(new { message = "No pending reimbursements found." });
                }
                List<ReimbursementDTO> reimbursementDto = reimbursements.Select(ReimbursementMapper.ToReimbursementDto).ToList();
                return Ok(reimbursementDto);

            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message });

            }

        }

        [HttpPost]
        [Route("getApproved")]
        public async Task<IActionResult> GetAllApproved([FromBody] Filter filter)
        {
            try
            {
                List<ReimbursementModel> reimbursements = await _reimbursementService.GetAllApproved(filter.mailId, filter.type);
                if (reimbursements == null )
                {
                    return NotFound(new { message = "No approved reimbursements found." });
                }
                List<ReimbursementDTO> reimbursementDto = reimbursements.Select(ReimbursementMapper.ToReimbursementDto).ToList();
                return Ok(reimbursementDto);

            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message });

            }

        }

        [HttpPost]
        [Route("getDeclined")]
        public async Task<IActionResult> GetAllDeclined([FromBody] Filter filter)
        {
            try
            {
                List<ReimbursementModel> reimbursements= await _reimbursementService.GetAllDeclined(filter.mailId, filter.type);
                if (reimbursements == null )
                {
                    return NotFound(new { message = "No declined reimbursements found." });
                }
                List<ReimbursementDTO> reimbursementDto = reimbursements.Select(ReimbursementMapper.ToReimbursementDto).ToList();
                return Ok(reimbursementDto);

            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message });

            }

        }

        [HttpGet]
        [Route("getMyReimbursement/{id:Guid}")]
        public async Task<IActionResult> GetMyReimbursements(string id)
        {
            try
            {
                List<ReimbursementModel> reimbursements = await _reimbursementService.MyReimbursements(id);

                if (reimbursements == null)
                {
                    return NotFound(new { message = "No reimbursements found." });
                }
                List<ReimbursementDTO> reimbursementDto = reimbursements.Select(ReimbursementMapper.ToReimbursementDto).ToList();
                return Ok(reimbursementDto);

            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message });

            }

        }

        [HttpGet]
        [Route("getById/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            try
            {
                var reimbursement = await _reimbursementService.GetId(id);

                if (reimbursement == null)
                {
                    return NotFound();
                }

                return Ok(ReimbursementMapper.ToReimbursementDto(reimbursement));

            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message });

            }

        }

        [HttpPost]
        [Route("addreimbursement")]
        public async Task<IActionResult> AddReimbursement([FromBody] CreateReimbursementDTO createReimbursementDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var reimbursementModel = ReimbursementMapper.ToEntityFromCreate(createReimbursementDTO);
                bool data = await _reimbursementService.Insert(reimbursementModel);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetById), new { id = reimbursementModel.Id }, ReimbursementMapper.ToReimbursementDto(reimbursementModel));
                
            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message });

            }

        }

        [HttpPut]
        [Route("editreimbursement/{id:Guid}")]
        public async Task<IActionResult> EditReimbursement(Guid id, [FromBody] UpdateReimbursementDTO updateReimbursementDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var reimbursementModel = ReimbursementMapper.ToEntityFromUpdate(updateReimbursementDTO);
                var data = await _reimbursementService.Update(reimbursementModel);
                await _unitOfWork.CompleteAsync();
                if (data == false)
                {
                    return NotFound();
                }

                return Ok(ReimbursementMapper.ToReimbursementDto(reimbursementModel));
                
            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message});

            }

        }

        [HttpDelete]
        [Route("delete/{id:Guid}")]
        public async Task<IActionResult> DeleteReimbursement(Guid id)
        {
            try
            {
                ReimbursementModel reimbursement = await _reimbursementService.GetId(id);
                bool data = await _reimbursementService.Remove(reimbursement);
                if (data) return Ok(data);
                else return NotFound();

            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message});

            }

        }
    }


}
