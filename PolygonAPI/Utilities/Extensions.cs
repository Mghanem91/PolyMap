using PolygonAPI.Utilities.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace PolygonAPI.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Compress list of file entity objects into array of byte.
        /// </summary>
        /// <param name="files">list of files to compress.</param>
        /// <returns></returns>
        public static byte[] Compress(this ConcurrentBag<FileEntity> files)
        {
            if (files.Any())
            {
                var ms = new MemoryStream();
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (var file in files)
                    {
                        var entry = archive.Add(file);
                    }
                }
                ms.Position = 0; //reset memory stream position.
                return ms.ReadBytes();
            }
            return null;
        }

        private static ZipArchiveEntry Add(this ZipArchive archive, FileEntity file)
        {
            var entry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
            using (var stream = entry.Open())
            {
                stream.Write(file.FileStream);
            }
            return entry;
        }

        private static byte[] ReadBytes(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }

}

