using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Albian.Kernel.Compression
{
    /// <summary>
    /// 压缩类
    /// </summary>
    public class Compress
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="text">需要压缩的字符串</param>
        /// <remarks>如果压缩和解压缩不能对应，请注意编码问题，默认使用UTF-8编码</remarks>
        /// <returns></returns>
        public static string GZipCompress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            //var outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="compressedText">需要解压的字符串</param>
        /// <remarks>如果压缩和解压缩不能对应，请注意编码问题，默认使用UTF-8编码</remarks>
        /// <returns></returns>
        public static string GZipDecompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}