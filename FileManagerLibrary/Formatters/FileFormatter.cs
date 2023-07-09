using FileManagerLibrary.Formatters.ConvertorHelper;
using FileManagerLibrary.Types;
using System.Diagnostics.Metrics;

namespace FileManagerLibrary.Formatters;

public class FileFormatter
{
    private FileBase _file;
    private IConvertor _convertor;

    public void CreateFile()
    {
        _file = new FileBase();
    }

    public void EditRecord(int recordIndex, Record record)
    {
        var oldRecord = _file.Records[recordIndex] ?? throw new NullReferenceException();
        oldRecord.Price = record.Price;
        oldRecord.BrandName = record.BrandName;
        oldRecord.Date = DateTime.Now;
    }

    public void AddRecord(Record record)
    {
        _file.Records.Add(record);
    }
    public void DeleteRecord(int recordIndex)
    {
        var record = _file.Records[recordIndex] ?? throw new NullReferenceException();
        _file.Records.Remove(record);
    }

    public List<Record> GetRecords()
    {
        return _file.Records;
    }

    public void LoadFile(string filePath)
    {
        if(!File.Exists(filePath))
        {
            throw new FileNotFoundException("File is not exists");
        }
        string fileExtension = Path.GetExtension(filePath);
        switch(fileExtension)
        {
            case ".xml":
                _convertor = new XMLConvertorHelper();
                break;
            case ".bin":
                _convertor = new BinaryConvertorHelper(); 
                break;
        }
        _file = _convertor.LoadFile(filePath);
    }

    public void SaveFile(FileFormat fileFormat,string filePath)
    {
        if(_file == null)
        {
            throw new NullReferenceException("File is not created");
        }
        switch (fileFormat)
        {
            case FileFormat.Binary:
                if (_file.Records.Count <= 0)
                {
                    throw new ArgumentNullException("Cannot save an empty file to a binary file");
                }
                _convertor = new BinaryConvertorHelper();
                break;
            case FileFormat.XML:
                _convertor = new XMLConvertorHelper();
                break;
        }
        _convertor.SaveFile(_file, filePath);
    }
}
