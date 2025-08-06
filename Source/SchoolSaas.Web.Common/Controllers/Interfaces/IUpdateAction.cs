using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Web.Common.Controllers.Interfaces
{
    public interface IUpdateAction<TEntity, TDto, TId>
        where TEntity : BaseEntity<TId>
        where TDto : class
    {
        Task<ActionResult> Update(TId id, TDto dto);
    }
}