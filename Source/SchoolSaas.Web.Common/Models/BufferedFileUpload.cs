using SchoolSaas.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SchoolSaas.Web.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BufferedFileUpload
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid AssociatedId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public DocumentTypeEnum Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DocumentSpec Spec { get; set; } = DocumentSpec.None;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public IFormFile File { get; set; }
        public string? DocumentCode { get; set; }
    }
}
