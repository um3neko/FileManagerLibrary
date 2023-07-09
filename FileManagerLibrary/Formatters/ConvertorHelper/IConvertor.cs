using FileManagerLibrary.Types;

namespace FileManagerLibrary.Formatters.ConvertorHelper
{
    public interface IConvertor
    {

        public FileBase LoadFile(string filePath);
        public void SaveFile(FileBase file, string filePath);
    }
}
