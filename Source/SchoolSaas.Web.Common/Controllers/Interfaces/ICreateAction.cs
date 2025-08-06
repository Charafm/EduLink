using SchoolSaas.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolSaas.Web.Common.Controllers.Interfaces
{
    public interface ICreateAction<TEntity, TDto, TId>
        where TEntity : BaseEntity<TId>
        where TDto : class
    {
        Task<ActionResult<TEntity>> Create(TDto dto);
    }
}