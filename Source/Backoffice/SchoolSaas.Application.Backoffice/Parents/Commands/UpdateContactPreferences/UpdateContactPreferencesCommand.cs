using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Backoffice.Parents.Commands.UpdateContactPreferences
{
    public class UpdateContactPreferencesCommand : IRequest<bool>
    {
        public Guid ParentId { get; set; }
        public ContactPreferencesDTO Preferences { get; set; }
    }
    public class UpdateContactPreferencesCommandHandler
    : IRequestHandler<UpdateContactPreferencesCommand, bool>
    {
        private readonly IParentService _service;
        public UpdateContactPreferencesCommandHandler(IParentService service) => _service = service;

        public Task<bool> Handle(UpdateContactPreferencesCommand req, CancellationToken ct) =>
            _service.UpdateContactPreferencesAsync(req.ParentId, req.Preferences, ct);
    }
}
