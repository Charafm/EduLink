using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Commands.BulkUploadDocuments
{
    //public class BulkUploadDocumentsCommand : IRequest<bool>
    //{
    //    public BulkEnrollmentDocumentsDTO DTO { get; set; }
    //}

    //public class BulkUploadDocumentsCommandHandler : IRequestHandler<BulkUploadDocumentsCommand, bool>
    //{
    //    private readonly IEnrollmentService _service;

    //    public BulkUploadDocumentsCommandHandler(IEnrollmentService service)
    //    {
    //        _service = service;
    //    }

    //    public async Task<bool> Handle(BulkUploadDocumentsCommand request, CancellationToken cancellationToken)
    //    {
    //        return await _service.BulkUploadEnrollmentDocumentsAsync(request.DTO, cancellationToken);
    //    }
    //}


}