using Reimbursement.Application.DTOs;
using Reimbursement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursement.Application.Mappers.ReimbursementMappers
{
    public static class ReimbursementMapper
    {
        public static ReimbursementDTO ToReimbursementDto(ReimbursementModel reimbursementModel)
        {
            return new ReimbursementDTO
            {
                Id = reimbursementModel.Id,
                CreatedDate = reimbursementModel.CreatedDate,
                CreatorId = reimbursementModel.CreatorId,
                Currency = reimbursementModel.Currency,
                RequestedValue = reimbursementModel.RequestedValue,
                ReimbursementType = reimbursementModel.ReimbursementType,
                RequestPhase = reimbursementModel.RequestPhase,
                ApprovedValue = reimbursementModel.ApprovedValue,
                ApprovedAmount = reimbursementModel.ApprovedAmount,
                ApprovedBy = reimbursementModel.ApprovedBy,
                InternalNotes = reimbursementModel.InternalNotes,
                ImagePath = reimbursementModel.ImagePath,
            };
        }

        public static ReimbursementModel ToReimbursementModel(ReimbursementDTO reimbursementDTO)
        {
            return new ReimbursementModel
            {
                Id = reimbursementDTO.Id,
                CreatedDate = reimbursementDTO.CreatedDate,
                CreatorId = reimbursementDTO.CreatorId,
                Currency = reimbursementDTO.Currency,
                RequestedValue = reimbursementDTO.RequestedValue,
                ReimbursementType = reimbursementDTO.ReimbursementType,
                RequestPhase = reimbursementDTO.RequestPhase,
                ApprovedValue = reimbursementDTO.ApprovedValue,
                ApprovedAmount = reimbursementDTO.ApprovedAmount,
                ApprovedBy = reimbursementDTO.ApprovedBy,
                InternalNotes = reimbursementDTO.InternalNotes,
                ImagePath = reimbursementDTO.ImagePath,
            };
        }

        public static ReimbursementModel ToEntityFromCreate(CreateReimbursementDTO createReimbursementDTO)
        {
            return new ReimbursementModel
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Currency = createReimbursementDTO.Currency,
                RequestedValue = createReimbursementDTO.RequestedValue,
                ReimbursementType = createReimbursementDTO.ReimbursementType,
                CreatorId = createReimbursementDTO.CreatorId,
                RequestPhase = createReimbursementDTO.RequestPhase,
                ImagePath = createReimbursementDTO.ImagePath
                
            };
        }

        public static ReimbursementModel ToEntityFromUpdate(UpdateReimbursementDTO updateReimbursementDTO)
        {
            return new ReimbursementModel
            {
                Id = updateReimbursementDTO.Id,
                CreatedDate = updateReimbursementDTO.CreatedDate,
                Currency = updateReimbursementDTO.Currency,
                RequestedValue = updateReimbursementDTO.RequestedValue,
                CreatorId = updateReimbursementDTO.CreatorId,
                ReimbursementType = updateReimbursementDTO.ReimbursementType,
                ImagePath = updateReimbursementDTO.ImagePath
            };
        }
    }
}
