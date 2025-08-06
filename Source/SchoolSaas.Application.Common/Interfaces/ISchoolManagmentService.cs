using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ISchoolManagmentService
    {
        Task<List<BranchDto>> getSchoolBranches (Guid SchoolId);
    }
}
