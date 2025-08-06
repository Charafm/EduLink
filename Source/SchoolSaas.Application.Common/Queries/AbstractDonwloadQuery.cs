//using SchoolSaas.Domain.Backoffice.Entities;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Configuration;

namespace SchoolSaas.Application.Common.Queries
{
    public abstract class AbstractDownloadQuery<TContext> : IRequest<AbstractDocument?>
        where TContext : IReadOnlyContext
    {
        public Guid Id { get; set; }
        public DocumentTypeEnum Type { get; set; }
        public DocumentSpec Spec { get; set; } = DocumentSpec.None;
    }


    public abstract class AbstractDonwloadQueryHandler<TCommand, TContext> : IRequestHandler<TCommand, AbstractDocument?>
        where TCommand : AbstractDownloadQuery<TContext>
        where TContext : IReadOnlyContext
    {
        protected readonly TContext _context;
        protected readonly IStorage _storage;
        protected readonly IMediaService _mediaService;
        private readonly bool _IsnfsOptionsEnabled;
        public AbstractDonwloadQueryHandler(TContext context, IStorage storage, IMediaService mediaService, IOptions<NfsOptions> nfsOptions)
        {
            _context = context;
            _storage = storage;
            _mediaService = mediaService;
            _IsnfsOptionsEnabled = nfsOptions != null ? nfsOptions.Value.IsEnabled : false;
        }

        public abstract Task<AbstractDocument?> Handle(TCommand request, CancellationToken cancellationToken);

        protected async Task<AbstractDocument?> GetDocument<TEntity, TDocument>(AbstractDownloadQuery<TContext> request)
            where TEntity : BaseEntity<Guid>, IHasDocuments<TDocument>
            where TDocument : AbstractDocument
        {
            AbstractDocument? document;

            if (request.Spec == DocumentSpec.None)
            {
                document = await _context.Set<TDocument>()
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(e => e.Id == request.Id);
            }
            else
            {
                document = await _context.Set<TDocument>()
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(e => e.ParentId == request.Id && e.Type == request.Type && e.Spec == request.Spec);
            }

            if (document == null)
            {
                IHasDocuments<TDocument>? entity = await _context.Set<TEntity>()
                    .IgnoreQueryFilters()
                    .Where(e => e.Id == request.Id)
                    .Include(e => e.Documents.Where(ee => ee.Type == request.Type && ee.Spec == request.Spec))
                    .OrderByDescending(e => e.Created)
                    .FirstOrDefaultAsync();

                document = entity?.Documents?.OrderBy(e => e.Uri).FirstOrDefault(e => e.Type == request.Type);
            }

            if (document != null)
            {
                if (request.Spec != DocumentSpec.None)
                {
                    var thumbnail = await _context.Set<TDocument>()
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(e => e.ParentId == document.Id && e.Spec == request.Spec);
                    if (thumbnail != null)
                    {
                        document = thumbnail;
                    }
                }
            }
            if (_IsnfsOptionsEnabled)
            {
                if (document != null && document.Uri != null)
                {
                    try
                    {
                        Stream fileStream = await _storage.GetFileAsync(document.Uri);
                        if (fileStream != null)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                fileStream.CopyTo(memoryStream);
                                memoryStream.Position = 0;
                                document.Data = memoryStream.ToArray();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception();
                    }
                }
            }
            else
            {
                document.Data = document.Data;
            }
            return document;
        }


    }
}