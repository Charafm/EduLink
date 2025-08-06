using SchoolSaas.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SchoolSaas.Web.Common.Controllers.Interfaces
{
    public interface IGetByIdAction<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        Task<ActionResult<TEntity>> GetById(TId id);
    }
}