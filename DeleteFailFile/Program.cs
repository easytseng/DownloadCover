using CommonFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteFailFile
{
    internal class Program
    {
        private static FileUtil fileUtil = new FileUtil();

        static void Main(string[] args)
        {
            string path = @"O:\";

            DirectoryInfo rootDir = new DirectoryInfo(path);
            fileUtil.WalkDirectoryTree(rootDir, executeFile);
        }

        private static void executeFile(string filePath)
        {
            fileUtil.checkFileSize(filePath);
        }
    }
}
