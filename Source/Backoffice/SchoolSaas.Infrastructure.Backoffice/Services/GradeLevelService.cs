using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using OtpNet;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class GradeLevelService : IGradeLevelService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;
        public GradeLevelService(IBackofficeContext dbContext, IBackofficeReadOnlyContext dbReadOnlyContext, IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }
        public async Task<bool> CreateLevel(GradeLevelDTO level, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var Level = MapToEntity(level);
                await _dbContext.GradeLevels.AddAsync(Level, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<bool> UpdateLevel(Guid Id, GradeLevelDTO level, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var Level = _dbReadOnlyContext.GradeLevels.FirstOrDefaultAsync(gl => gl.Id == Id, cancellationToken).Result
                ?? throw new KeyNotFoundException("Student not found");
                Level.TitleFr = level.TitleFr;
                Level.TitleEn = level.TitleEn;
                Level.TitleAr = level.TitleAr;
                Level.LastModified = DateTime.UtcNow;
                Level.EducationalStage = level.EducationalStage.Value;

                _dbContext.GradeLevels.Update(Level);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<bool> DeleteLevel(Guid Id, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var Level = _dbReadOnlyContext.GradeLevels.FirstOrDefaultAsync(gl => gl.Id == Id, cancellationToken).Result
                ?? throw new KeyNotFoundException("Student not found");
                _dbContext.GradeLevels.Remove(Level);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            });
        }
        public async Task<GradeLevelDTO> GetLevel(Guid Id, CancellationToken cancellationToken)
        {
            var level =  _dbReadOnlyContext.GradeLevels.FirstOrDefaultAsync(gl => gl.Id == Id, cancellationToken).Result
                ?? throw new KeyNotFoundException("Student not found");
            return MapToDTO(level);
        }
        public async Task<GradeLevelDTO> GetLevelByName(string name, CancellationToken cancellationToken)
        {
            var level = _dbReadOnlyContext.GradeLevels.FirstOrDefaultAsync(gl => gl.TitleFr == name || gl.TitleAr == name || gl.TitleEn == name, cancellationToken).Result
               ?? throw new KeyNotFoundException("Student not found");
            return MapToDTO(level);
        }
        public async Task<PagedResult<GradeLevelDTO>> GetLevelByEducationalStage(EducationalStageEnum EducationalStage,int page, int? size, CancellationToken cancellationToken)
        {
            var level = _dbReadOnlyContext.GradeLevels.Select(gl => gl).Where(gl => gl.EducationalStage == EducationalStage).ToList()
            ?? throw new KeyNotFoundException("Student not found");
            return MapToPagedDTO(level, page, size);
        }


        private PagedResult<GradeLevelDTO> MapToPagedDTO(List<GradeLevel> levels, int page, int? size)
        {
            List<GradeLevelDTO> list = new List<GradeLevelDTO>();

            foreach (var level in levels)
            {
                list.Add(new GradeLevelDTO
                {
                    TitleFr = level.TitleFr,
                    TitleAr = level.TitleAr,
                    TitleEn = level.TitleEn,
                    Description = level.Description,
                    EducationalStage = level.EducationalStage
                });
            }

            int pageSize = size ?? PagedResult<GradeLevelDTO>.DefaultPageSize;

            return new PagedResult<GradeLevelDTO>
            {
                Results = list,
                RowCount = list.Count,
                CurrentPage = page,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling(list.Count / (double)pageSize)
            };
        }

        private GradeLevelDTO MapToDTO(GradeLevel level)
        {
            return new GradeLevelDTO
            {
                TitleFr = level.TitleFr,
                TitleAr = level.TitleAr,
                TitleEn = level.TitleEn,
                Description = level.Description,
                EducationalStage = level.EducationalStage,

            };
        }
        private GradeLevel MapToEntity(GradeLevelDTO level)
        {
            return new GradeLevel
            {
                TitleFr = level.TitleFr,
                TitleAr = level.TitleAr,
                TitleEn = level.TitleEn,
                Description = level.Description,
                EducationalStage = level.EducationalStage.Value,

            };
        }
    }
}
