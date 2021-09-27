using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThreadingDemos
{
    public class FileSearcher
    {
        private static readonly List<string> Files = new List<string>();

        private static bool IsSlnFileInDir(string directory)
        {
            var files = Directory.GetFiles(directory);
            return files.Any(file => Path.GetExtension(file) == ".sln");
        }

        public static string FindSlnPathInProject(string directoryInProject)
        {
            try
            {
                while (!IsSlnFileInDir(directoryInProject))
                {
                    directoryInProject = Path.GetDirectoryName(directoryInProject);
                    if (directoryInProject == null)
                        return null;
                }

                return directoryInProject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        

        public static string FindFileInProject(string fileName)
        {
            var slnPath = FindSlnPathInProject(directoryInProject: Directory.GetCurrentDirectory());
            return FindFileInSubDirs("WordsStockRus.txt", slnPath);
        }
        public static string FindFileInSubDirs(string fileName, string directory)
        {
            var files = Directory.GetFiles(directory);
            var dirs = Directory.GetDirectories(directory);
            string result = null;
            
            foreach (var file in files)
            {
                if (Path.GetFileName(file) == fileName)
                {
                    return file;
                }
            }

            foreach (var dir in dirs)
            {
                 result = FindFileInSubDirs(fileName, dir);
            }

            return result;
        }
        public static List<string> FindFilesInDirectory(string fileName, string directory)
        {
            var files = Directory.GetFiles(directory);
            var dirs = Directory.GetDirectories(directory);
            foreach (var file in files)
            {
                if (Path.GetFileName(file) == fileName)
                {
                    Files.Add(file);
                }
            }

            foreach (var dir in dirs)
            {
                var result = FindFileInSubDirs(fileName, dir);
                if (result != null)
                {
                    Files.Add(result);
                }
            }

            return Files;
        }

        public FileSearcher(string fileName = "WordsStockRus.txt")
        {
        }
    }
}