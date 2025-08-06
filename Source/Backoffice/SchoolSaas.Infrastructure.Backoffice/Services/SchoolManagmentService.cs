using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class SchoolManagmentService : ISchoolManagmentService
    {
        private readonly IBackofficeReadOnlyContext _dbContext;

        public SchoolManagmentService(IBackofficeReadOnlyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BranchDto>> getSchoolBranches(Guid SchoolId)
        {
            return  _dbContext.Branches.Where(b => b.SchoolId == SchoolId).Select(b => new BranchDto { 
            Id = b.Id,
            AddressFr = b.AddressFr,
            AddressAr = b.AddressAr,
            BranchNameAr = b.BranchNameAr,
            PrincipalNameAr = b.PrincipalNameAr,
            BranchNameFr = b.BranchNameFr,
            BranchNameEn = b.BranchNameEn,
            CityId = b.CityId,
            Phone= b.Phone,
            PrincipalNameFr = b.PrincipalNameFr,
            SchoolId = SchoolId,
            
            }).ToList();
        }
    }
}
