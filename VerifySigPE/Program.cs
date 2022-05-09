using System;
using System.Collections.Generic;
using System.Linq;
using Security.WinTrust;
using System.IO;

namespace VerifySigPE
{
    class Program
    {
        static string[] ListAllPeFiles(string path)
        {
            string[] files = Directory.GetFiles(path,"*",SearchOption.AllDirectories);
            var pefiles =  files.Where(file => file.ToLower().EndsWith("exe") || file.ToLower().EndsWith("dll")).ToList();
            return pefiles.ToArray();
        }
        static void Main(string[] args)
        {
            if (args.Length < 1)
                return;
            var dir_path = args[0];
            if (!Directory.Exists(dir_path))
            {
                Console.WriteLine("Dir not exists.");
                return;
            }

            string[] files = ListAllPeFiles(dir_path);
            Console.WriteLine("[*]File nums: {0} ", files.Length);
            foreach (var filepath in files)
            {
                bool ret = WinTrust.VerifyEmbeddedSignature(filepath);
                if(ret == false)
                    Console.WriteLine("FilePath: {0}, result: {1}", filepath, ret);
            }
            Console.WriteLine("[*]All files checked.");
        }
    }
}
