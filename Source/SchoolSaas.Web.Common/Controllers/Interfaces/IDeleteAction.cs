using Microsoft.AspNetCore.Mvc;

namespace SchoolSaas.Web.Common.Controllers.Interfaces
{
    public interface IDeleteAction<TId>
    {
        Task<ActionResult> Delete(TId id);
    }
}