namespace photuris_backend.DTOs
{
    public class PicturesMetaDataDto
    {
        public string DateTimeCreatedString { get; set; }

        public DateTime DateTimeCreated =>
            Convert.ToDateTime(DateTimeCreatedString);

        public string FileName { get; set; }

        public ulong FileSize { get; set; }
    }
}
