using MicrosLabel.Application.Enumerations;

namespace MicrosLabel.Domain.Services
{
    public record Label(string Zpl, DocumentSize DocumentSize, DocumentFormat DocumentFormat);
}
