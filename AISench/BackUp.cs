using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AISench
{
    internal class BackUp
    {
        public static void MakeBackUp()
        {
            FileStream input = new FileStream("D:\\АИС.accdb", FileMode.Open);
            FileStream output = new FileStream($"АИС({DateTime.UtcNow.ToString().Replace(':', '-').Replace('.', '-')}).gz", FileMode.Create);
            GZipStream stream = new GZipStream(output, CompressionMode.Compress);
            input.CopyTo(stream);
            input.Close();
            input.Dispose();
            stream.Close();
            stream.Dispose();
            output.Close();
            output.Dispose();
        }
        public static void LoadBackUp(string name)
        {
            string back_up_file = name;
            FileStream input = new FileStream(back_up_file, FileMode.Open);
            FileStream output = new FileStream($"АИС.accdb", FileMode.Create);
            GZipStream stream = new GZipStream(input, CompressionMode.Decompress);
            stream.CopyTo(output);
            input.Close();
            input.Dispose();
            stream.Close();
            stream.Dispose();
            output.Close();
            output.Dispose();
        }
        public static void ClearOldBackUp()
        {
            string[] getter = Directory.GetFiles(Application.StartupPath,"*.gz");
            foreach(string name in getter)
            {
                if ((DateTime.Now - new FileInfo(name).LastWriteTime).TotalMinutes > 1.0)
                    File.Delete(name);
            }
        }
    }
}
