using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Management;

namespace TestConsoleApplication
{
    class Program
    {
        class Range
        {
            public float From { get; set; }
            public float To { get; set; }
        }

        static void Main(string[] args)
        {
            var v1 = new Vector(1, 2);
            var v2 = new Vector(8, 6);

            var r = new Range { From = 5, To = 3 };

            Console.WriteLine(VectorToString(v1));
            Console.WriteLine(VectorToString(v2));
            Console.WriteLine(VectorToString(r));

            var v3 = VectorToString<Vector>("Y=98;");
            Console.WriteLine(v3);

            Console.ReadLine();
        }

        public static T VectorToString<T>(string data)
        {
            var type = typeof(T);

            var props_tuple = data.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var obj = Activator.CreateInstance<T>();

            foreach (var tuple in props_tuple)
            {
                var t = tuple.Split('=');
                var prop = type.GetProperty(t[0]);

                prop.SetValue(obj, Convert.ChangeType(t[1], prop.PropertyType));
            }

            return obj;
        }

        public static string VectorToString<T>(T obj)
        {
            var result = "";

            foreach (var prop in obj.GetType().GetProperties())
            {
                var val = prop.GetValue(obj);
                result += string.Format("{0}={1};", prop.Name, val);
            }

            return result;
        }
    }
}