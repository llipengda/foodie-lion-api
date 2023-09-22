using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Params;

public class UpdateNotificationParams
{
    public string? Title { get; set; }

    public string? Content { get; set; }

    public bool? IsVisiable { get; set; } = null;
}
