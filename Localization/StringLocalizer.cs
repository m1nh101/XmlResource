using Microsoft.Extensions.Localization;
using System.Reflection;

namespace MultiLanguage.Localization;

public class StringLocalizer : IStringLocalizer
{
    private readonly IStringLocalizer _localizer;

    public StringLocalizer(IStringLocalizerFactory factory)
    {
        var type = typeof(StringLocalizer);
        _localizer = factory.Create(string.Empty, type.Assembly.FullName!);
    }

    public LocalizedString this[string name] => _localizer[name];

    public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);
}
