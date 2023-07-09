
using System.Xml.Serialization;

namespace FileManagerLibrary.Types.XML;

[Serializable]
[XmlRoot("Document")]
public class XMLFile
{
    public List<Car> Cars { get; set; } = new();
}