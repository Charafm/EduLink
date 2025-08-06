using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.Entities
{
    public class PagedRequestBase
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
