using SchoolSaas.Application.Common.Specifications;
using SchoolSaas.Domain.Common.DataObjects.Notification;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Backoffice;
public class GetAllNotificationsByCriteriaSpecification : BaseSpecification<Notification>
{
    public GetAllNotificationsByCriteriaSpecification(GetAllNotificationsByCriteriaRequestDTO criteria)
    {
        if (!string.IsNullOrEmpty(criteria.UserId))
        {
            AddCriteria(n => n.UserId == criteria.UserId);
        }

        if (criteria.IsRead.HasValue)
        {
            AddCriteria(n => n.IsRead == criteria.IsRead);
        }

        if (criteria.IsSeen.HasValue)
        {
            AddCriteria(n => n.IsSeen == criteria.IsSeen);
        }
    }
}
