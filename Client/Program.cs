

using FileManagerLibrary.Formatters;
using FileManagerLibrary.Types;

var fileFormatter = new FileFormatter();

#region Create empty File and show it's records 
fileFormatter.CreateFile();
for (int i = 0; i < 5; i++)
{
    var record = new Record
    {
        Date = DateTime.UtcNow,
        BrandName = $"Brand {i}",
        Price = 100 * i
    };
    fileFormatter.AddRecord(record);
}

var items = fileFormatter.GetRecords();
foreach (var item in items)
{
    Console.WriteLine($"{item.BrandName} \t\t {item.Date} \t\t {item.Price}");
}
#endregion

#region Save file in two formats 
fileFormatter.SaveFile(FileFormat.XML, "xml1.xml");
fileFormatter.SaveFile(FileFormat.Binary, "bin1.bin");
#endregion

#region Show records readed from two files
Console.WriteLine("---Items from xml file---");
fileFormatter.LoadFile("xml1.xml");
var itemsXML = fileFormatter.GetRecords();
foreach (var item in itemsXML)
{
    Console.WriteLine($"{item.BrandName} \t\t {item.Date} \t\t {item.Price}");
}

Console.WriteLine("---Items from binary file---");

fileFormatter.LoadFile("bin1.bin");
var itemsBIN = fileFormatter.GetRecords();
foreach (var item in itemsBIN)
{
    Console.WriteLine($"{item.BrandName} \t\t {item.Date} \t\t {item.Price}");
}

#endregion

#region Edit file and save

Console.WriteLine("---Edit file, save and read---");
fileFormatter.LoadFile("xml1.xml");
fileFormatter.EditRecord(1, new Record { BrandName = "Edited Brand" , Date = DateTime.UtcNow, Price = 123 });
var records = fileFormatter.GetRecords();
Console.WriteLine($"{records[1].BrandName} \t\t {records[1].Date} \t\t {records[1].Price}");
Console.WriteLine("---Read from edited file---");
fileFormatter.SaveFile(FileFormat.XML, "editedXML.xml");
fileFormatter.LoadFile("editedXML.xml");

var items4 = fileFormatter.GetRecords();
foreach (var item in items4)
{
    Console.WriteLine($"{item.BrandName} \t\t {item.Date} \t\t {item.Price}");
}

#endregion

Console.ReadKey();