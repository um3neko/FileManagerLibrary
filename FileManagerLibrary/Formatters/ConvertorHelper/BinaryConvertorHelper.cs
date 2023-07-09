using FileManagerLibrary.Types;
using System.Text;

namespace FileManagerLibrary.Formatters.ConvertorHelper;

public class BinaryConvertorHelper : IConvertor
{
    public FileBase LoadFile(string filePath)
    {
        FileBase file = new FileBase();

        using (FileStream fileStream = new(filePath, FileMode.Open))
        using (BinaryReader reader = new(fileStream))
        {
            ushort header = reader.ReadUInt16();
            if (header != 0x2526)
            {
                throw new FormatException("Invalid file header.");
            }

            int recordsCount = reader.ReadInt32();
            file.Records = new List<Record>();
            for (int i = 0; i < recordsCount; i++)
            {
                Record record = new Record();

                int day = reader.ReadInt32();
                int month = reader.ReadInt32();
                int year = reader.ReadInt32();
                record.Date = new DateTime(year, month, day);

                ushort brandNameLength = reader.ReadUInt16();
                byte[] brandNameBytes = reader.ReadBytes(brandNameLength);
                record.BrandName = Encoding.Unicode.GetString(brandNameBytes);

                record.Price = reader.ReadInt32();

                file.Records.Add(record);
            }
        }
        return file;
    }

    public void SaveFile(FileBase file, string filePath)
    {
        using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        using BinaryWriter writer = new(fileStream);
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
