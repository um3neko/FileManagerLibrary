namespace FileManagerLibrary.Types
{
    public enum FileFormat
    {
        XML, 
        Binary,
    }
    
    public class FileBase
    {
        public string? FilePath { get; set; }
        public FileFormat FileFormat { get; set; }
        public virtual List<Record> Records { get; set; } = new();

    }
}
