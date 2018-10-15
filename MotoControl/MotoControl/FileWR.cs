using System;
using System.Collections.Generic;
using System.IO;

namespace MotorControl
{
    internal class FileWR
    {
        public void WriteToFile(List<string> list, string txtFile)
        {
            FileStream fileStream = new FileStream(txtFile, FileMode.Create, FileAccess.Write,FileShare.Read);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.Flush();
            streamWriter.BaseStream.Seek(0L, SeekOrigin.Begin);
            for (int i = 0; i < list.Count; i++)
            {
                streamWriter.WriteLine(list[i].ToString());
            }
            streamWriter.WriteLine("END");
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
        }

        public List<string> ReadToList(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            List<string> list = new List<string>();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read,FileShare.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            streamReader.BaseStream.Seek(0L, SeekOrigin.Begin);
            while (true)
            {
                string text = streamReader.ReadLine();
                if (text == null | text == "END")
                {
                    break;
                }
                list.Add(text);
            }
            streamReader.Close();
            fileStream.Close();
            return list;
        }
    }
}
