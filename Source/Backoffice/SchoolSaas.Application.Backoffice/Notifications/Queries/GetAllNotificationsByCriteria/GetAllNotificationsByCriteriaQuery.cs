using SchoolSaas.Application.Backoffice.Notifications.Commands.UpdateNotificationStatus;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSaas.Domain.Common.DataObjects;

namespace SchoolSaas.Application.Backoffice.Notifications.Queries.GetAllNotificationsByCriteria
{
    public class GetAllNotificationsByCriteriaQuery: IRequest<GetAllNotificationsByCriteriaResponseDTO>
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public GetAllNotificationsByCriteriaRequestDTO criteria { get;set;}

    }
    public class GetAllNotificationsByCriteriaQueryHandler(IBackofficeService backofficeService, ICurrentUserService currentUserService) : IRequestHandler<GetAllNotificationsByCriteriaQuery, GetAllNotificationsByCriteriaResponseDTO>
    {

        private readonly IBackofficeService _backofficeService = backofficeService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<GetAllNotificationsByCriteriaResponseDTO> Handle(GetAllNotificationsByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _backofficeService.GetAllNotificationsByCriteria(request.Page, request.Size, request.criteria);
        }

    }
}
