
using System;
using System.IO;
using System.Text;
using System.Reflection;

namespace Common
{
    class CommonFile
    {
        public static string Read(string path)
        {
            Console.WriteLine($"filepath:{path}");
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("utf-8")))
            {
                var data = sr.ReadToEnd();
                return data;
            }
        }

        public static void Write(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(content);
            }
        }


        public static void WriteAdd(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(content);
            }
        }

        public static string Load(string path)
        {
            Console.WriteLine($"filepath:{path}");
            using (StreamReader sr = new StreamReader(path))
            {
                var data = sr.ReadToEnd();
                return data;
            }
        }

        public static void Make(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(content);
            }
        }

        public static void Create(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(content);
            }
        }

        public static string CreateAbsPath(string path)
        {
            if (System.IO.Path.IsPathRooted(path))
            {
                return path;
            }
            Assembly? myAssembly = Assembly.GetEntryAssembly();
            var dirName = System.IO.Path.GetDirectoryName(myAssembly?.Location);
            var filePath = $"{dirName}/{path}";
            return filePath;
        }

        public static bool ExistsPath(string path)
        {
            return File.Exists(path);
        }

        public static bool ExistsDirecoryPath(string path)
        {
            return Directory.Exists(path);
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static string GetCwd()
        {
            Assembly? myAssembly = Assembly.GetEntryAssembly();
            return System.IO.Path.GetDirectoryName(myAssembly?.Location ?? "") ?? "";
        }

    }
}