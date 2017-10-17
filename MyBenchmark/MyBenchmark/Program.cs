using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            TesterCore.RunAllTests(typeof(Program).Assembly);
            Console.ReadLine();
        }

        [MyTest(500)]
        [Params(100, 1000, 2000, 10000)]
        public static void Test1(int ArraySize)
        {
            var sha256 = SHA256.Create();
            var data = new byte[ArraySize];

            new Random().NextBytes(data);

            byte[] Sha256 = sha256.ComputeHash(data);
        }

        [MyTest]
        [Params(100, 1000, 2000, 10000)]
        public static void Test2(int ArraySize)
        {
            var md5 = MD5.Create();
            var data = new byte[ArraySize];

            new Random().NextBytes(data);

            byte[] Md5 = md5.ComputeHash(data);
        }
    }
}
