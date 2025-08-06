using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolSaas.Web.Common.Controllers.Interfaces
{
    public interface IGetAction<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        Task<ActionResult<PagedResult<TEntity>>> Get(int? page, int? size);
    }
}