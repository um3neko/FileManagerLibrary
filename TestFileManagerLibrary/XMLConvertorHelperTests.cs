using FileManagerLibrary.Formatters.ConvertorHelper;
using FileManagerLibrary.Types.XML;
using FileManagerLibrary.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

[TestFixture]
public class XMLConvertorHelperTests
{
    private XMLConvertorHelper convertor;
    private string testFilePath;

    [SetUp]
    public void Setup()
    {
        convertor = new XMLConvertorHelper();
        testFilePath = "testfile.xml";
    }

    [TearDown]
    public void Cleanup()
    {
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }

    [Test]
    public void LoadFile_ValidFile_ReturnsFileBase()
    {
        // Arrange
        FileBase expectedFile = CreateSampleFile();
        SaveSampleFile(expectedFile, testFilePath);

        // Act
        FileBase actualFile = convertor.LoadFile(testFilePath);

        // Assert
        Assert.AreEqual(expectedFile.Records.Count, actualFile.Records.Count);
        for (int i = 0; i < expectedFile.Records.Count; i++)
        {
            Assert.AreEqual(expectedFile.Records[i].BrandName, actualFile.Records[i].BrandName);
            Assert.AreEqual(expectedFile.Records[i].Date, actualFile.Records[i].Date);
            Assert.AreEqual(expectedFile.Records[i].Price, actualFile.Records[i].Price);
        }
    }

    [Test]
    public void SaveFile_ValidFile_SavesCorrectly()
    {
        // Arrange
        FileBase expectedFile = CreateSampleFile();

        // Act
        convertor.SaveFile(expectedFile, testFilePath);

        // Assert
        FileBase actualFile = convertor.LoadFile(testFilePath);
        Assert.AreEqual(expectedFile.Records.Count, actualFile.Records.Count);
        for (int i = 0; i < expectedFile.Records.Count; i++)
        {
            Assert.AreEqual(expectedFile.Records[i].BrandName, actualFile.Records[i].BrandName);
            Assert.AreEqual(expectedFile.Records[i].Date, actualFile.Records[i].Date);
            Assert.AreEqual(expectedFile.Records[i].Price, actualFile.Records[i].Price);
        }
    }

    private FileBase CreateSampleFile()
    {
        FileBase file = new FileBase();
        file.Records = new List<Record>
        {
            new Record
            {
                Date = new DateTime(2023, 7, 1),
                BrandName = "Brand A",
                Price = 100
            },
            new Record
            {
                Date = new DateTime(2023, 7, 2),
                BrandName = "Brand B",
                Price = 200
            }
        };
        return file;
    }

    private void SaveSampleFile(FileBase file, string filePath)
    {
        XMLFile xmlFile = new XMLFile
        {
            Cars = file.Records.Select(r => new Car
            {
                BrandName = r.BrandName,
                Date = r.Date,
                Price = r.Price,
            }).ToList()
        };

        XmlSerializer serializer = new XmlSerializer(typeof(XMLFile));
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(fileStream, xmlFile);
        }
    }
}