using SchoolSaas.Domain.Common.Constants;

using System.Text.Json.Serialization;

namespace SchoolSaas.Domain.Common.Entities
{
    public abstract class BaseEntityTranslation<TEntity, TId> : BaseEntity<TId>,
        IEntityTranslation<TEntity>
    {
        [JsonIgnore]
        public virtual TEntity? Parent { get; set; }
        public virtual TId? ParentId { get; set; }

        public string LanguageCode { get; set; } = CoreConstants.DefaultLanguageCode;
    }
}