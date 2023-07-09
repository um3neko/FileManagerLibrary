
using System.Xml.Serialization;

namespace FileManagerLibrary.Types.XML;

public class Car
{   
    public DateTime Date { get; set; } //dd/mm/yyyy
    public string BrandName { get; set; } // unicode string
    public int Price { get; set; } // integer
}
