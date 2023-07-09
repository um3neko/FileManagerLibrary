using FileManagerLibrary.Types;
using FileManagerLibrary.Types.XML;
using System.Xml.Serialization;

namespace FileManagerLibrary.Formatters.ConvertorHelper
{
    public class XMLConvertorHelper : IConvertor
    {
        public FileBase LoadFile(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLFile));
            XMLFile xmlFile;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                xmlFile = (XMLFile)serializer.Deserialize(fileStream);
            }

            FileBase fileBase = new FileBase
            {
                FileFormat = FileFormat.XML,
                FilePath = filePath,
                Records = xmlFile.Cars.Select(r => new Record
                {
                    BrandName = r.BrandName,
                    Date = r.Date,
                    Price = r.Price,
                }).ToList(),
            };
            return fileBase;
        }

        public void SaveFile(FileBase file, string filePath)
        {
            XMLFile xmlFile = new XMLFile()
            {
                Cars = file.Records.Select(r => new Car
                {
                    BrandName = r.BrandName,
                    Date = r.Date,
                    Price = r.Price,

                }).ToList(),
            };
            XmlSerializer serializer = new XmlSerializer(xmlFile.GetType());
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(fileStream, xmlFile);
        }
    }
}
