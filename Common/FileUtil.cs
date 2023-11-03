namespace Common
{
    public class FileUtil
    {
        private LogUtil logUtil = new LogUtil();

        public Boolean checkFileSize(string filePath)
        {
            long length = new FileInfo(filePath).Length;
            if (length <= 13000)
            {
                File.Delete(filePath);
                return false;
            }
            return true;
        }

        public void WalkDirectoryTree(System.IO.DirectoryInfo root, Action<string> executeFile)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (Exception ex)
            {
                logUtil.WriteLog(ex);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    Console.WriteLine(fi.FullName);
                    executeFile(fi.FullName);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, executeFile);
                }
            }
        }

        public string getReplaceFileName(FileInfo file)
        {
            string fileName = ReplaceLastOccurrence(file.Name, file.Extension, string.Empty).ToLower();
            if (fileName.EndsWith("-cs"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-cs", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-uc"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-uc", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-mr"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-mr", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-es"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-es", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-ch"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-ch", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("ch"))
            {
                fileName = ReplaceLastOccurrence(fileName, "ch", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-4k"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-4k", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("_2k"))
            {
                fileName = ReplaceLastOccurrence(fileName, "_2k", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-uncensored"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-uncensored", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-1"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-1", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-2"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-2", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-3"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-2", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-a"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-a", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-b"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-b", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-c"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-c", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-d"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-d", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("-u"))
            {
                fileName = ReplaceLastOccurrence(fileName, "-u", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("_c"))
            {
                fileName = ReplaceLastOccurrence(fileName, "_c", string.Empty);
                Console.WriteLine(fileName);
            }
            else if (fileName.EndsWith("c"))
            {
                fileName = ReplaceLastOccurrence(fileName, "c", string.Empty);
                Console.WriteLine(fileName);
            }
            else
            {
                Console.WriteLine(fileName);
            }

            return fileName;
        }

        public string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
    }
}