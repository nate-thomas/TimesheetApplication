using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TimeSheetApplication.ViewModels
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
