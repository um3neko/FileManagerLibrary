using FileManagerLibrary.Formatters.ConvertorHelper;
using FileManagerLibrary.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

[TestFixture]
public class BinaryConvertorHelperTests
{
    private BinaryConvertorHelper convertor;
    private string testFilePath;

    [SetUp]
    public void Setup()
    {
        convertor = new BinaryConvertorHelper();
        testFilePath = "testfile.bin";
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
            Assert.AreEqual(expectedFile.Records[i].Date, actualFile.Records[i].Date);
            Assert.AreEqual(expectedFile.Records[i].BrandName, actualFile.Records[i].BrandName);
            Assert.AreEqual(expectedFile.Records[i].Price, actualFile.Records[i].Price);
        }
    }

    [Test]
    public void LoadFile_InvalidFileHeader_ThrowsFormatException()
    {
        // Arrange
        FileBase expectedFile = CreateSampleFile();
        SaveSampleFile(expectedFile, testFilePath);

        using (FileStream fileStream = new FileStream(testFilePath, FileMode.Open))
        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
            writer.Write((ushort)0x1234); // Invalid header
        }

        // Act & Assert
        Assert.Throws<FormatException>(() => convertor.LoadFile(testFilePath));
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
            Assert.AreEqual(expectedFile.Records[i].Date, actualFile.Records[i].Date);
            Assert.AreEqual(expectedFile.Records[i].BrandName, actualFile.Records[i].BrandName);
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
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
            writer.Write((ushort)0x2526);
            writer.Write(file.Records.Count);

            foreach (Record record in file.Records)
            {
                writer.Write(record.Date.Day);
                writer.Write(record.Date.Month);
                writer.Write(record.Date.Year);

                byte[] brandNameBytes = Encoding.Unicode.GetBytes(record.BrandName);
                writer.Write((ushort)brandNameBytes.Length);
                writer.Write(brandNameBytes);
                writer.Write(record.Price);
            }
        }
    }
}