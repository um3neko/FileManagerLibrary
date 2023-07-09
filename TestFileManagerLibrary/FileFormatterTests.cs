using FileManagerLibrary.Formatters.ConvertorHelper;
using FileManagerLibrary.Formatters;
using FileManagerLibrary.Types;

[TestFixture]
public class FileFormatterTests
{
    private FileFormatter formatter;
    private string testFilePath;

    [SetUp]
    public void Setup()
    {
        formatter = new FileFormatter();
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
    public void CreateFile_InitializesFileBase()
    {
        // Act
        formatter.CreateFile();

        // Assert
        Assert.NotNull(formatter.GetRecords());
        Assert.IsEmpty(formatter.GetRecords());
    }

    [Test]
    public void AddRecord_AddsRecordToCollection()
    {
        // Arrange
        formatter.CreateFile();
        Record record = new Record
        {
            Date = DateTime.Now,
            BrandName = "Brand A",
            Price = 100
        };

        // Act
        formatter.AddRecord(record);

        // Assert
        List<Record> records = formatter.GetRecords();
        Assert.AreEqual(1, records.Count);
        Assert.AreEqual(record.Date, records[0].Date);
        Assert.AreEqual(record.BrandName, records[0].BrandName);
        Assert.AreEqual(record.Price, records[0].Price);
    }

    [Test]
    public void DeleteRecord_ValidIndex_RemovesRecordFromCollection()
    {
        // Arrange
        formatter.CreateFile();
        formatter.AddRecord(new Record
        {
            Date = DateTime.Now,
            BrandName = "qwerass",
            Price = 100
        });
        int recordIndex = 0;

        // Act
        formatter.DeleteRecord(recordIndex);

        // Assert
        List<Record> records = formatter.GetRecords();
        Assert.IsEmpty(records);
    }

    [Test]
    public void LoadFile_InvalidFileExtension_ThrowsException()
    {
        // Arrange
        string invalidFilePath = "testfile.invalid";
        File.WriteAllText(invalidFilePath, "Invalid file content");

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => formatter.LoadFile(invalidFilePath));
    }

    [Test]
    public void SaveFile_SavesFileCorrectly()
    {
        // Arrange
        FileBase expectedFile = CreateSampleFile();

        // Act
        formatter.CreateFile();
        foreach (Record record in expectedFile.Records)
        {
            formatter.AddRecord(record);
        }
        formatter.SaveFile(FileFormat.XML, testFilePath);

        // Assert
        FileBase actualFile = LoadSampleFile(testFilePath);
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
                Date = DateTime.Now,
                BrandName = "Brand A",
                Price = 100
            },
            new Record
            {
                Date = DateTime.Now,
                BrandName = "Brand B",
                Price = 200
            }
        };
        return file;
    }

    private FileBase LoadSampleFile(string filePath)
    {
        XMLConvertorHelper convertor = new XMLConvertorHelper();
        return convertor.LoadFile(filePath);
    }
}