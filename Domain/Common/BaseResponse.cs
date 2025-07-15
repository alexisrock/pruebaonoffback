using System.Diagnostics.CodeAnalysis;

namespace Domain.Common
{
    [ExcludeFromCodeCoverage]
    public class BaseResponse
    {
       public string message { get; set; } = string.Empty;
      
    }
}
