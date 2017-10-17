using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace MyBenchmark
{
    public class TesterCore
    {
        public static void RunAllTests(Assembly asm)
        {
            foreach (var type in asm.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var myTestAttrib = method.GetCustomAttribute<MyTestAttribute>();

                    if (myTestAttrib != null)
                    {
                        RunTest(method);
                    }
                }
            }

            Console.WriteLine("All test done.");
        }

        private static void RunTest(MethodInfo method)
        {
            var timer = new Stopwatch();

            var myTestAttrib = method.GetCustomAttribute<MyTestAttribute>();
            var paramsAttrib = method.GetCustomAttribute<ParamsAttribute>();

            if (!method.IsStatic)
            {
                throw new NotImplementedException("Статические методы не поддерживаются, сделайте ДЗ!");
            }

            if (paramsAttrib == null)
            {
                throw new NotImplementedException("Методы без параметров не поддерживаются, сделайте ДЗ!");
            }

            foreach (var param in paramsAttrib.Params)
            {
                timer.Restart();    // Reset + Start

                Console.Write("Begin {0}...", method.Name);

                for (var i = 0; i < myTestAttrib.TestCount; i++)
                {
                    method.Invoke(null, new[] {param});
                }

                timer.Stop();

                var avgTime = timer.ElapsedMilliseconds / (double) myTestAttrib.TestCount;

                // "\r" переведет каретку в начало строки и текст будет выведен по верх прошлой записи "Begin..."
                Console.WriteLine("\r{0}:\tavg: {1} ms ( {2} ms / {3} )",
                    method.Name,
                    avgTime,
                    timer.ElapsedMilliseconds,
                    myTestAttrib.TestCount);

                // В C# 7.0 можно писать удобнее
                //Console.WriteLine($"\r{method.Name}:\tavg: {avgTime} ms ( {timer.ElapsedMilliseconds} ms / {myTestAttrib.TestCount} )");
            }
        }
    }
}