using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolygonAPI.Utilities.Entities
{
    public class FileEntity
    {
        public string FileName { get; set; }
        public byte[] FileStream { get; set; }
        public string FileType { get; set; }
    }
}
