using System;

namespace CleanArchitecture.Infrastructure.FileManager.Models
{
    public class FileEntity
    {
        private FileEntity()
        {

        }
        public FileEntity(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }

        internal void UpdateContent(byte[] content)
        {
            Content = content ?? Array.Empty<byte>();
        }
    }
}