using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static MemoryStream GZipCompress(Stream inputStream)
        {
            MemoryStream stream = new MemoryStream();
            using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true))
            {
                int BufferSize = 0x1000;
                int num;
                byte[] buffer = new byte[BufferSize];
                while ((num = inputStream.Read(buffer, 0, BufferSize)) > 0)
                {
                    stream2.Write(buffer, 0, num);
                }
            }
            stream.Seek(0L, SeekOrigin.Begin);
            return stream;
        }

        public static MemoryStream GZipDecompress(Stream inputStream)
        {
            MemoryStream stream = new MemoryStream();
            using (GZipStream stream2 = new GZipStream(inputStream, CompressionMode.Decompress, true))
            {
                int num;
                int BufferSize = 0x1000;
                byte[] buffer = new byte[BufferSize];
                while ((num = stream2.Read(buffer, 0, BufferSize)) > 0)
                {
                    stream.Write(buffer, 0, num);
                }
            }
            stream.Seek(0L, SeekOrigin.Begin);
            return stream;
        }

        public static byte[] GetData(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, obj);
                stream.Position = 0L;
                MemoryStream stream2 = GZipCompress(stream);
                byte[] buffer = stream2.ToArray();
                stream2.Close();
                return buffer;
            }
        }

        public static object GetObject(byte[] data)
        {
            if ((data == null) || (data.Length == 0))
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream(data))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream serializationStream = GZipDecompress(stream);
                object obj2 = binaryFormatter.Deserialize(serializationStream);
                serializationStream.Close();
                return obj2;
            }
        }
    }
}