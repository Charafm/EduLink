using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Web.Common.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class BaseController<TEntity, TId> : ApiController
        where TEntity : IdBasedEntity<TId>, IDeletableEntity, IAuditableEntity<string>
    {

    }
}