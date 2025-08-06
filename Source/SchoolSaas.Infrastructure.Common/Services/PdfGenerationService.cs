using SchoolSaas.Application.Common.Interfaces;
using Wkhtmltopdf.NetCore;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class PdfGenerationService : IPdfGenerationService
    {
        private readonly ITemplateToStringRenderer _templateToStringRenderer;
        private readonly IGeneratePdf _generatePdf;

        public PdfGenerationService(ITemplateToStringRenderer templateToStringRenderer, IGeneratePdf generatePdf)
        {
            _templateToStringRenderer = templateToStringRenderer;
            _generatePdf = generatePdf;
        }

        public async Task<byte[]> GeneratePdfAsync<TModel>(string template, TModel model, bool isLandscape = false) where TModel : class
        {
            try
            {
                var html = await _templateToStringRenderer.RenderTemplateToStringAsync($"/Areas/Reports/Views/Templates/{template}.cshtml",
                new PdfViewModel<TModel>()
                {
                    Data = model,
                });

                var workStream = new MemoryStream();

                if (isLandscape)
                {
                    _generatePdf.SetConvertOptions(new ConvertOptions()
                    {
                        PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Landscape,
                        PageMargins = new Wkhtmltopdf.NetCore.Options.Margins(4, 4, 4, 4)
                    });
                }
                var pdf = _generatePdf.GetPDF(html);
                workStream.Write(pdf, 0, pdf.Length);
                workStream.Position = 0;


                return workStream.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
