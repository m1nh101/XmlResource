using Microsoft.Extensions.Localization;
using System.Globalization;

namespace MultiLanguage.Localization;

public class XmlStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly IWebHostEnvironment _env;

    public XmlStringLocalizerFactory(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        var path = Path.Combine(_env.WebRootPath, "Resources", $"{CultureInfo.CurrentCulture.Name}.xml");
        return new XmlStringLocalizer(path);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        var path = Path.Combine(_env.ContentRootPath, "Resources", $"{CultureInfo.CurrentCulture.Name}.xml");
        return new XmlStringLocalizer(path);
    }
}
