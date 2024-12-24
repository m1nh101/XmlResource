using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Xml.Linq;

namespace MultiLanguage.Localization;

public class XmlStringLocalizer : IStringLocalizer
{
    private readonly Dictionary<string, string> _dict;

    public XmlStringLocalizer(string filePath)
    {
        _dict = ReadXmlFile(filePath);
    }

    public LocalizedString this[string name]
    {
        get
        {
            if (_dict.TryGetValue(name, out var value))
            {
                return new LocalizedString(name, value, resourceNotFound: false);
            }

            // Return a fallback if not found in the dictionary
            return new LocalizedString(name, name, resourceNotFound: true);
        }
    }

    public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _dict.Select(e => new LocalizedString(e.Key, e.Value, false));

    private static Dictionary<string, string> ReadXmlFile(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream);
        var data = reader.ReadToEnd();
        var xml = XElement.Parse(data);
        var result = new Dictionary<string, string>();
        foreach(var element in xml.Descendants("text"))
        {
            var key = element.Attribute("name")?.Value ?? throw new NullReferenceException();
            var value = element.Attribute("value")?.Value ?? throw new NullReferenceException();

            result[key] = value;
        }
        
        return result;
    }
}
