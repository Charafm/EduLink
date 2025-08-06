using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IGradeLevelService
    {
        Task<bool> CreateLevel(GradeLevelDTO level, CancellationToken cancellationToken);
        Task<bool> UpdateLevel(Guid Id, GradeLevelDTO level, CancellationToken cancellationToken);
        Task<bool> DeleteLevel(Guid Id, CancellationToken cancellationToken);
        Task<GradeLevelDTO> GetLevel(Guid Id, CancellationToken cancellationToken);
        Task<GradeLevelDTO> GetLevelByName(string name, CancellationToken cancellationToken);
        Task<PagedResult<GradeLevelDTO>> GetLevelByEducationalStage(EducationalStageEnum EducationalStage,int page, int? size,  CancellationToken cancellationToken);
       // Task<PagedResult<GradeLevelDTO>> SearchGradeLevel(GradeLevelDTO DTO, CancellationToken cancellationToken);
    }
}
