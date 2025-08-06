using System.ComponentModel.DataAnnotations;

namespace SchoolSaas.Web.Identity.ViewModels.Shared;

public class ErrorViewModel2
{
    [Display(Name = "Error")]
    public string Error { get; set; }

    [Display(Name = "Description")]
    public string ErrorDescription { get; set; }
}
